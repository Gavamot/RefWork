using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefWork.Model
{
    class Logger : ILogger
    {
        public event EventHandler<string> OnLogInfoAct;
        public event EventHandler<string> OnLogErrorAct;

        public void LogInfo(string msg)
        {
            OnLogInfoAct?.Invoke(this, msg);
        }

        public void LogError(string msg)
        {
            OnLogErrorAct?.Invoke(this, msg);
        }
    }
}
