using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    internal class SettingModuleLoad : SettingModule
    {
        public SettingModuleLoad(Backup bu) : base(bu)
        {
        }
        public override int get_priority()
        {
            return 20;
        }
        public override WORKABLE_STATE get_state()
        {
            if(n_backup.get_memory_cluster.get_read_setting==null)
            { throw new Exception("why read file stream is null"); }
            FILETASK_STATE fts = n_backup.get_memory_cluster.get_read_setting.get_state;
            return FileTask.filetask_state2workable_state(fts);
        }
        public override Action get_work()
        {
            return () => {h_setting_load(n_backup); };
        }
    }
}
