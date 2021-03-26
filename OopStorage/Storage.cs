using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopStorage.StorageClasses;
using OopStorage.Exceptions;

namespace OopStorage
{
    public class Storage
    {
        public Address Address;
        int Square;
        Employee MainEmployee;
        public List<IProduct> Products = new List<IProduct>();
        public bool open;
        //это вывод при отсутствии элемента при поиске товара, 
        //нам же просто запрещено использовать вывод на консоль внутри классов
        IProduct Error = new OverallProduct("ERROR", "ERROR", 000, 0);

        public delegate void GoodPut(object sender, StorageEventArgs args);
        public event GoodPut NotifyGood;
        public delegate void BadPut(object sender, StorageEventArgs args);
        public event BadPut NotifyBad;

        public void AddProduct(IProduct helper, decimal Count)
        {
            IProduct adding = (IProduct)helper.Clone();

            if (adding.Type == "Dry")
            {
                if (open == true)
                {
                    if (Products.Contains(adding))
                    {
                        for (int i = 0; i < Products.Count(); i++)
                        {
                            if (Products[i].SKU == adding.SKU)
                            {
                                Products[i].Count += Count;
                            }
                        }
                        
                        if (NotifyGood!= null)
                        {
                            NotifyGood(this, new StorageEventArgs($"Product {helper.Name} was added {helper.Count}", NotifyGood.GetType().Name, this.Address, DateTime.Now, helper));
                        }
                    }
                    else
                    {
                        adding.Count = Count;
                        Products.Add(adding);
                        if (NotifyGood != null)
                        {
                            NotifyGood(this, new StorageEventArgs($"Product {helper.Name} was added COUNT: {helper.Count}", NotifyGood.GetType().Name, this.Address, DateTime.Now, helper));
                        }
                    }
                }
                else
                {
                    if (NotifyBad != null)
                    {
                        NotifyBad(this, new StorageEventArgs($"Can't add dry product {helper.Name} into closed storage", NotifyBad.GetType().Name, this.Address, DateTime.Now, helper));
                    }
                    throw new DryProductException("Can't add dry product into closed storage");
                }
            }
            else
            {
                if (Products.Contains(adding))
                {
                    for(int i = 0; i < Products.Count(); i++)
                        {
                        if (Products[i].SKU == adding.SKU)
                        {
                            Products[i].Count += Count;
                        }
                    }
                    if (NotifyGood != null)
                    {
                        NotifyGood(this, new StorageEventArgs($"Product {helper.Name} was added {helper.Count}", NotifyGood.GetType().Name, this.Address, DateTime.Now, helper));
                    }
                }
                else
                {
                    adding.Count = Count;
                    Products.Add(adding);
                    if (NotifyGood != null)
                    {
                        NotifyGood(this, new StorageEventArgs($"Product {helper.Name} was added {helper.Count}", NotifyGood.GetType().Name, this.Address, DateTime.Now, helper));
                    }
                }
            }
            
        }

        public string MoveProduct(IProduct moving, int count, Storage where)
        {
            // находим все продукты с таким именем
            var finder = Products.Find(x => x.SKU == moving.SKU);
            //если количества не хватает, то даём об этом знать
            if (finder.Count < count)
            {
                return $"This storage have only {finder.Count} {moving.Unit} of product";
            }
            //если количество есть, то добавляем этот товар в другой склад и удаляем у себя
            else
            {
                where.AddProduct(moving, count);
                for (int i = 0; i < Products.Count(); i++)
                {
                    if (Products[i].SKU == moving.SKU)
                    {
                        Products[i].Count -= count;
                    }
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
