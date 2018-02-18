using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefWork.ViewModel.Class
{
    public class ValueWrapper<T>
    {
        public T Value { get; set; }

        public ValueWrapper(T value)
        {
            Value = value;
        }
    }
}
