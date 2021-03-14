using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses
{
    class DryProduct : IProduct //добавляю возможность клонирования, т.к. так проще жить
    {
        public string Name { get; set; }
        public int SKU { get; set; }
        public string Definition { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } = "Dry";
        public string Unit { get; set; } = "Kilogram";
        public int Count { get; set; } = 0;
        //функция для клонирования
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public DryProduct(string Name, string Definition, int Price, int SKU)
        {
            this.Name = Name;
            this.Definition = Definition;
            this.Price = Price;
            this.SKU = SKU;
        }
    }
}
