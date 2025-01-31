using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Concurrent;


namespace BackupFunc
{
    public class Backup
    {
        public static Backup m_backup_singleton;

        protected Setting n_setting;
        protected List<Task> n_worker_list;

        protected ModuleCluster n_module_cluster;
        protected MemoryCluster n_memory_cluster;


        #region Get
        public Setting get_setting => n_setting;

        #endregion
        
        public static void h_save(string from_path, string to_path,
                                  int chunk_size, 
                                  int worker_count,STRUCT_TYPE type)
        {

        }
        public static void h_load(int worker_count)
        {

        }
        public Backup(Setting setting)
        {
            m_backup_singleton = this;
            n_setting
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
