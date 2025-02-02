using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunco4
{
    /// <summary>
     /// ModuleCluster는 MemoryCluster를 참조하여 각 모듈(Search, Read, Write)을 보관함
     /// </summary>
    public class ModuleCluster
    {
        public SearchModule SearchModule { get; }
        public ReadModule ReadModule { get; }
        public WriteModule WriteModule { get; }

        public ModuleCluster(MemoryCluster memory)
        {
            SearchModule = new SearchModule(memory);
            ReadModule = new ReadModule(memory);
            WriteModule = new WriteModule(memory);
        }
    }
}
