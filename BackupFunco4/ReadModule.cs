using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{

    /// <summary>
    /// 파일 읽기 I/O를 담당하는 모듈
    /// </summary>
    public class ReadModule : IModule
    {
        private readonly MemoryCluster _memory;

        public ReadModule(MemoryCluster memory)
        {
            _memory = memory;
        }
        /// <summary>
        /// 지정한 ReadTask를 처리하여 파일의 chunk를 읽고, WriteTask를 생성하여 큐에 추가
        /// </summary>
        public void ProcessTask(ReadTask task)
        {
            try
            {
                if (!File.Exists(task.FilePath))
                    return;

                using (var fs = new FileStream(task.FilePath, FileMode.Open, FileAccess.Read))
                {
                    fs.Seek(task.Offset, SeekOrigin.Begin);
                    byte[] buffer = new byte[task.ChunkSize];
                    int bytesRead = fs.Read(buffer, 0, task.ChunkSize);

                    if (bytesRead > 0)
                    {
                        // 예제에서는 백업 대상 경로를 "Backup" 폴더 아래에 원본 경로와 동일하게 재현
                        string destPath = Path.Combine("Backup", task.FilePath.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));

                        // 읽은 데이터를 WriteTask로 생성 후 WriteQueue에 추가
                        var writeTask = new WriteTask
                        {
                            DestinationFilePath = destPath,
                            Offset = task.Offset,
                            Data = buffer.Take(bytesRead).ToArray()
                        };
                        _memory.WriteQueue.Enqueue(writeTask);

                        // 파일의 끝에 도달하지 않았다면, 다음 chunk에 대한 ReadTask 생성
                        if (fs.Position < fs.Length)
                        {
                            _memory.ReadQueue.Enqueue(new ReadTask
                            {
                                FilePath = task.FilePath,
                                Offset = fs.Position,
                                ChunkSize = task.ChunkSize
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReadModule] {task.FilePath} 처리 중 오류: {ex.Message}");
            }
        }
    }

}
