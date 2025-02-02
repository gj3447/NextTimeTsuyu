using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public abstract class LoadBalancer
    {
        public static LoadBalancer h_create_load_balancer(Backup backup, LOADBALANCER_TYPE type)
        {
            switch(type)
            {
                case LOADBALANCER_TYPE.ALL_RAND:
                    return new LoadBalancer_AllRand(backup);
                case LOADBALANCER_TYPE.DEMOCRACY:
                    return new LoadBalancer_Democracy(backup);
                case LOADBALANCER_TYPE.DEMOCRACY_RAND:
                    return new LoadBalancer_DemocracyRand(backup);
                case LOADBALANCER_TYPE.SEQ:
                    return new LoadBalancer_Seq(backup);
                case LOADBALANCER_TYPE.WEIGHT_RAND:
                    return new LoadBalancer_WeightRand(backup);
                default:
                    return new LoadBalancer_AllRand(backup);

            }
        }
        public virtual Action h_work()
        {
            return null;
        }
        protected LoadBalancer(Backup backup) 
        {
        }
    }
    public enum LOADBALANCER_TYPE
    {
        NULL,
        ALL_RAND,
        DEMOCRACY,
        DEMOCRACY_RAND,
        SEQ,
        WEIGHT_RAND,
    }
}
