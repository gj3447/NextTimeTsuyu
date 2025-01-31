using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    internal class SerachModuleLoad : SearchModule
    {
        public SerachModuleLoad(Backup bu) : base(bu)
        { }
        public override Action get_work()
        {
            return () =>
            {
                h_search_load(n_backup);
            };
        }
    }
}
