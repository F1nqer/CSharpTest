using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopStorage.StorageClasses;
using OopStorage.Exceptions;

namespace OopStorage
{
    class Storage
    {
        Address Address;
        int Square;
        Employee MainEmployee;
        List<IProduct> Products = new List<IProduct>();
        bool open;
        //это вывод при отсутствии элемента при поиске товара, 
        //нам же просто запрещено использовать вывод на консоль внутри классов
        IProduct Error = new OverallProduct("ERROR", "ERROR", 000, 0); 
        

        public string AddProduct(IProduct helper, int Count)
        {
            IProduct adding = (IProduct)helper.Clone();

            if (adding.Type == "Dry")
            {
                if (open == true)
                {
                    if (Products.Contains(adding))
                    {
                        Products.Find(x => x == adding).Count += Count;
                        return "Product was added";
                    }
                    else
                    {
                        adding.Count = Count;
                        Products.Add(adding);
                        return "Product was added";
                    }
                }
                else
                {
                    throw new DryProductException("Can't add dry product into closed storage");
                }
            }
            else
            {
                if (Products.Contains(adding))
                {
                    Products.Find(x => x == adding).Count += Count;
                    return "Product was added";
                }
                else
                {
                    adding.Count = Count;
                    Products.Add(adding);
                    return "Product was added";
                }
            }
            
        }

        public string MoveProduct(IProduct moving, int count, Storage where)
        {
            // находим все продукты с таким именем
            var finder = Products.Find(x => x.Name == moving.Name);
            //если количества не хватает, то даём об этом знать
            if (finder.Count < count)
            {
                return $"This storage have only {finder.Count} {moving.Unit} of product";
            }
            //если количество есть, то добавляем этот товар в другой склад и удаляем у себя
            else
            {
                where.AddProduct(moving, count);
                Products.Find(x => x == moving).Count -= count;
            
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
            return Products.Sum(x => x.Price*x.Count);
            //почему-то не пашет 0_о
            //а вру, всё тут пашет, и нижняя, и верхняя
            /*decimal sum = 0;
            foreach(IProduct i in Products)
            {
                sum += i.Price;
            }
            return sum;*/
        }

        public string ChangeMainEmployee(Employee changing)
        {
            MainEmployee = changing;
            return "Employee was changing";
        }
        public Storage(Address Address, int Square, Employee MainEmployee, bool open )
        {
            this.Address = Address;
            this.Square = Square;
            this.MainEmployee = MainEmployee;
            this.open = open;
        }
    }
}
