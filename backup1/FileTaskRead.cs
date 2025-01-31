using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class FileTaskRead :FileTask
    {

        public Chunk h_read_file()
        {
            byte[] buffer = new byte[backup.get_setting.get_chunk_size];
            lock (stream)
            {
                for (int i = 0; i < backup.get_setting.get_chunk_size; i++)
                {
                    byte b = (byte)stream.ReadByte();
                }
            }
            Chunk chunk = new Chunk(buffer, get_relative_path, index);
            return chunk;
        }
        public void h_read_setting()
        {

        }
        public Chunk h_read_all_chunk()
        {
            for (int i = 0; i < backup.get_setting.get_chunk_size; i++)
            {

            }
            
        }
        public Chunk h_read_chunk()
        {

        }
        public FileTaskRead(FileInfo file_info, Backup bu)
        {
            n_file_info = file_info;
            n_index = 0;
            n_backup = bu;
            n_end = false;
        }
    }
}
