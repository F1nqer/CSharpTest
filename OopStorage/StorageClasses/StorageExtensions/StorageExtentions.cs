using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses.StorageExtensions
{
    public static class StorageExtentions
    {
            public static Dictionary<int, string> StorageSKU(this Storage example)
            {
                Dictionary<int, string> SKUProducts = new Dictionary<int, string>();
                foreach (IProduct i in example.Products)
                {
                    SKUProducts.Add(i.SKU, i.Name);
                }
                return SKUProducts;
            }

        public static void TwoStorageProducts(this Storage example, Storage helper)
        {
            Dictionary<int, string> SKUProducts = new Dictionary<int, string>();
            foreach (IProduct i in example.Products)
            {
                foreach(IProduct j in helper.Products)
                {
                    if(i.SKU==j.SKU)
                        Console.WriteLine($"{i.SKU}: {i.Name} in {example.Address.City} and {helper.Address.City}");
                }
            }
        }

        public static void OtherStorageHelp(this Storage first, Storage second)
        {
            foreach (IProduct i in first.Products)
            {
                if (i.Count > 1)
                {
                    if (!second.Products.Contains(i))
                    {
                        IProduct helper = second.Products.Find(x => x.SKU == i.SKU);
                        first.MoveProduct(i, (int)Math.Round(i.Count / 2), second);
                    }
                }
            }

        }


    }
}
