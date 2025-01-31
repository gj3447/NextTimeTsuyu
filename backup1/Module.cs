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
        public Module(Backup bu) 
        {
            n_backup = bu;
        }
    }
}
