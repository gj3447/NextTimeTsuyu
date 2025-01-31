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
        public virtual int get_priority()
        {
            return 10;
        }
        public virtual WORKABLE_STATE get_state()
        {
            if(n_backup.get_memory_cluster.get_search_directory_queue.Count ==0)
            {
                if (n_is_start) return WORKABLE_STATE.END;
                else return WORKABLE_STATE.WATING;
            }
            else
            {
                if (n_is_start) return WORKABLE_STATE.WORKING;
                else return WORKABLE_STATE.WORKING;
            }
        }
        public virtual Action get_work()
        {
            return null;
        }
        #region Overriding Func List
        /// <summary>
        /// 전체 파일시스템 탐색하면서 dir 을 dictionary 에 저장
        /// </summary>
        protected static Action<Backup> h_search_save =(backup)=>
        {
            // Search dirqueue 에서 탐색할 dir 하나 빼서
            DirectoryInfo search_dir = backup.get_memory_cluster.h_dequeue_search_directory();
            if (search_dir == null) throw new Exception("dequeue search_dir is null?");

            // 내부 구조 search dirqueue 랑 file read queue 에다가 집어넣고
            foreach (DirectoryInfo directory in search_dir.GetDirectories())
            {
                backup.get_memory_cluster.h_enqueue_search_directory(directory);
            }
            foreach(FileInfo fileinfo in search_dir.GetFiles())
            {
                backup.get_memory_cluster.h_enqueue_file_read(new FileTaskRead(fileinfo,backup));
            }

            // 이미 탐색한 dir 으로 치고 dictionary 에 저장하고 세팅에도 저장
            string relative_path = Static.h_create_relative_path(search_dir.FullName, backup.get_setting.get_from_path);
            if (!backup.get_setting.add_save_directory(relative_path))
                throw new Exception();
            backup.get_memory_cluster.h_enqueue_setting_save
                (backup.get_setting.get_save_directory(relative_path).ToString() );
        };
        /// <summary>
        /// 전체 파일시스템 탐색하기만함 (dictionary 저장은 setting module 이 해주겟지)
        /// </summary>
        protected static Action<Backup> h_search_load = (backup) =>
        {
            // Search dirqueue 에서 탐색할 dir 하나 빼서
            DirectoryInfo search_dir = backup.get_memory_cluster.h_dequeue_search_directory();
            if (search_dir == null) throw new Exception("dequeue search_dir is null?");

            // 내부 구조 search dirqueue 랑 file read queue 에다가 집어넣고
            foreach (DirectoryInfo directory in search_dir.GetDirectories())
            {
                backup.get_memory_cluster.h_enqueue_search_directory(directory);
            }
            foreach (FileInfo fileinfo in search_dir.GetFiles())
            {
                backup.get_memory_cluster.h_enqueue_file_read(new FileTaskRead(fileinfo, backup));
            }

        };
        #endregion

        public SearchModule(Backup bu) : base(bu)
        {
        }
    }
}