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
    public class FileUrlRep : IUrlRep
    {
        public FileUrlRep()
        {

        }

        public IEnumerable<Uri> GetUris(string parameter)
        {
            using (StreamReader sr = new StreamReader(parameter))
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

                    }
                    yield return uri;
                }
            }
        }

    }
}
