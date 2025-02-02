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
        protected int index = 0;
        protected int remainder = int.MaxValue;
        protected bool is_start = false;
        protected bool is_end = false;
        public virtual string get_path => path;
        public virtual int get_index => index;
        public virtual int get_remainder => remainder;
        public virtual FILETASK_STATE get_state
        {
            get
            {
                if (is_end) return FILETASK_STATE.END;
                if (!is_start) return FILETASK_STATE.START;
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
            }
            if (stream != null)
            {
                throw new Exception(); 
            }
            is_start = true;
            return true;
        }
        public virtual bool h_end()
        {
            if(is_end)throw new Exception();
            if(stream == null)throw new Exception();
            lock (stream)
            {
                stream.Close();
                stream = null;
            }
            is_end = true;
            return true;
        }
        public FileTask()
        {
            is_start = false;
            is_end = false;
            index = 0;
        }
        public static WORKABLE_STATE filetask_state2workable_state(FILETASK_STATE fs)
        {
            switch (fs)
            {
                case FILETASK_STATE.WAITING: return WORKABLE_STATE.WATING;
                case FILETASK_STATE.START: return WORKABLE_STATE.START;
                case FILETASK_STATE.WORKING: return WORKABLE_STATE.WORKING;
                case FILETASK_STATE.STOP: return WORKABLE_STATE.STOP;
                case FILETASK_STATE.END: return WORKABLE_STATE.END;
                default: return WORKABLE_STATE.END;
            }
        }
        public static FILETASK_STATE workable_state2filetask_state(WORKABLE_STATE ws)
        {
            switch (ws)
            {
                case WORKABLE_STATE.WATING: return FILETASK_STATE.WAITING;
                case WORKABLE_STATE.START: return FILETASK_STATE.START;
                case WORKABLE_STATE.WORKING: return FILETASK_STATE.WORKING;
                case WORKABLE_STATE.STOP: return FILETASK_STATE.STOP;
                case WORKABLE_STATE.END: return FILETASK_STATE.END;
                default: return FILETASK_STATE.END;
            }
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
