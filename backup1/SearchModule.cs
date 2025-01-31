using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
namespace BackupFunc
{
    public class SearchModule : Module,IWorkable
    {
        public int get_priority()
        {
            return 1;
        }
        public WORKABLE_STATE get_state()
        {
            return WORKABLE_STATE.START;
        }
        public Action get_work()
        {
            return null;
        }
        protected static Action<Backup> read_struct =(bu)=>
        {
            DirectoryInfo dg_dir;
            while (!bu.get_search_directory_queue.
            TryDequeue(out dg_dir))
            {}
            foreach(DirectoryInfo directory in dg_dir.GetDirectories())
            {
                bu.get_search_directory_queue.Enqueue(directory);
            }
            foreach(FileInfo file in dg_dir.GetFiles())
            {
                FileTaskRead fri = new FileTaskRead(file, bu);
                while (!bu.get_read_file_dic
                .TryAdd(fri.get_full_path,fri )) ;
            }
        };
        protected static Action<Backup> write_struct

        public SearchModule(Backup bu) : base(bu)
        {
        }
    }
}