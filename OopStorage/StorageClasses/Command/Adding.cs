using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses.Command
{
    public class Adding: ICommand
    {
        Storage Storage;
        int Count;
        IProduct Product;
        public delegate void AddingHandler(string message);
        public event AddingHandler Notify;
        public Adding(Storage storage, IProduct product, int count)
        {
            Storage = storage;
            Product = product;
            Count = count;
        }
        public void Execute() 
        {
            Storage.AddProduct(Product, Count);
            
        }
        public void Undo() 
        {
            Notify?.Invoke($"{Storage.MainEmployee.FullName} added {Product.Name} in storage. Count: {Count}.");
        }
    }
}
