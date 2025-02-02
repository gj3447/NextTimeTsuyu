using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{

    public class LoadBalancerPriority : LoadBalancer
    {
        public override object GetProcessInfo(ModuleCluster module, MemoryCluster memory)
        {
            // 각 큐의 작업 수와 우선순위 정보를 동적으로 반환할 수 있음
            return new
            {
                SearchCount = memory.SearchQueue.Count,
                ReadCount = memory.ReadQueue.Count,
                WriteCount = memory.WriteQueue.Count,
                HighestPriorityQueue = GetHighestPriorityQueue(memory)
            };
        }

        private string GetHighestPriorityQueue(MemoryCluster memory)
        {
            // 예시: 가장 작업 수가 많은 큐를 우선순위로 선정
            int searchCount = memory.SearchQueue.Count;
            int readCount = memory.ReadQueue.Count;
            int writeCount = memory.WriteQueue.Count;
            if (searchCount >= readCount && searchCount >= writeCount)
                return "Search";
            else if (readCount >= searchCount && readCount >= writeCount)
                return "Read";
            else
                return "Write";
        }

        public override Action GetProcess(ModuleCluster module, MemoryCluster memory)
        {
            // 동적으로 각 큐의 작업량을 고려하여 우선순위를 결정
            if (memory.WriteQueue.Count > memory.ReadQueue.Count && memory.WriteQueue.Count > memory.SearchQueue.Count)
            {
                if (memory.WriteQueue.TryDequeue(out WriteTask writeTask))
                {
                    return () => module.WriteModule.ProcessTask(writeTask);
                }
            }
            else if (memory.ReadQueue.Count > memory.SearchQueue.Count)
            {
                if (memory.ReadQueue.TryDequeue(out ReadTask readTask))
                {
                    return () => module.ReadModule.ProcessTask(readTask);
                }
            }
            else if (memory.SearchQueue.TryDequeue(out SearchTask searchTask))
            {
                return () => module.SearchModule.ProcessTask(searchTask);
            }
            // 작업이 없으면 대기
            return () => Thread.Sleep(10);
        }
    }
}
