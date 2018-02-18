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
        public int CountTegs(string content, string tegName)
        {
            if (content == null)
                throw new TegCounterExceprion($"TegCounter agrument - content is null or empty");
            if(string.IsNullOrEmpty(tegName))
                throw new TegCounterExceprion($"TegCounter agrument - tegName is null or empty");
            if (content == string.Empty)
                return 0;
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var nodes = doc.DocumentNode.SelectNodes("//" + tegName);
                return nodes == null ? 0 : nodes.Count;
            }catch(Exception e)
            {
                throw new TegCounterExceprion($"TegCounter can not count nodes", e);
            }
        }
    }
}
