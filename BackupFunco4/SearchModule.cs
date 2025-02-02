using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{
    /// <summary>
    /// 폴더 구조 탐색을 담당하는 모듈
    /// </summary>
    public class SearchModule : IModule
    {
        private readonly MemoryCluster _memory;

        public SearchModule(MemoryCluster memory)
        {
            _memory = memory;
        }

        /// <summary>
        /// 지정한 폴더를 탐색하여 하위 폴더와 파일을 큐에 추가함
        /// </summary>
        public void ProcessTask(SearchTask task)
        {
            try
            {
                // 현재 디렉터리의 하위 폴더 및 파일 가져오기
                var directories = Directory.GetDirectories(task.DirectoryPath);
                var files = Directory.GetFiles(task.DirectoryPath);

                // 하위 폴더에 대해 검색 작업을 재귀적으로 추가
                foreach (var dir in directories)
                {
                    _memory.SearchQueue.Enqueue(new SearchTask { DirectoryPath = dir });
                }

                // 각 파일에 대해 읽기 작업을 생성 (첫번째 chunk부터 시작)
                foreach (var file in files)
                {
                    _memory.ReadQueue.Enqueue(new ReadTask
                    {
                        FilePath = file,
                        Offset = 0,
                        ChunkSize = 1024 * 1024 // 예제에서는 1MB 단위
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SearchModule] {task.DirectoryPath} 처리 중 오류: {ex.Message}");
            }
        }
    }
}
