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

        public int get_priority()
        {
            throw new NotImplementedException();
        }

        public WORKABLE_STATE get_state()
        {
            throw new NotImplementedException();
        }

        public Action get_work()
        {
            throw new NotImplementedException();
        }
        protected static Action<Backup> h_read_setting_file = (bu) =>
        {

        };
        protected static Action<Backup> h_write_setting_file = (bu) =>
        {

        };
    }
    public class SettingModuleWrite : SettingModule

}