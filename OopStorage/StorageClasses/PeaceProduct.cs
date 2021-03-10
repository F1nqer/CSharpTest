using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses
{
    class PeaceProduct : Product
    {
        public string Name { get; set; }
        public int SKU { get; set; }
        public string Definition { get; set; }
        public int Price { get; set; }
    }
}
