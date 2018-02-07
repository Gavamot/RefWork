using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefWork.Model
{
    interface ILogger
    {
        void LogInfo(string msg);
        void LogError(string msg);
    }
}
