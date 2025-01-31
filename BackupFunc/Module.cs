using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class Module
    {
        protected Backup n_backup;
        protected bool n_is_start;
        public Module(Backup bu) 
        {
            n_backup = bu;
            n_is_start = false;
        }
    }
}
