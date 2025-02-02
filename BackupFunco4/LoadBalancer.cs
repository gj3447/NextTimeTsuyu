using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{
    public enum LOADBALANCER_TYPE
    {
        Simple,
        Priority
    }

    public abstract class LoadBalancer
    {
        public static LoadBalancer CreateLoadBalancer(LOADBALANCER_TYPE type)
        {
            switch (type)
            {
                case LOADBALANCER_TYPE.Simple:
                    return new LoadBalancerSimple();
                case LOADBALANCER_TYPE.Priority:
                    return new LoadBalancerPriority();
                default:
                    throw new NotSupportedException("Unknown load balancer type.");
            }
        }

        // 예시로 간단한 상태 정보 반환 (필요에 따라 확장)
        public abstract object GetProcessInfo(ModuleCluster module, MemoryCluster memory);

        // 작업(모듈의 작업 실행 Action)을 반환
        public abstract Action GetProcess(ModuleCluster module, MemoryCluster memory);
    }
}
