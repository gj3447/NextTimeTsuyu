using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public interface IWorkable
    {
        Action get_work();
        int get_priority();
        WORKABLE_STATE get_state();
    }
    public enum WORKABLE_STATE
    {
        WATING,
        START,
        WORKING,
        STOP,
        END,
    }
}
