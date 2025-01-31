using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class WriteModule : Module, IWorkable
    {
        public WriteModule(Backup bu) : base(bu)
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

        public static Action<Backup> h_write_save = (bu) =>
        {
        };
        public static Action<Backup> h_write_save_chunk = (bu) =>
        {
        };
        public static Action<Backup> h_write_save_all_chunk = (bu) =>
        {
        };
        public static Action<Backup> h_write_load_file_system = (bu) =>
        {
        };
    }
}
