using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RefWork.Model.Interface;

namespace RefWork.Model
{
    class TegCounter : ITegCounter
    {
        public int CountTegs(XmlDocument doc, string tegName)
        {
            return doc.GetElementsByTagName(tegName).Count;
        }
    }
}
