using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{   
    /// <summary>
    /// 각 모듈이 사용할 작업 데이터들을 담은 ConcurrentQueue 집합
    /// </summary>
    public class MemoryCluster
    {
        public ConcurrentQueue<SearchTask> SearchQueue { get; } = new ConcurrentQueue<SearchTask>();
        public ConcurrentQueue<ReadTask> ReadQueue { get; } = new ConcurrentQueue<ReadTask>();
        public ConcurrentQueue<WriteTask> WriteQueue { get; } = new ConcurrentQueue<WriteTask>();
    }
}
