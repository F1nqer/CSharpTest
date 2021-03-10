using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses
{
    interface Product
    {
        string Name { get; set; }
        int SKU { get; set; }
        string Definition { get; set; }
        int Price { get; set; }

    }
}
