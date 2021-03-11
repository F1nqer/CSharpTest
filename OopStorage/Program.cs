using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopStorage.StorageClasses;

namespace OopStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee Arystan = new Employee("Arystan", "Engineer");
            Employee Diana = new Employee("Diana", "Director");
            Employee Zhangir = new Employee("Zhangir", "HR");
            Employee Chingiz = new Employee("Chingiz", "Manager");

            Storage ForAll = new Storage("Almaty", 2000, Arystan, false);
            Storage ForDry = new Storage("Taraz", 3000, Diana, true);
            Storage ForAll2 = new Storage("Temirtau", 1000, Zhangir, false);

            int SKUCocaCola = 1949390;
            int SKUCar = 2312312;
            int SKUDry = 5949494;
            int SKUPeace = 4930323;


            for(int i = 0; i<10; i++)
            {
                SKUCocaCola++;
                IProduct CocaCola = new LiquidProduct("CocaCola", "The best liquid", 200, SKUCocaCola);
                ForAll.AddProduct(CocaCola);
            }
            for (int i = 0; i < 10; i++)
            {
                SKUCar++;
                IProduct Car = new OverallProduct("TheBestCar", "Good good car", 200000, SKUCar);
                ForAll.AddProduct(Car);
            }
            for (int i = 0; i < 10; i++)
            {
                SKUDry++;
                IProduct Dry = new DryProduct("DryProduct", "The driest product", 200, SKUDry);
                ForDry.AddProduct(Dry);
            }
            for (int i = 0; i < 10; i++)
            {
                SKUPeace++;
                IProduct ChupaChups = new DryProduct("ChupaChups", "The best candy", 24, SKUPeace);
                ForAll2.AddProduct(ChupaChups);
            }

            IProduct Find = ForAll.FindProduct(2312315);
            Console.WriteLine($"{Find.Definition} is {Find.Name} with SKU: {Find.SKU}");
            Find = ForAll2.FindProduct(4930325);
            Console.WriteLine($"{Find.Definition} is {Find.Name} with SKU: {Find.SKU}");
            Find = ForDry.FindProduct(5949496);
            Console.WriteLine($"{Find.Definition} is {Find.Name} with SKU: {Find.SKU}");

            decimal summ = ForAll.PriceSum() + ForAll2.PriceSum() + ForDry.PriceSum();
            Console.WriteLine(summ);

            Console.ReadKey();

        }
    }

 
}
