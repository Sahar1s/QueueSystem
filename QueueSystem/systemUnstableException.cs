﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSystem
{
    public class SystemUnstableException:Exception
    {
        public SystemUnstableException(string msg):base(msg) { }
     
    }
}
