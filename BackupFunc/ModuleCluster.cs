using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class ModuleCluster
    {
        protected Backup n_backup;
        protected SearchModule n_search_module;
        protected ReadModule n_read_module;
        protected WriteModule n_write_module;
        protected SettingModule n_setting_module;


        public SearchModule get_search_module => n_search_module;
        public ReadModule get_read_module => n_read_module;
        public WriteModule get_write_module => n_write_module;
        public SettingModule get_setting_module => n_setting_module;

        public bool is_init_module_all
        { 
            get
            {
                if (n_search_module == null) return false;
                if (n_read_module == null) return false;
                if (n_write_module == null) return false;
                if (n_setting_module == null) return false;
                return true;
            } 
        }
        public bool is_init_module_any
        {
            get
            {
                if (n_search_module != null) return true;
                if (n_read_module != null) return true;
                if (n_write_module != null) return true;
                if (n_setting_module != null) return true;
                return false;
            } 
        }
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
        public ModuleCluster(Backup backup , Setting s)
        {
            Backup n_backup = backup;
            h_init_module(s);
        }
        public void h_init_module(Setting s)
        {
            if(is_init_module_all)
            { 
                return; 
            }
            if (is_init_module_any)
            {
                if(s.get_mode==MODE.SAVE)
                {
                    throw new Exception("why save mode module is exists??");
                }
                else
                {
                    h_init_load_module(s);
                }
                return;
            }
            else
            {
                if (s.get_mode == MODE.SAVE)
                {
                    h_init_save_module(s);
                }
                else
                {
                    h_init_load_module(s);
                }
            }
        }
        protected void h_init_save_module(Setting s)
        {
            if (s.is_init_module_all)
            {
                n_search_module = new SearchModuleSave(n_backup);
                n_setting_module = new SettingModuleSave(n_backup);
                if (s.get_struct_type == STRUCT_TYPE.ALL_CHUNK)
                {
                    n_read_module = new
                }
                if (s.get_struct_type == STRUCT_TYPE.ALL_CHUNK)
                { }
                if (s.get_struct_type == STRUCT_TYPE.ALL_CHUNK)
                { }
            }
            else
            {
                throw new Exception("");
            }
        }
        protected void h_init_load_module(Setting s)
        {
            if (s.is_load_module())
            {
                h_set_load
            }
            else
            {

            }
        }
        protected void h_init_load_module_all(Setting s)
        { }
        protected void h_init_load_module_merge_all(Setting s)
        { }
        protected void h_init_load_module_part(Setting s)
        { }
        
    }
}
