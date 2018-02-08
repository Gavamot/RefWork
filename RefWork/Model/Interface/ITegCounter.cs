using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RefWork.Model
{
    public interface ITegCounter
    {
        int? CountTegs(string content, string tegName);
    }
}
