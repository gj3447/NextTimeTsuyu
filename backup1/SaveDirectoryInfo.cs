using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class SaveDirectoryInfo
    {
        string path;//상대경로
        public override string ToString()
        {
            return $"directory:{path}";
        }
    }
}
