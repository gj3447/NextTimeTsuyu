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
        public static readonly string setting_file_name ="setting.ntt";

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
        public ConcurrentDictionary<string, SaveFileInfo> get_save_file_dic => n_save_file_dic;
        public ConcurrentDictionary<string, SaveDirectoryInfo> get_save_directory_dic => n_save_directory_dic;

        public bool is_save_field_load
        {
            get
            {
                if (worker_count < 0) return false;
                if (struct_type == STRUCT_TYPE.NULL) return false;
                return true;
            }
        }

        #endregion

        #region Func
      


        public SaveDirectoryInfo get_save_directory(string key)
        {
            SaveDirectoryInfo result = null;
            n_save_directory_dic.TryGetValue(key, out result);
            return result;
        }
        public SaveFileInfo get_save_file(string key)
        {
            SaveFileInfo result = null;
            n_save_file_dic.TryGetValue(key, out result);
            return result;
        }
        public int get_save_file_remainder(string file_path, int index)
        {
            SaveFileInfo sfi = get_save_file(file_path);
            if (sfi == null)
                return -1;
            else if(sfi.get_index_max < index)
            {
                throw new Exception("save file index out of range!");
            }
            else if (sfi.get_index_max<index)
            {
                return get_chunk_size;
            }
            else if (sfi.get_index_max == index)
            {
                return sfi.get_index_remainer;
            }
            return -1;
        }
        public bool add_save_directory(string path)
        {
            if (n_save_directory_dic.ContainsKey(path))
                return false;
            return n_save_directory_dic.TryAdd(path,new SaveDirectoryInfo(path));
        }
        public bool add_save_file(string path)
        {
            if (n_save_file_dic.ContainsKey(path))
                return false;
            return n_save_file_dic.TryAdd(path, new SaveFileInfo(path));
        }

        public string h_field2string()
        {
            string f = h_setting_type2string(SETTING_TYPE.from_path);
            string t = h_setting_type2string(SETTING_TYPE.to_path);
            string w = h_setting_type2string(SETTING_TYPE.worker_count);
            string m = h_setting_type2string(SETTING_TYPE.mode);
            string c = h_setting_type2string(SETTING_TYPE.chunk_size);
            string s = h_setting_type2string(SETTING_TYPE.struct_type);
            return $"{f}\r\n{t}\r\n{w}\r\n{m}\r\n{c}\r\n{s}";
        }
        public string h_savefield2string()
        {
            string c = h_setting_type2string(SETTING_TYPE.chunk_size);
            string s = h_setting_type2string(SETTING_TYPE.struct_type);
            return $"{c}\r\n{s}";
        }
        public string h_save_file2string()
        {
            string result = "";
            bool start = false;
            foreach (SaveFileInfo e in n_save_file_dic.Values)
            {
                if (start) result += "\r\n";
                else start = true;
                result += e.ToString();
            }
            return result;
        }
        public string h_save_directory2string()
        {
            string result = "";
            bool start = false;
            foreach (SaveDirectoryInfo e in n_save_directory_dic.Values)
            {
                if (start) result += "\r\n";
                else start = true;
                result += e.ToString();
            }
            return result;
        }
        public bool h_set_setting_type(SETTING_TYPE type, string str)
        {

            switch (type)
            {
                case SETTING_TYPE.from_path:
                    from_path = str;
                    break;
                case SETTING_TYPE.to_path:
                    to_path = str;
                    break;
                case SETTING_TYPE.chunk_size:
                    chunk_size = int.Parse(str);
                    break;
                case SETTING_TYPE.struct_type:
                    struct_type = (STRUCT_TYPE)Enum.Parse(typeof(STRUCT_TYPE), str);
                    break;
                case SETTING_TYPE.worker_count:
                    worker_count = int.Parse(str);
                    break;
                case SETTING_TYPE.mode:
                    mode = (MODE)Enum.Parse(typeof(MODE), str);
                    break;
                case SETTING_TYPE.directory:
                    SaveDirectoryInfo sdi = new SaveDirectoryInfo(str);
                    return n_save_directory_dic.TryAdd(str, sdi);
                case SETTING_TYPE.file:
                    SaveFileInfo sfi = new SaveFileInfo(str);
                    return n_save_file_dic.TryAdd(str, sfi);
                default:
                    return false;
            }
            return true;
        }
        public object h_get_setting_type(SETTING_TYPE type)
        {
            switch (type)
            {
                case SETTING_TYPE.from_path:
                    return from_path;
                case SETTING_TYPE.to_path:
                    return to_path;
                case SETTING_TYPE.chunk_size:
                    return chunk_size;
                case SETTING_TYPE.struct_type:
                    return struct_type;
                case SETTING_TYPE.worker_count:
                    return worker_count;
                case SETTING_TYPE.mode:
                    return mode;
                default:
                    return null;
            }
        }
        public string h_setting_type2string(SETTING_TYPE type)
        {
            if (type == SETTING_TYPE.file)
                return h_save_file2string();
            if (type == SETTING_TYPE.directory)
                return h_save_directory2string();
            return type.ToString() + ":" + h_get_setting_type(type).ToString();
        }


        #endregion
        public Setting(
                        string from_path_, string to_path_,
                       int worker_count_, MODE mode_,
                       int chunk_size_, STRUCT_TYPE struct_type_
                       )
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
            return h_field2string() + "\r\n" +
                   h_save_file2string() + "\r\n" +
                   h_save_directory2string();
        }
    }

    public enum STRUCT_TYPE
    {
        NULL,
        FILE_SYSTEM,
        CHUNK,
        ALL_CHUNK,
    }
    public enum SETTING_TYPE
    {
        from_path,
        to_path,
        chunk_size,
        struct_type,
        worker_count,
        mode,
        file,
        directory,
    }
}
