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
        protected ConcurrentQueue<FileTaskRead> n_read_file_queue;
        protected ConcurrentDictionary<string, FileTaskWrite> n_write_file_dic;
        protected FileTaskRead n_read_setting;
        protected FileTaskWrite n_write_setting;
        #endregion

        #region Property List
        public ConcurrentQueue<DirectoryInfo> get_search_directory_queue => n_search_directory_queue;
        public ConcurrentQueue<Chunk> get_chunk_queue=> n_chunk_queue;
        public ConcurrentQueue<string> get_setting_save_queue => n_setting_save_queue;
        public ConcurrentQueue<FileTaskRead> get_read_file_queue => n_read_file_queue;
        public ConcurrentDictionary<string, FileTaskWrite> get_write_file_dic=> n_write_file_dic;
        public FileTaskRead get_read_setting=>n_read_setting;
        public FileTaskWrite get_write_setting=>n_write_setting;
        #endregion

        #region Func
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
        public void h_enqueue_file_read(FileTaskRead ftr)
        {
            n_read_file_queue.Enqueue(ftr);
        }
        public FileTaskRead h_dequeue_file_read()
        {
            FileTaskRead result = null;
            n_read_file_queue.TryDequeue(out result);
            return result;
        }
        public bool h_add_file_write(FileTaskWrite ftw)
        {
            if(n_write_file_dic.ContainsKey(ftw.get_path))
            {
                throw new Exception();
            }
            return n_write_file_dic.TryAdd(ftw.get_path, ftw);
        }
        public FileTaskWrite h_remove_file_write(string key)
        {
            FileTaskWrite result = null;
            n_write_file_dic.TryRemove(key,out result);
            return result;
        }
        public FileTaskWrite h_get_file_write(string key)
        {
            FileTaskWrite result = null;
            n_write_file_dic.TryGetValue(key, out result);
            return result;
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
        #endregion

        public MemoryCluster(Backup backup ,Setting setting)
        {

            n_search_directory_queue = new ConcurrentQueue<DirectoryInfo>();
            n_chunk_queue = new ConcurrentQueue<Chunk>();
            n_setting_save_queue = new ConcurrentQueue<string>();

            n_read_file_queue = new ConcurrentQueue<FileTaskRead>();
            n_write_file_dic = new ConcurrentDictionary<string, FileTaskWrite>();

            string setting_file_path = Path.Combine(setting.get_from_path, Setting.setting_file_name);
            if (backup.get_setting.get_mode == MODE.SAVE)
            {
                n_read_setting = new FileTaskRead(setting_file_path, backup);
            }
            if (backup.get_setting.get_mode == MODE.LOAD)
            {

            }
        }
    }
}
