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
            //Создаём сотрудников
            Employee Arystan = new Employee("Arystan", "Engineer");
            Employee Diana = new Employee("Diana", "Director");
            Employee Zhangir = new Employee("Zhangir", "HR");
            Employee Chingiz = new Employee("Chingiz", "Manager");
            //Создаём склады и назначем сотрудников
            Storage ForAll = new Storage("Almaty", 2000, Arystan, false);
            Storage ForDry = new Storage("Taraz", 3000, Diana, true);
            Storage ForAll2 = new Storage("Temirtau", 1000, Zhangir, false);
            //Рандомно генерирую СКЮ
            int SKUCocaCola = 1949390;
            int SKUCar = 2312312;
            int SKUDry = 5949494;
            int SKUPeace = 4930323;

            //Заполняю склады товарами
            for(int i = 0; i<10; i++)
            {
                SKUCocaCola++;
                IProduct CocaCola = new LiquidProduct("CocaCola", "The best liquid", 200, SKUCocaCola);
                ForAll.AddProduct(CocaCola);
            }
            for (int i = 0; i < 10; i++)
            {
                SKUCar++;
                IProduct Car = new OverallProduct("TheBestCar", "Good good car", 2000, SKUCar);
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
                IProduct ChupaChups = new PeaceProduct("ChupaChups", "The best candy", 24, SKUPeace);
                ForAll2.AddProduct(ChupaChups);
            }

            //Провожу поиск товара по СКЮ и вывожу информацию по товару
            Console.WriteLine("Find product in ForAll Storage");
            IProduct Find = ForAll.FindProduct(2312313);
            Console.WriteLine($"{Find.Definition} is {Find.Name} with SKU: {Find.SKU}");

            Console.WriteLine("Find product in ForAll2 Storage");
            Find = ForAll2.FindProduct(4930325);
            Console.WriteLine($"{Find.Definition} is {Find.Name} with SKU: {Find.SKU}");

            Console.WriteLine("Find product in ForDry Storage");
            Find = ForDry.FindProduct(5949496);
            Console.WriteLine($"{Find.Definition} is {Find.Name} with SKU: {Find.SKU}");

            //Сейчас для примера выведу, что не покажет функция добавления товара,
            //если я в закрытый склад попытаюсь добавить сыпучий товар
            Console.WriteLine("Dry product can't add in this Storage! I'm adding Dry product into closed storage");
            IProduct Dry1 = new DryProduct("DryProduct", "The driest product", 200, SKUDry);
            string DryTest = ForAll.AddProduct(Dry1);
            Console.WriteLine(DryTest);

            //Высчитываю сумму цен во всех складах

            decimal summ = ForAll.PriceSum() + ForAll2.PriceSum() + ForDry.PriceSum();
            Console.WriteLine($"Summ: {summ}");

            Console.ReadKey();

        }
    }

 
}
