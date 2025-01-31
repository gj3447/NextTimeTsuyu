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
        int index_remainder;
        public string get_path => path;
        public int get_index_max => index_max;
        public int get_index_remainer => index_remainder;

        public int set_index_max
        { set { index_max = value; } }
        public int set_index_remainder
        { set { index_remainder = value; } }

        public override string ToString()
        {
            return $"{SETTING_TYPE.file.ToString()}:{path}|{index_max.ToString()}|{index_remainder.ToString()}";
        }
        public SaveFileInfo(string path_)
        {
            this.path = path_;
        }
        public SaveFileInfo(string path_, int index_max_, int remainder_)
        {
            this.path = path_;
            this.index_max = index_max_;
            this.index_remainder = remainder_;
        }
    }
}
