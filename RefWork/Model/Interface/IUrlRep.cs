using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefWork.Model
{
    public interface IUrlRep
    {
        IEnumerable<Uri> GetUris(string parameter);
    }
}
