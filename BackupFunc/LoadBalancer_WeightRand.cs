﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupFunc
{
    public class LoadBalancer_WeightRand : LoadBalancer
    {
        public LoadBalancer_WeightRand(Backup backup) : base(backup)
        {
        }
    }
}
