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
        public static List<IProduct> LessThanThree(Storage first)
        {
            List<IProduct> helper = first.Products;
            var selectedItems = from t in helper
                                where t.Count < 3
                                select t;
            return selectedItems.ToList();
        }
        public static List<IProduct> Distinct(Storage first)
        {
            List<IProduct> helper = first.Products;
            var selectedItems = from t in helper
                                select t;
            selectedItems = selectedItems.Distinct();
            return selectedItems.ToList();
        }
        public static List<IProduct> FirstBiggerThree(Storage first)
        {
            List<IProduct> helper = first.Products;
            var selectedItems = helper.OrderByDescending(u => u.Count).Take(5);
            return selectedItems.ToList();
        }

        public static List<Storage> WithoutDryStorages(List<Storage> storages)
        {
            var selectedStorages = storages.Where(p => p.Products.Where(a => a.Type != "Dry").Any()).ToList();
            return selectedStorages;
        }
    }
}
