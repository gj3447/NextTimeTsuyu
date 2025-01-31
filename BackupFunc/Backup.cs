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
        
        public static void h_save(string from_path, string to_path, int worker_count,
                                  int chunk_size, STRUCT_TYPE struct_type)
        {
            MODE mode = MODE.SAVE;
            Setting setting = new Setting
                (from_path,to_path,worker_count,mode,chunk_size,struct_type);
            Backup backup = new Backup(setting);

        }
        public static void h_load(string from_path,string to_path, int worker_count,
                                  int chunk_size, STRUCT_TYPE struct_type)
        {
            MODE mode = MODE.LOAD;
            Setting setting = new Setting
                (from_path,to_path,worker_count,mode,chunk_size,struct_type);
        }
        public static void h_load(string from_path, string to_path ,int worker_count)
        {
            MODE mode = MODE.LOAD;
            Setting setting = new Setting
                (from_path, to_path, worker_count, mode, -1, STRUCT_TYPE.NULL);
        }
        public Backup(Setting setting)
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
