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
        private byte[] data;
        private string path;
        private int index;
        public byte[] get_data  => data;
        public int    get_index => index;
        public string get_path  => path;
        public Chunk(byte[] data_,string path_, int index_)
        {
            this.data = data_;
            this.path = path_;
            this.index = index_;
        }
        public Chunk(byte[] packet, int chunk_size )
        {
            // 데이터 부분만 저장
            data = new byte[chunk_size];
            Array.Copy(packet, 0, data, 0, chunk_size);
            // 헤더 부분 저장 (남은 부분을 헤더로)
            int headerSize = packet.Length - chunk_size;
            if (headerSize > 0)
            {
                byte[] headerData = new byte[headerSize];
                Array.Copy(packet, chunk_size, headerData, 0, headerSize);
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
                + HeaderToString();
        }
        public string HeaderToString()
        {
            return 
            path.ToString()+":"+
            index.ToString();
        }
        public byte[] HeaderToByte()
        {
            return Encoding.UTF8.GetBytes(
                HeaderToString());
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
                {
                    throw new FormatException("Invalid index format in header data.");
                }
            }
            else
            {
                throw new FormatException("Invalid header format. Expected 'path|index'.");
            }
        }
        public byte[] ToBytes()
        {
            byte[] headerBytes = HeaderToByte();
            byte[] result = new byte[headerBytes.Length + data.Length];
            Array.Copy(headerBytes, 0, result, 0, headerBytes.Length);
            Array.Copy(data, 0, result, headerBytes.Length, data.Length);
            return result;
        }
    }
}
