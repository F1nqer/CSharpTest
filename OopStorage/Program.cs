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
            //Создаём Адреса
            Address First = new Address("Almaty", "Lermontova", 47);
            Address Second = new Address("Taraz", "Lermontova", 47, 10, 20);
            Address Third = new Address("Talgar", "Lermontova", 47);
            //Создаём склады и назначем сотрудников
            Storage ForAll = new Storage(First, 2000, Arystan, false);
            Storage ForDry = new Storage(Second, 3000, Diana, true);
            Storage ForAll2 = new Storage(Third, 1000, Zhangir, false);
            //Рандомно генерирую СКЮ
            int SKUCocaCola = 1949390;
            int SKUCar = 2312312;
            int SKUDry = 5949494;
            int SKUPeace = 4930323;

            //Заполняю склады товарами
            IProduct CocaCola = new LiquidProduct("CocaCola", "The best liquid", 200, SKUCocaCola);
            ForAll.AddProduct(CocaCola, 10);
            //2000 CocaCola
            IProduct Car = new OverallProduct("TheBestCar", "Good good car", 2000, SKUCar);
            ForAll.AddProduct(Car, 20);
            //40000 Cars
            IProduct Dry = new DryProduct("DryProduct", "The driest product", 200, SKUDry);
            ForDry.AddProduct(Dry, 8);
           //1600 Dry
            IProduct ChupaChups = new PeaceProduct("ChupaChups", "The best candy", 24, SKUPeace);
            ForAll2.AddProduct(ChupaChups, 100);
            //Второе добавление чупачупсов для проверки работы клонирования обьектов класса
            ForAll2.AddProduct(ChupaChups, 100);
            //2400 Chupa chups
            //SUM is 46000
            //Провожу поиск товара по СКЮ и вывожу информацию по товару
            Console.WriteLine("Find product in ForAll Storage");
            IProduct Find = ForAll.FindProduct(SKUCocaCola);
            Console.WriteLine($"{Find.Definition} is {Find.Name} {Find.Count} {Find.Unit}with SKU: {Find.SKU}");

            Console.WriteLine("Find product in ForAll Storage");
            Find = ForAll.FindProduct(SKUCar);
            Console.WriteLine($"{Find.Definition} is {Find.Name} {Find.Count} {Find.Unit}with SKU: {Find.SKU}");

            Console.WriteLine("Find product in ForAll2 Storage");
            Find = ForAll2.FindProduct(SKUPeace);
            Console.WriteLine($"{Find.Definition} is {Find.Name} {Find.Count} {Find.Unit} with SKU: {Find.SKU}");

            Console.WriteLine("Find product in ForDry Storage");
            Find = ForDry.FindProduct(SKUDry);
            Console.WriteLine($"{Find.Definition} is {Find.Name} {Find.Count} {Find.Unit} with SKU: {Find.SKU}");

            //Сейчас для примера выведу, что не покажет функция добавления товара,
            //если я в закрытый склад попытаюсь добавить сыпучий товар
            Console.WriteLine("Dry product can't add in this Storage! I'm adding Dry product into closed storage");
            IProduct Dry1 = new DryProduct("DryProduct", "The driest product", 200, SKUDry);
            string DryTest = ForAll.AddProduct(Dry1, 100);
            Console.WriteLine(DryTest);

            //Высчитываю сумму цен во всех складах

            decimal summ = ForAll.PriceSum() + ForAll2.PriceSum() + ForDry.PriceSum();
            Console.WriteLine($"Summ: {summ}");

            Console.ReadKey();

        }
    }

 
}
