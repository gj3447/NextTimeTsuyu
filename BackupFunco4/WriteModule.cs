using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{
    /// <summary>
    /// 파일 쓰기(Write)를 담당하는 모듈  
    /// - WriteTask를 처리하여 지정된 대상 파일에 데이터를 기록함
    /// </summary>
    public class WriteModule : IModule
    {
        private readonly MemoryCluster _memory;

        public WriteModule(MemoryCluster memory)
        {
            _memory = memory;
        }

        public void ProcessTask(WriteTask task)
        {
            try
            {
                // 대상 폴더가 없으면 생성
                string directory = Path.GetDirectoryName(task.DestinationFilePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (var fs = new FileStream(task.DestinationFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Seek(task.Offset, SeekOrigin.Begin);
                    fs.Write(task.Data, 0, task.Data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WriteModule] 파일 {task.DestinationFilePath} 처리 중 오류: {ex.Message}");
            }
        }
    }
}
