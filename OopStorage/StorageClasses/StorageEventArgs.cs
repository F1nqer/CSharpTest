using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses
{
    class StorageEventArgs
    {
        public string Message { get; }
        public string Type { get; }
        public Address Adrs { get; }
        public DateTime Now { get; }
        public IProduct Product { get; }


        public StorageEventArgs(string mes, string type, Address adrs, DateTime now, IProduct product)
        {
            Message = mes;
            Type = type;
            Adrs = adrs;
            Now = now;
            Product = product;
        }
    }
}
