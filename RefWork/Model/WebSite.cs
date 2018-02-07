using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace RefWork.Model
{
    class WebSite : ISiteLoader
    {
        public XmlDocument GetDom(Uri url)
        {
            var client = new WebClient();
            string str = client.DownloadString(url);
            var res = new XmlDocument();
            res.Load(str);
            return res;
        }
    }
}
