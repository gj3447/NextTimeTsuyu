using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class ReadModule :Module,IWorkable
    {
        public ReadModule(Backup bu) : base(bu)
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
            return null;
        }
        protected static Action<Backup> h_read_save_file_system = (bu)=>
        {
            
        };
        protected static Action<Backup> h_read_load_file_system = (bu)=>
        {
        };
        protected static Action<Backup> h_read_load_chunk = (bu) =>
        {

        };
        protected static Action<Backup> h_read_load_all_chunk = (bu) =>
        {
        };
    }
}
