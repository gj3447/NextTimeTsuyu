using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{ 
    // 쓰기 작업: 대상 파일의 어느 위치에 어떤 데이터를 쓸지 지정
    public class WriteTask
    {
        public string DestinationFilePath { get; set; }
        public long Offset { get; set; }
        public byte[] Data { get; set; }
    }
}
