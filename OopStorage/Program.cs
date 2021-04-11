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
using System.Diagnostics;
using System.Reflection;

namespace OopStorage
{
    internal class Program
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            /*List<int> a = new List<int>();
            Type myType = a.GetType();*/
            Assembly asm = Assembly.LoadFile("C:/Windows/Microsoft.NET/Framework64/v4.0.30319/System.dll");
            Type[] types = asm.GetTypes();
            Type check = asm.GetType();
            foreach (Type t in types)
            {
                if (t.Name.Contains("StringCollection") || t.Name.Contains("Generic"))
                {
                    Console.WriteLine(t.Name);
                    check = t;
                }
            }

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

           
            string path = @"D:/net/";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            CSVtest.CSVwriter(ForAll, path);
            CSVtest.CSVwriter(ForAll2, path);
            /*if (Storages != null)
            {
                using (StreamWriter streamWriter = new StreamWriter($@"{ path}\test.csv", true, Encoding.GetEncoding("windows-1251")))
                {
                    foreach (var storage in Storages)
                    {
                        streamWriter.WriteLine($"{storage.Address.City};{storage.Address.Street}; open: {storage.open};{storage.PriceSum()}");
                    }
                }
            }
*/
            Task.WaitAll();

            /*forMd5();*/ //функция для md5

           

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
        
        private static void md5(int count)
        {
            string str = "abcdeABCDEOIBAI"+count.ToString();
            int j = 0;
            do
            {
                j++;
                /*Console.WriteLine("start");*/
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] bytes = Encoding.Default.GetBytes(str);
                byte[] encoded = md5.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < encoded.Length; i++)
                    sb.Append(encoded[i].ToString("x2"));

                Console.WriteLine(sb.ToString());
                /*Console.WriteLine("end");*/
            }
            while (j < count);
        }

        private static void forMd5()
        {
            Stopwatch StopWatch = new Stopwatch();
            Stopwatch StopWatch1 = new Stopwatch();
            Stopwatch StopWatch2 = new Stopwatch();
            StopWatch.Start();
            ParallelLoopResult result = Parallel.For(1, 8, md5);
            Task.WaitAll();
            if (result.IsCompleted)
                StopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = StopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            StopWatch1.Start();
            ParallelLoopResult result1 = Parallel.For(1, 32, md5);
            Task.WaitAll();
            if (!result1.IsCompleted)
                StopWatch1.Stop();
            TimeSpan ts1 = StopWatch1.Elapsed;

            string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
               ts1.Hours, ts1.Minutes, ts1.Seconds,
               ts1.Milliseconds / 10);

            StopWatch.Start();
            ParallelLoopResult result2 = Parallel.For(1, 64, md5);
            Task.WaitAll();
            if (!result2.IsCompleted)
                StopWatch.Stop();
            TimeSpan ts2 = StopWatch2.Elapsed;
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
               ts2.Hours, ts2.Minutes, ts2.Seconds,
               ts2.Milliseconds / 10);


            Console.WriteLine("RunTime " + elapsedTime);
            Console.WriteLine("RunTime1 " + elapsedTime1);
            Console.WriteLine("RunTime2 " + elapsedTime2);
        }

        private static void ShowInfo(object sender, StorageEventArgs args)
        {
            logger.Debug($"All args: {args.Now.ToString()}, {args.Message}, {args.Adrs.City}, {args.Product.Name}");
        }


    }
}
