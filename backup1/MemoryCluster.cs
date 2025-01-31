using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class MemoryCluster
    {


        #region Queue List
        protected ConcurrentQueue<DirectoryInfo> n_search_directory_queue;
        protected ConcurrentQueue<Chunk> n_chunk_queue;
        protected ConcurrentQueue<string> n_setting_save_queue;
        #endregion

        #region IO List
        protected ConcurrentDictionary<string, FileTaskRead> n_read_file_dic;
        protected ConcurrentDictionary<string, FileTaskWrite> n_write_file_dic;
        protected FileTaskRead n_read_setting;
        protected FileTaskWrite n_write_setting;
        #endregion

        public ConcurrentQueue<DirectoryInfo> get_search_directory_queue => n_search_directory_queue;
        public ConcurrentQueue<Chunk> get_chunk_queue=> n_chunk_queue;
        public ConcurrentQueue<string> get_setting_save_queue => n_setting_save_queue;
        public ConcurrentDictionary<string, FileTaskRead> get_read_file_dic => n_read_file_dic;
        public ConcurrentDictionary<string, FileTaskWrite> get_write_file_dic=> n_write_file_dic;
        public FileTaskRead get_read_setting=>n_read_setting;
        public FileTaskWrite get_write_setting=>n_write_setting;

        public void h_enqueue_chunk(Chunk ch)
        {
            n_chunk_queue.Enqueue(ch);
        }
        public void h_enqueue_setting_save(string s)
        {
            n_setting_save_queue.Enqueue(s);
        }
        public void h_enqueue_search_directory(DirectoryInfo dir)
        {
            n_search_directory_queue.Enqueue(dir);
        }
        public Chunk h_dequeue_chunk()
        {
            Chunk ch = null;
            n_chunk_queue.TryDequeue(out ch);
            return ch;
        }
        public string h_dequeue_setting_save()
        {
            string s = null;
            n_setting_save_queue.TryDequeue(out s);
            return s;
        }
        public DirectoryInfo h_dequeue_search_directory()
        {
            DirectoryInfo dir = null;
            n_search_directory_queue.TryDequeue(out dir);
            return dir;
        }
    }
}
