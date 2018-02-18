using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;

namespace RefWork.Model
{
    public class TegCounterExceprion : Exception
    {
        public TegCounterExceprion(string message) : base(message)
        {
        }
        public TegCounterExceprion(string message, Exception e) : base(message, e)
        {
        }
    }

    public class TegCounter : ITegCounter
    {
        public IEnumerable<string> GetTegs(string content, string tegName)
        {
            if (string.IsNullOrEmpty(tegName))
                throw new ArgumentException("tegName or content is null or empty");
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var nodes = doc.DocumentNode.SelectNodes("//" + tegName);
            return nodes.Select(x => x.ToString());
        }
    }
}
