using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.Exceptions
{
    class DryProductException : Exception
    {
        public DryProductException(string message):base(message)
        {
        }
    }
}
