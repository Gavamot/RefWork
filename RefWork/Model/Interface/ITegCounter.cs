﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RefWork.Model
{
    public interface ITegCounter
    {
        IEnumerable<string> GetTegs(string content, string tegName);
    }
}