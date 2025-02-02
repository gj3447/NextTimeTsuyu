using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{
    // 읽기 작업: 파일의 어느 위치부터 얼마만큼 읽을지를 지정
    public class ReadTask
    {
        public string FilePath { get; set; }
        public long Offset { get; set; }
        public int ChunkSize { get; set; }
    }
}
