using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    internal class SettingModuleSave : SettingModule
    {
        public SettingModuleSave(Backup bu) : base(bu)
        {
        }
        public override int get_priority()
        {
            return 10;
        }
        public override WORKABLE_STATE get_state()
        {
            if (n_backup.get_memory_cluster.get_write_setting == null)
            { throw new Exception("why read file stream is null"); }
            if (n_backup.get_memory_cluster.get_write_setting.get_state == FILETASK_STATE.END)
                return WORKABLE_STATE.END;
            if (n_backup.get_memory_cluster.get_setting_save_queue.Count == 0)
                return WORKABLE_STATE.STOP;
            if (n_backup.get_memory_cluster.get_write_setting.get_state == FILETASK_STATE.START)
                return WORKABLE_STATE.START;
            return WORKABLE_STATE.WORKING;
        }
        public override Action get_work()
        {
            return () => { h_setting_save(n_backup); };
        }
    }
}
