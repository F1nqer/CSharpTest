﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses
{
    interface IProduct : ICloneable
        //добавляю возможность клонирования, т.к. так проще жить
    {
        string Name { get; set; }
        int SKU { get; set; }
        string Definition { get; set; }
        decimal Price { get; set; }
        int Count { get; set; }
        string Type { get; set; }
        string Unit { get; set; }

    }
}
