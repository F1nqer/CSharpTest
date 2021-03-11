using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopStorage.StorageClasses;

namespace OopStorage
{
    class Storage
    {
        string Address;
        int Square;
        Employee MainEmployee;
        List<IProduct> Products = new List<IProduct>();
        bool open;
        IProduct Error = new OverallProduct("ERROR", "ERROR", 000, 0);

        public string AddProduct(IProduct adding)
        {
            if (adding.Type == "Dry")
            {
                if (open == true)
                {
                    Products.Add(adding);
                }
            }
            else {
                return "Storage is closed";
            }
            Products.Add(adding);
            return "Product was added";
        }

        public string MoveProduct(IProduct moving, int count, Storage where)
        {
            List<IProduct> finder = Products.FindAll(x => x == moving);
            if (finder.Count() < count)
            {
                return $"This storage have only {finder.Count()} {moving.Unit} of product";
            }
            else
            {
                foreach (IProduct i in finder)
                {
                    where.AddProduct(i);
                    Products.Remove(i);
                }
                return "Product is moved";
            }
        }

        public IProduct FindProduct(int sku)
        {
            foreach(IProduct i in Products)
            {
                if(i.SKU == sku)
                {
                    return i;
                }
            }
            return Error;
        }

        public decimal PriceSum()
        {
            return Products.Sum(x => x.Price);
        }

        public string ChangeMainEmployee(Employee changing)
        {
            MainEmployee = changing;
            return "Employee was changing";
        }
        public Storage(string Address, int Square, Employee MainEmployee, bool open )
        {
            this.Address = Address;
            this.Square = Square;
            this.MainEmployee = MainEmployee;
            this.open = open;
        }
    }
}
