using OopStorage.StorageClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage
{
    class Reports
    {
        public static void LessThanThree(Storage first)
        {
            List<IProduct> helper = first.Products;
            var selectedItems = from t in helper
                                where t.Count < 3
                                select t;
            String report = "Report LessThanThree!";
            foreach (IProduct j in selectedItems.ToList())
            {
                report = "\n";
                report += "\n" + j.Name;
                report += "\n" + j.SKU;
                report += "\n" + j.Count;
            }
            Console.WriteLine(report);
        }
        public async void LessThanThreeAsync(Storage first)
        {
            await Task.Run(()=>LessThanThree(first));
        }


        public static void Distinct(Storage first)
        {
            List<IProduct> helper = first.Products;
            var selectedItems = from t in helper
                                select t;
            selectedItems = selectedItems.Distinct();
            String report = "Report Distinct!";
            foreach (IProduct j in selectedItems.ToList())
            {
                report = "\n";
                report += "\n" + j.Name;
                report += "\n" + j.SKU;
                report += "\n" + j.Count;
            }
            Console.WriteLine(report);
        }
        public async void DistinctAsync(Storage first)
        {
            await Task.Run(() => Distinct(first));
        }

        public static void FirstBiggerThree(Storage first)
        {
            List<IProduct> helper = first.Products;
            var selectedItems = helper.OrderByDescending(u => u.Count).Take(5);
            String report = "Report LessThanThree!";
            foreach (IProduct j in selectedItems.ToList())
            {
                report += "\n";
                report += "\n" + j.Name;
                report += "\n" + j.SKU;
                report += "\n" + j.Count;
            }
            Console.WriteLine(report);
        }
        public async void FirstBiggerThreeAsync(Storage first)
        {
            await Task.Run(() => FirstBiggerThree(first));
        }

        public static void WithoutDryStorages(List<Storage> storages)
        {
            var selectedStorages = storages.Where(p => p.Products.Where(a => a.Type != "Dry").Any()).ToList();
            String report = "Report WithoutDryStorages!";
            foreach (Storage i in selectedStorages.ToList())
            {
                report += "/n";
                report += i.Products.Count;
            }
            Console.WriteLine(report);
        }
        public async void WithoutDryStoragesAsync(List<Storage> storages)
        {
            await Task.Run(() => WithoutDryStorages(storages));
        }
    }
}
