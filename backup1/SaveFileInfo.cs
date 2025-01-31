using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class SaveFileInfo
    {
        string path;//상대경로
        int index_max;
        int index_remain;
        public override string ToString()
        {
            return $"file:{path}|{index_max}|{index_remain}";
        }
    }
}
