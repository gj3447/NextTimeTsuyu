using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class FileTask
    {
        protected FileStream stream;
        protected string path; // 상대경로
        protected Backup backup;


        public virtual string get_relative_path
        { get; }
        public virtual string get_full_path
        { get; }
        protected int index = 0;

        protected bool is_start = false;
        protected bool is_end = false ;
        public virtual FILETASK_STATE get_state
        {
            get
            {
                if (is_end) return FILETASK_STATE.END;
                if (is_start) return FILETASK_STATE.START;
                if (stream == null) 
                { return FILETASK_STATE.START; }
                else
                {
                    return FILETASK_STATE.WORKING;
                }

            }
        }
        public virtual bool h_start()
        {
            if (is_start)
            {
                throw new Exception();
                return false;
            }
            if (stream != null)
            {
                throw new Exception(); 
                return false;
            }
            is_start = true;
            return true;
        }
        public virtual bool h_end()
        {
            if(is_end)
            {
                throw new Exception();
                return false;
            }
            if(stream != null)
            {
                throw new Exception();
                return false;
            }

            return true;
        }
    }

    public enum FILETASK_STATE
    {
        WAITING,
        START,
        WORKING,
        STOP,
        END,
    }
}
