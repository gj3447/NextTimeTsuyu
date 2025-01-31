using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class ModuleCluster
    {
        protected SearchModule n_search_module;
        protected ReadModule n_read_module;
        protected WriteModule n_write_module;
        protected SettingModule n_setting_module;

        public SearchModule get_search_module => n_search_module;
        public ReadModule get_read_module => n_read_module;
        public WriteModule get_write_module => n_write_module;
        public SettingModule get_setting_module => n_setting_module;

        public List<IWorkable> get_workable_list
        { 
            get
            {
                List<IWorkable> result = new List<IWorkable>();
                if(n_search_module != null)
                { result.Add(n_search_module); }
                if(n_setting_module != null)
                { result.Add(n_setting_module); }
                if (n_write_module != null)
                { result.Add(n_write_module); }
                if (n_read_module != null)
                { result.Add(n_read_module); }
                return result;
            } 
        }
        public ModuleCluster(Backup backup)
        {
            this.n_backup = backup;
        }
    }
}
