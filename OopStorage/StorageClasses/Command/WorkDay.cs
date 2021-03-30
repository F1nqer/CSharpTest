using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses.Command
{
    class WorkDay
    {
        Employee Employee;
        
        public WorkDay(Employee employee)
        {
            Employee = employee;
        }

        public void StartDay()
        {
            foreach(Adding i in Employee.CommandQueue) { 
                i.Execute();
            }
        }
        public void EndDay()
        {
            foreach (Adding i in Employee.CommandQueue)
            {
                i.Undo();
            }
        }

    }
}
