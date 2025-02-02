using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class Chunk
    {
        //data[] 의 크기는 chunk_size 가 정배고
        //data byte 뒤의 쓰레기값은 Module 이 알아서 처리해줘야함
        private byte[] data;
        private string path;
        private int index;
        public byte[] get_data  => data;
        public byte[] get_data_fixed(int data_size)
        {
        }
        public int    get_index => index;
        public string get_path  => path;
        public Chunk(byte[] data_,string path_, int index_)
        {
            this.data = data_;
            this.path = path_;
            this.index = index_;
        }
        /// <summary>
        /// 파일 읽은거 그대로 전달하고 
        /// 실제 data size 와 chunk size 입력받기 data_size 없을경우 그냥 chunk size 인줄 알아라(파일제외 전부 그거니까)
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="data_size"></param>
        /// <param name="chunk_size"></param>
        /// <exception cref="Exception"></exception>
        public Chunk(byte[] packet, int chunk_size, int data_size = -1 )
        {
            if (data_size == -1)
                data_size = chunk_size;
            // 데이터 부분만 저장
            data = new byte[chunk_size];
            Array.Copy(packet, 0, data, 0, data_size);
            // 헤더 부분 저장 (남은 부분을 헤더로)
            int header_size = packet.Length - data_size;
            if (header_size > 0)
            {
                byte[] headerData = new byte[header_size];
                Array.Copy(packet, data_size, headerData, 0, header_size);
                set_header(headerData);
            }
            else
            {
                throw new Exception("Chunk Packet header is not exists");
            }
        }
        public override string ToString()
        {
            return Encoding.UTF8.GetString(data)
                + get_header2string;
        }
        public string get_header2string
        {
            get{
                return
                path.ToString() + ":" +
                index.ToString();
            }
        }
        public byte[] get_header2byte
        {
            get{
                return Encoding.UTF8.GetBytes(
                    get_header2string);
            }
        }
        public void set_header(byte[] header_data)
        {
            string header_string = Encoding.UTF8.GetString(header_data);
            set_header(header_string);
        }
        public void set_header(string header_data)
        {
            string[] parts = header_data.Split(':');
            if (parts.Length == 2)
            {
                this.path = parts[0];
                if (int.TryParse(parts[1], out int parsedIndex))
                {
                    this.index = parsedIndex;
                }
                else
                    throw new FormatException("Invalid index format in header data.");
            }
            else
            {
                throw new FormatException("Invalid header format. Expected 'path|index'.");
            }
        }
        public byte[] get_byte
        {
            get
            {
                byte[] headerBytes = get_header2byte;
                byte[] result = new byte[headerBytes.Length + data.Length];
                // 먼저 data를 앞쪽에 복사
                Array.Copy(data, 0, result, 0, data.Length);
                // 그 다음 headerBytes를 뒤쪽에 복사
                Array.Copy(headerBytes, 0, result, data.Length, headerBytes.Length);
                return result;
            }
        }
        public byte[] get_byte_fixed(int fixed_size)
        {

            byte[] headerBytes = get_header2byte;
            byte[] data = get_byte_fixed(fixed_size);
            byte[] result = new byte[headerBytes.Length + data.Length];
            // 먼저 data를 앞쪽에 복사
            Array.Copy(data, 0, result, 0, data.Length);
            // 그 다음 headerBytes를 뒤쪽에 복사
            Array.Copy(headerBytes, 0, result, data.Length, headerBytes.Length);
            return result;

        }
    }
}
