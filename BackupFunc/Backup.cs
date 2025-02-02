using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace BackupFunc
{
    public class Backup
    {
        public static Backup m_backup_singleton;

        protected Setting n_setting;
        protected ModuleCluster n_module_cluster;
        protected MemoryCluster n_memory_cluster;
        protected LoadBalancer n_load_balancer;
        protected List<Task> n_worker_list;

        #region Property List
        public Setting get_setting => n_setting;
        public ModuleCluster get_module_cluster => n_module_cluster;
        public MemoryCluster get_memory_cluster => n_memory_cluster;
        public LoadBalancer get_load_balancer => n_load_balancer;
        #endregion

        public static void h_save(string from_path, string to_path, int worker_count, LOADBALANCER_TYPE lbt,
                                  int chunk_size, STRUCT_TYPE struct_type)
        {
            MODE mode = MODE.SAVE;
            Backup backup = new Backup();
            Setting setting = new Setting (from_path,to_path,worker_count,mode,chunk_size,struct_type);
            ModuleCluster module_cluster = new ModuleCluster(backup, setting);
            MemoryCluster memory_cluster = new MemoryCluster(backup, setting);
            LoadBalancer loadbalancer = LoadBalancer.h_create_load_balancer(backup ,lbt);
            backup.h_set_worker(loadbalancer, worker_count);
        }
        public static void h_load(string from_path,string to_path, int worker_count, LOADBALANCER_TYPE lbt,
                                  int chunk_size, STRUCT_TYPE struct_type)
        {
            MODE mode = MODE.LOAD;
            Backup backup = new Backup();
            Setting setting = new Setting (from_path,to_path,worker_count,mode,chunk_size,struct_type);
            ModuleCluster module_cluster = new ModuleCluster(backup, setting);
            MemoryCluster memory_cluster = new MemoryCluster(backup, setting);
            LoadBalancer loadbalancer = LoadBalancer.h_create_load_balancer(backup, lbt);
            backup.h_set_worker(loadbalancer, worker_count);
        }
        public static void h_load(string from_path, string to_path ,int worker_count, LOADBALANCER_TYPE lbt)
        {
            MODE mode = MODE.LOAD;
            Backup backup = new Backup();
            Setting setting = new Setting (from_path, to_path, worker_count, mode, -1, STRUCT_TYPE.NULL);
            ModuleCluster module_cluster = new ModuleCluster(backup ,setting);
            MemoryCluster memory_cluster = new MemoryCluster(backup, setting);
            LoadBalancer loadbalancer = LoadBalancer.h_create_load_balancer(backup, lbt);
            backup.h_set_worker(loadbalancer, worker_count);
        }
        public List<Task> h_set_worker(LoadBalancer load_balancer , int worker_count)
        {
            if(worker_count<1)
            {
                throw new Exception("why worker count is not exists...");
            }
            List<Task> tasks = new List<Task>();
            for(int i = 0;i< worker_count; i++)
            {
                Task task = new Task(load_balancer.h_work());
                tasks.Add(task);
            }
            return tasks;
        }
        public void h_start()
        {

        }
        private Backup()
        {
        }
        public void h_work()
        {
        }

    }
    public enum MODE
    {
        SAVE,
        LOAD
    }
}
