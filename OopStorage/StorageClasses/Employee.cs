using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using OopStorage.StorageClasses.Command;

namespace OopStorage.StorageClasses
{
    public class Employee
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string FullName;
        public string Position;
        public Storage Storage;
        public ConcurrentQueue<ICommand> CommandQueue = new ConcurrentQueue<ICommand>();
        public Employee(string Fullname, string Position)
        {
            this.FullName = Fullname;
            this.Position = Position;
            
        }
        public void AddingTask(IProduct product, int count)
        {
            Adding helper = new Adding(Storage, product, count);
            helper.Notify += DisplayMessage;
            CommandQueue.Enqueue(helper);
        }
        private static void DisplayMessage(string message)
        {
            logger.Debug(message);
        }
    }
}
