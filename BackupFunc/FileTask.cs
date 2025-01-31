﻿using System;
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
