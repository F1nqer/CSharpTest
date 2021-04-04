using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopStorage.StorageClasses;
using OopStorage.StorageClasses.StorageExtensions;
using OopStorage.StorageClasses.Command;
using System.Threading;
using NLog;

namespace OopStorage
{
    internal class Program
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
           
        //receiver - Storage.cs
        //command - ICommand.cs
        //concrete command - Adding.cs
        //client - Employee.cs
        //invoker - WorkDay.cs
        Catalog CatalogTest = Catalog.GetInstance();
            Dictionary<int, string> StorageProducts = new Dictionary<int, string>();
            //Создаём сотрудников
            Employee Arystan = new Employee("Arystan", "Engineer");
            Employee Diana = new Employee("Diana", "Director");
            Employee Zhangir = new Employee("Zhangir", "Manager");
            //Создаём Адреса
            Address First = new Address("Almaty", "Lermontova", 47);
            Address Second = new Address("Taraz", "Lermontova", 47, 10, 20);
            Address Third = new Address("Talgar", "Lermontova", 47);
            //Создаём склады и назначем сотрудников
            Storage ForAll = new Storage(First, 2000, Arystan, false);
            ForAll.ChangeMainEmployee(Arystan, ForAll);
            Storage ForDry = new Storage(Second, 3000, Diana, true);
            ForDry.ChangeMainEmployee(Diana, ForDry);
            Storage ForAll2 = new Storage(Third, 1000, Zhangir, false);
            ForAll2.ChangeMainEmployee(Zhangir, ForAll2);

            List<Storage> Storages = new List<Storage> { ForAll, ForAll2, ForDry };
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;


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

            //Заполняю директоров задачами добавления
            IProduct CocaCola = CatalogTest.GetProduct(SKUCocaCola);
            IProduct Car = CatalogTest.GetProduct(SKUCar);
            IProduct Dry = CatalogTest.GetProduct(SKUDry);
            IProduct ChupaChups = CatalogTest.GetProduct(SKUPeace);

            Zhangir.AddingTask(ChupaChups, 100);
            Zhangir.AddingTask(ChupaChups, 100);
            Arystan.AddingTask(Car, 2);
            Diana.AddingTask(Dry, 2);
            Arystan.AddingTask(CocaCola, 10);
            Arystan.AddingTask(CocaCola, 10);

            WorkDay DianaDay = new WorkDay(Diana);
            WorkDay ZhangirDay = new WorkDay(Zhangir);
            WorkDay ArystanDay = new WorkDay(Arystan);
            DianaDay.AddCancelationToken(token);
            ZhangirDay.AddCancelationToken(token);
            ArystanDay.AddCancelationToken(token);

            Task ArystanStartTask = Task.Run(() => ArystanDay.StartDay());
            Task DianaStartTask = Task.Run(() => DianaDay.StartDay());
            Task ZhangirStartTask = Task.Run(() => ZhangirDay.StartDay());

            Task ArystanEndTask = ArystanStartTask.ContinueWith(ArystanDay.EndDay);
            Task DianaEndTask = DianaStartTask.ContinueWith(DianaDay.EndDay);
            Task ZhangirEndTask = ZhangirStartTask.ContinueWith(ZhangirDay.EndDay);

            Console.WriteLine("Введите Y для отмены операции или любой другой символ для ее продолжения:");
            string s = Console.ReadLine();
            if (s == "Y")
            {
                cancelTokenSource.Cancel();
            }
            else
            {
                Task.WaitAll();
            }
            //Extention-методы
            /*StorageProducts = ForAll.StorageSKU();
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
*/
            //Высчитываю сумму цен во всех складах

            decimal summ = ForAll.PriceSum() + ForAll2.PriceSum() + ForDry.PriceSum();
            logger.Debug($"Summ: {summ}");



            

            logger.Debug("Reports");

            Parallel.Invoke(()=>Reports.Distinct(ForAll),
            () => Reports.FirstBiggerThree(ForAll2),
            () => Reports.LessThanThree(ForAll),
            () => Reports.WithoutDryStorages(Storages));

           
            string path = @"D:\net";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            if (Storages != null)
            {
                using (StreamWriter streamWriter = new StreamWriter($@"{path}\test.csv", true, Encoding.GetEncoding("windows-1251")))
                {
                    foreach (var storage in Storages)
                    {
                        streamWriter.WriteLine($"{storage.Address.City};{storage.Address.Street}; open: {storage.open};{storage.PriceSum()}");
                    }
                }
            }


            //Сейчас для примера выведу, что не покажет функция добавления товара,
            //если я в закрытый склад попытаюсь добавить сыпучий товар
            //Console.WriteLine("Dry product can't add in this Storage! I'm adding Dry product into closed storage");
            /*try
            {
                IProduct Dry1 = new DryProduct("DryProduct", "The driest product", 200, SKUDry);
                ForAll.AddProduct(Dry1, 100);

            }
            catch (Exception dry)
            {
                Console.WriteLine(dry.Message);
            }
            finally
            {
                Console.WriteLine("finally test!");

            }*/

            Console.ReadKey();

        }
        private static void DisplayMessage(object sender, StorageEventArgs args)
        {
            logger.Debug($"Returned storage event: {args.Type}");
            logger.Debug(args.Message);
        }

        private static void ShowInfo(object sender, StorageEventArgs args)
        {
            logger.Debug($"All args: {args.Now.ToString()}, {args.Message}, {args.Adrs.City}, {args.Product.Name}");
        }
    }
}
