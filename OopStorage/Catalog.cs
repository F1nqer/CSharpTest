using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopStorage.StorageClasses;

namespace OopStorage
{
    public class Catalog
    {
        private static Catalog _instance;
        private List<IProduct> Products = new List<IProduct>();
        int SKUCocaCola = 1949390;
        int SKUCar = 2312312;
        int SKUDry = 5949494;
        int SKUPeace = 4930323;

        public static Catalog GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Catalog();
            }
            return _instance;
        }

        public IProduct GetProduct(int sku)
        {
            return Products.Find(x => x.SKU == sku);
        }

        public Dictionary<int, string> GetAllProducts() {
            Dictionary<int, string> SKUProducts = new Dictionary<int, string>();
            foreach (IProduct i in Products)
            {
                SKUProducts.Add(i.SKU, i.Name);
            }
            return SKUProducts;
        }

        private Catalog()
        {
            Products.Add(new LiquidProduct("CocaCola", "The best liquid", 200, SKUCocaCola));
            Products.Add(new OverallProduct("TheBestCar", "Good good car", 2000, SKUCar));
            Products.Add(new DryProduct("DryProduct", "The driest product", 200, SKUDry));
            Products.Add(new PeaceProduct("ChupaChups", "The best candy", 24, SKUPeace));
        }
    }
}
