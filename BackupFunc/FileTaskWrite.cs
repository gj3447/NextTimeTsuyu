using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace BackupFunc
{
    /// <summary>
    /// Chunk => 
    /// </summary>
    public class FileTaskWrite : FileTask
    {
        bool written = false;
        /// <summary>
        /// 청크 내용만큼만 열린 Stream 에 쓰기
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public bool h_write_file(Chunk chunk)
        {
            if (index == chunk.get_index)
            {
                lock (stream)
                {
                    stream.Write(chunk.get_data, 0, chunk.get_data.Length);
                }
                written = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 줄바꿈 문자를 구분자로 사용해서 해당 문자열 쓰기
        /// </summary>
        /// <param name="string_"></param>
        /// <returns></returns>
        public bool h_write_setting(string string_ )
        {
            byte[] data = Encoding.UTF8.GetBytes(string_);
            byte[] crlf = Encoding.UTF8.GetBytes("\r\n");
            lock (stream)
            {
                if (written)
                    stream.Write(crlf, 0, data.Length);
                stream.Write(data, 0, data.Length);
            }
            written = true;
            return true;
        }
        /// <summary>
        /// 파일 하나가 하나의 청크
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public bool h_write_chunk(Chunk chunk )
        {
            byte[] data = chunk.ToBytes();
            lock (stream)
            {
                stream.Write(data, 0, data.Length);
            }
            written = true;
            return true;
        }
        /// <summary>
        /// all chunk 파일에다가 우르르 다쏟아버리기
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public bool h_write_all_chunk(Chunk chunk)
        {
            byte[] data = chunk.ToBytes();
            byte[] crlf = Encoding.UTF8.GetBytes("\r\n");
            lock (stream)
            {
                if (written)
                    stream.Write(crlf, 0, data.Length);
                stream.Write(data, 0, data.Length);
            }
            written = true;
            return true;
        }
        public FileTaskWrite(FileInfo file_info , Backup bu) : base()
        {
            string root_path = bu.get_setting.get_from_path;
            string full_path = file_info.FullName;
            path = Static.h_create_relative_path(full_path, root_path);
            stream = new FileStream(full_path, FileMode.Create, FileAccess.Write);
            index = 0;
        }
    }
}
