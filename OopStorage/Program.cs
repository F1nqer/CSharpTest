using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopStorage.StorageClasses;
using OopStorage.StorageClasses.StorageExtensions;

namespace OopStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            int ReportsNum = 1;
            Dictionary<int, string> StorageProducts = new Dictionary<int, string>(); 
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

            //Добавляем обработчик действий
            // была проверка с подпиской, без подписки, с двумя разными обработчиками
            //ForAll.NotifyBad += DisplayMessage;
            ForAll.NotifyGood += DisplayMessage;
            ForAll.NotifyBad += ShowInfo;
            ForAll2.NotifyGood += DisplayMessage;
            ForAll2.NotifyBad += ShowInfo;
            ForDry.NotifyGood += DisplayMessage;
            ForDry.NotifyBad += ShowInfo;
            //ForAll.NotifyGood += ShowInfo;

            //Рандомно генерирую СКЮ
            int SKUCocaCola = 1949390;
            int SKUCar = 2312312;
            int SKUDry = 5949494;
            int SKUPeace = 4930323;

            //Заполняю склады товарами
            IProduct CocaCola = new LiquidProduct("CocaCola", "The best liquid", 200, SKUCocaCola);
            ForAll.AddProduct(CocaCola, 10);
            ForAll.AddProduct(CocaCola, 10);
            //2000 CocaCola
            IProduct Car = new OverallProduct("TheBestCar", "Good good car", 2000, SKUCar);
            ForAll.AddProduct(Car, 2);
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

            StorageProducts = ForAll.StorageSKU();
            ForAll.TwoStorageProducts(ForAll2);
            foreach (KeyValuePair<int, string> i in StorageProducts)
            {
                Console.WriteLine(i);
            }
            ForAll2.OtherStorageHelp(ForAll);
            ForAll.TwoStorageProducts(ForAll2);
            
            foreach (KeyValuePair<int, string> i in StorageProducts)
            {
                Console.WriteLine(i);
            }
            //Сейчас для примера выведу, что не покажет функция добавления товара,
            //если я в закрытый склад попытаюсь добавить сыпучий товар
            //Console.WriteLine("Dry product can't add in this Storage! I'm adding Dry product into closed storage");
            try
            {
                IProduct Dry1 = new DryProduct("DryProduct", "The driest product", 200, SKUDry);
                ForAll.AddProduct(Dry1, 100);

            }
            catch(Exception dry)    
            {
                Console.WriteLine(dry.Message);
            }
            finally
            {
                Console.WriteLine("finally test!");

            }

            //Высчитываю сумму цен во всех складах

            decimal summ = ForAll.PriceSum() + ForAll2.PriceSum() + ForDry.PriceSum();
            Console.WriteLine($"Summ: {summ}");



            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("");
            }

            Console.WriteLine("Reports");

            List<Storage> Storages = new List<Storage> { ForAll, ForAll2, ForDry };

           /* ForAll.AddProduct(CocaCola, 10);
            ForAll.AddProduct(Car, 2);
            ForAll.AddProduct(ChupaChups, 1000);
          
            ForAll.AddProduct(CocaCola, 10);*/

            List<IProduct> Report1 = Reports.Distinct(ForAll);
            List<IProduct> Report2 = Reports.FirstBiggerThree(ForAll2); 
            List<IProduct> Report3 = Reports.LessThanThree(ForAll);
            List<Storage> Report4 = Reports.WithoutDryStorages(Storages);

            List<List<IProduct>> ListofLists = new List<List<IProduct>> { Report1, Report2, Report3 };
            foreach(List<IProduct> i in ListofLists)
            {
                Console.WriteLine($"Report {ReportsNum}!");
                foreach(IProduct j in i)
                {
                    Console.WriteLine(j.Name);
                    Console.WriteLine(j.SKU);
                    Console.WriteLine(j.Count);
                }
                Console.WriteLine("End of report!");
                ReportsNum++;
            }

            Console.WriteLine("Report 4!");
            foreach (Storage i in Report4)
            {
                Console.WriteLine(i.Products.Count);
            }

            Console.ReadKey();

        }
        private static void DisplayMessage(object sender, StorageEventArgs args)
        {
            Console.WriteLine($"Returned storage event: {args.Type}");
            Console.WriteLine(args.Message);
        }

        private static void ShowInfo(object sender, StorageEventArgs args)
        {
            Console.WriteLine($"All args: {args.Now.ToString()}, {args.Message}, {args.Adrs.City}, {args.Product.Name}");
        }

        
    }
}
