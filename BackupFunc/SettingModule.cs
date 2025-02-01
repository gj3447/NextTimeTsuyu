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

        /// <summary>
        /// 세팅 파일을  write_setting 에 저장함
        /// </summary>
        protected static Action<Backup> h_setting_save = (backup) =>
        {
            string str = backup.get_memory_cluster.h_dequeue_setting_save();
            if (str == null) throw new Exception("setting save queue is null");
            FileTaskWrite ftw = backup.get_memory_cluster.get_write_setting;
            if (ftw == null) throw new Exception("");
            ftw.h_write_setting(str);
        };
        /// <summary>
        /// 세팅 파일을 읽은후에 file 과 dictionary 만 속속들이 세팅
        /// </summary>
        protected static Action<Backup> h_setting_load = (backup) =>
        {
            FileTaskRead ftr = backup.get_memory_cluster.get_read_setting;
            if (ftr == null) throw new Exception("read_setting is null");
            string str = ftr.h_read_setting();
            string data;
            SETTING_TYPE st = Static.h_string2setting_type(str, out data);
            if (st == SETTING_TYPE.file || st == SETTING_TYPE.directory)
                backup.get_setting.h_set_setting_type(st, str);
            
        };
        /// <summary>
        /// 세팅 파일을 읽은후에 file 과 dictionary 와 또한 chunk_size, struct_type 이 어떤지 확인후에 module 설정까지~
        /// </summary>
        protected static Action<Backup> h_setting_load_module_create = (backup) =>
        {

        };
    }
}