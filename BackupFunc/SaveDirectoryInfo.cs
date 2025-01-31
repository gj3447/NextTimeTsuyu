using System;
using System.Collections.Generic;
using System.IO;
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
            return $"{SETTING_TYPE.directory.ToString()}:{path}";
        }
        public SaveDirectoryInfo(string path_)
        {
            this.path = path_;
        }
    }
}
