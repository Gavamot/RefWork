using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace RefWork.Model
{
    public class WebSite : ISiteLoader
    {
        public string GetContent(Uri url)
        {
            var client = new WebClient();
            string res = client.DownloadString(url);
            return res;
        }
    }
}
