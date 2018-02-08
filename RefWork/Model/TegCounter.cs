using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;

namespace RefWork.Model
{
    public class TegCounter : ITegCounter
    {
        public int? CountTegs(string content, string tegName)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
        
            var nodes = doc.DocumentNode.SelectNodes("//" + tegName);
            return nodes?.Count;
        }
    }
}
