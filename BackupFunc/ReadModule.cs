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
        /// <summary>
        /// 그냥 READ QUEUE 에 있는내용을 CHUNK 만큼 읽어들여서 CHUNK QUEUE 에 집어넣고 
        /// 만약에 끝낫을경우 그 값을 Directory SaveFileInfo 에도 적용하고 setting save queue 에도 해당내용 집어넣음
        /// </summary>
        protected static Action<Backup> h_read_save_file_system = (bu)=>
        {
            
        };
        /// <summary>
        /// 얘도 똑같은데 그냥 READ QUEUE 에 있는내용을 CHUNK 만큼 읽어들여서 CHUNK QUEUE 에 집어넣고 그냥 끝
        /// </summary>
        protected static Action<Backup> h_read_load_file_system = (bu)=>
        {
        };
        /// <summary>
        /// 얘는 청크 읽는 애임 파일 전체 다읽어들여서 청크로 만들고 chunk Queue 에 집어넣기~
        /// </summary>
        protected static Action<Backup> h_read_load_chunk = (bu) =>
        {

        };
        /// <summary>
        /// 청크 읽고 chunkqueue~ chunk 패쓰는 어디있는지 알쥐?
        /// </summary>
        protected static Action<Backup> h_read_load_all_chunk = (bu) =>
        {
        };
    }
}
