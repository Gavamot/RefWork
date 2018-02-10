using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RefWork.Model
{
    public class WebSiteLoaderExceprion : Exception
    {
        public WebSiteLoaderExceprion(string message, Exception e) : base(message, e)
        {
        }
    }

    public class WebSiteLoader : ISiteLoader
    {
        public string GetContent(Uri url)
        {
            try
            {
                var client = new WebClient();
                string res = client.DownloadString(url);
                return res;
            }catch(Exception e)
            {
                throw new WebSiteLoaderExceprion($"({url.AbsoluteUri}) Could not get a information by a url", e);
            }
        }
    }
}
