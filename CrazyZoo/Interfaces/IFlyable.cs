﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Interfaces
{
    public interface IFlyable
    {
        void Fly(Action<string> log);
    }
}
