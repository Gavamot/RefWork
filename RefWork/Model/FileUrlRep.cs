using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RefWork.Model
{
    /// <summary>
    /// Read url from file
    /// </summary>
    class FileUrlRep : IUrlRep
    {
        public string fileName { get; set; }
        public ILogger logger { get; set; }

        public FileUrlRep(ILogger logger)
        {
            this.logger = logger;
        }

        public IEnumerable<Uri> GetUris()
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (sr.Peek() >= 0)
                {
                    string str = sr.ReadLine();
                    if(string.IsNullOrEmpty(str))
                        continue;
                    Uri uri = null;
                    try
                    {
                        uri = new Uri(str);
                    }
                    catch (UriFormatException e)
                    {
                        logger.LogError($"Uri Format Exception - {str}");
                    }
                    yield return uri;
                }
            }
        }

    }
}
