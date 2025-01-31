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
            return null;
        }
        protected static Action<Backup> read_file_system = (bu)=>
        {
        };
        protected static Action<Backup> read_chunk = (bu)=>
        {
        };
        protected static Action<Backup> read_all_chunk = (bu) =>
        {
        };
    }
}
