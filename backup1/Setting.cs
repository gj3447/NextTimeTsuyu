using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace BackupFunc
{
    public class Setting
    {
        public static readonly string setting_file_name ="setting";

        #region Field List
        //not save
        protected string      from_path;
        protected string      to_path;
        protected int         worker_count;
        protected MODE mode;

        //save
        protected int         chunk_size;
        protected STRUCT_TYPE struct_type;

        #endregion

        #region Dictionary List
        protected ConcurrentDictionary<string, SaveFileInfo> n_save_file_dic;
        protected ConcurrentDictionary<string, SaveDirectoryInfo> n_save_directory_dic;
        #endregion

        #region Property List
        public string get_from_path => from_path;
        public string get_to_path => to_path;
        public int get_chunk_size => chunk_size; 
        public int get_worker_count => worker_count; 
        public STRUCT_TYPE get_struct_type=> struct_type;
        public MODE get_mode => mode;

        #endregion

        public Setting(string from_path_, string to_path_,
                       MODE mode_ , int worker_count_, 
                       int chunk_size_, STRUCT_TYPE struct_type_ )
        {
            from_path = from_path_;
            to_path = to_path_;
            chunk_size = chunk_size_;
            mode = mode_;

            worker_count = worker_count_;
            struct_type = struct_type_;
            n_save_directory_dic = new ConcurrentDictionary<string, SaveDirectoryInfo>();
            n_save_file_dic = new ConcurrentDictionary<string, SaveFileInfo>();
        }
        public override string ToString()
        {
            return FieldToString() + "\r\n" +
                   DirectorydicToString() + "\r\n" +
                   FiledicToString();
        }
        public string FieldToString()
        {
            string f = $"from_path:{from_path}";
            string t = $"to_path:{to_path}";
            string w = $"worker_count:{worker_count}";
            string m = $"mode:{mode}";
            string c = $"chunk_size:{chunk_size}";
            string s = $"struct_type:{struct_type}";
            return $"{f}\r\n{t}\r\n{w}\r\n{m}\r\n{c}\r\n{s}";
        }
        public string SaveFieldToString()
        {
            string c = $"chunk_size:{chunk_size}";
            string s = $"struct_type:{struct_type}";
            return $"{c}\r\n{s}";
        }
        public string FiledicToString()
        {
            string result = "";
            bool start = false;
            foreach(SaveFileInfo e in n_save_file_dic.Values)
            {
                if (start) result += "\r\n";
                else start = true;
                result += e.ToString();
            }
            return result;
        }
        public string DirectorydicToString()
        {
            string result = "";
            bool start = false;
            foreach(SaveDirectoryInfo e in n_save_directory_dic.Values)
            {
                if (start) result += "\r\n";
                else start = true;
                result += e.ToString();
            }
            return result;
        }
    }

    public enum STRUCT_TYPE
    {
        FILE_SYSTEM,
        CHUNK,
        ALL_CHUNK,
    }
}
