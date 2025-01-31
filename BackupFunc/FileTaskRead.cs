using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class FileTaskRead :FileTask
    {

        /// <summary>
        /// 파일 시스템을 읽고 청크로 바꿈
        /// </summary>
        /// <param name="data_size"></param>
        /// <returns>Chunk</returns>
        public Chunk h_read_file(int data_size)
        {
            byte[] buffer = new byte[data_size];
            lock (stream)
            {
                for (int i = 0; i < data_size; i++)
                {
                    int byte_ = stream.ReadByte();
                    if(byte_==-1)
                    {
                        is_end = true;
                        remainder = i ;
                        break;
                    }
                    buffer[i] = (byte)byte_;
                }
            }
            Chunk chunk = new Chunk(buffer, path, index);
            if(!is_end) index++;
            return chunk;
        }
        /// <summary>
        /// setting.ntt 를 읽고 string 으로 바꿈
        /// </summary>
        /// <param name="data_size"></param>
        /// <returns></returns>
        public string h_read_setting(int data_size)
        {
            byte backward = 0;
            byte forward = 0;
            int count = 0;
            bool is_crlf = false;
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                lock (stream)
                {
                    while (true)
                    {
                        int byte_ = stream.ReadByte();
                        if (byte_ == -1) // EOF 체크
                        {
                            remainder = count ;
                            is_end = true;
                            break;
                        }
                        count++;
                        ms.WriteByte((byte)byte_);
                        if (count % 2 == 0) forward = (byte)byte_;
                        else backward = (byte)byte_;
                        // CRLF (\r\n 또는 \n\r) 검사
                        if (Static.h_is_crlf(forward, backward))
                        {
                            is_crlf = true;
                            break;
                        }
                    }
                }
                buffer = ms.ToArray();
            }
            if (is_crlf && buffer.Length >= 2)
            {
                Array.Resize(ref buffer, buffer.Length - 2);
            }
            // MemoryStream 데이터를 문자열로 변환
            return Encoding.UTF8.GetString(buffer);
        }
        /// <summary>
        /// all_chunk 를 읽고 청크로 바꿈 
        /// </summary>
        /// <param name="data_size"></param>
        /// <returns></returns>
        public Chunk h_read_all_chunk(int data_size)
        {
            byte backward = 0;
            byte forward = 0;
            int count = 0;
            bool is_crlf = false;
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                lock (stream)
                {
                    while (true)
                    {
                        int byte_ = stream.ReadByte();
                        if (byte_ == -1) // EOF 체크
                        {
                            is_end = true;
                            remainder = count;
                            break;
                        }
                        count++;
                        ms.WriteByte((byte)byte_);
                        if (count % 2 == 0) forward = (byte)byte_;
                        else backward = (byte)byte_;
                        // CRLF (\r\n 또는 \n\r) 검사

                        if (data_size+2 <=count)
                        { 
                            if (Static.h_is_crlf(forward, backward))
                            {
                                is_crlf = true;
                                break;
                            }
                        }
                    }
                }
                buffer = ms.ToArray();
            }
            if (is_crlf && buffer.Length >= 2)
            {
                Array.Resize(ref buffer, buffer.Length - 2);
            }
            return new Chunk(buffer,data_size);
        }
        /// <summary>
        /// 개개인 청크 파일을 읽고 chunk 로 바꿈
        /// </summary>
        /// <param name="data_size"></param>
        /// <returns></returns>
        public Chunk h_read_chunk(int data_size)
        {
            byte[] buffer;
            int count = 0;
            using (MemoryStream ms = new MemoryStream())
            {
                lock (stream)
                {
                    while (true)
                    {
                        int byte_ = stream.ReadByte();
                        if (byte_ == -1) // EOF 체크
                        {
                            is_end = true;
                            remainder = count;
                            break;
                        }
                        count++;
                        ms.WriteByte((byte)byte_);
                        // CRLF (\r\n 또는 \n\r) 검사
                    }
                }
                buffer = ms.ToArray();
            }

            return new Chunk(buffer, data_size);
        }
        public FileTaskRead(FileInfo file_info, Backup bu) :base()
        {
            string root_path = bu.get_setting.get_from_path;
            string full_path = file_info.FullName;
            path = Static.h_create_relative_path(full_path, root_path);
            stream = new FileStream(full_path, FileMode.Open, FileAccess.Read);
            index = 0;
        }
    }
}
