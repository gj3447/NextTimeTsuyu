using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{
    public class LoadBalancerSimple : LoadBalancer
    {
        public override object GetProcessInfo(ModuleCluster module, MemoryCluster memory)
        {
            return new
            {
                SearchCount = memory.SearchQueue.Count,
                ReadCount = memory.ReadQueue.Count,
                WriteCount = memory.WriteQueue.Count
            };
        }

        public override Action GetProcess(ModuleCluster module, MemoryCluster memory)
        {
            // 우선순위: Search → Read → Write 순서로 단순하게 작업을 선택
            if (memory.SearchQueue.TryDequeue(out SearchTask searchTask))
            {
                return () => module.SearchModule.ProcessTask(searchTask);
            }
            else if (memory.ReadQueue.TryDequeue(out ReadTask readTask))
            {
                return () => module.ReadModule.ProcessTask(readTask);
            }
            else if (memory.WriteQueue.TryDequeue(out WriteTask writeTask))
            {
                return () => module.WriteModule.ProcessTask(writeTask);
            }
            else
            {
                // 작업이 없으면 잠시 대기
                return () => Thread.Sleep(10);
            }
        }
    }

}
