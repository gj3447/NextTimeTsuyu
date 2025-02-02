using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{
    internal class Backup
    {
        private readonly MemoryCluster _memoryCluster;
        private readonly ModuleCluster _moduleCluster;
        private readonly int _workerCount;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly LoadBalancer _loadBalancer;

        public Backup(int workerCount, LOADBALANCER_TYPE lbt)
        {
            _memoryCluster = new MemoryCluster();
            _moduleCluster = new ModuleCluster(_memoryCluster);
            _workerCount = workerCount;
            _loadBalancer = LoadBalancer.CreateLoadBalancer(lbt);
        }

        public void Start()
        {
            for (int i = 0; i < _workerCount; i++)
            {
                Task.Run(() => Worker(_cts.Token));
            }
        }

        public void Stop() => _cts.Cancel();

        private void Worker(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                // 로드 밸런서가 현재 상태를 보고 어떤 작업을 선택할지 결정
                object process_info = _loadBalancer.GetProcessInfo(_moduleCluster, _memoryCluster);
                // 필요에 따라 process_info를 기반으로 모니터링 또는 종료 판단 등을 할 수 있음

                // 로드 밸런서가 반환한 Action(모듈 작업)을 실행
                Action processAction = _loadBalancer.GetProcess(_moduleCluster, _memoryCluster);
                processAction();
            }
        }
    }
}
