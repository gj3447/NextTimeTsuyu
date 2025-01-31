using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class SettingModule : Module ,IWorkable
    {
        public SettingModule(Backup bu) : base(bu)
        {
        }

        public virtual int get_priority()
        {
            throw new NotImplementedException();
        }

        public virtual WORKABLE_STATE get_state()
        {
            throw new NotImplementedException();
        }

        public virtual Action get_work()
        {
            throw new NotImplementedException();
        }
        protected static Action<Backup> h_setting_save = (backup) =>
        {
            string str = backup.get_memory_cluster.h_dequeue_setting_save();
            if (str == null) throw new Exception("");
            FileTaskWrite ftw = backup.get_memory_cluster.get_write_setting;
            if (ftw == null) throw new Exception("");
            ftw.h_write_setting(str);
        };
        protected static Action<Backup> h_setting_load = (backup) =>
        {
            FileTaskRead ftr = backup.get_memory_cluster.get_read_setting;
        };
        protected static Action<Backup> h_setting_load_module_create = (backup) =>
        {

        };
    }
}