using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses
{
    public class Employee
    {
        public string FullName;
        public string Position;
        public Employee(string Fullname, string Position)
        {
            this.FullName = FullName;
            this.Position = Position;
        }
    }
}
