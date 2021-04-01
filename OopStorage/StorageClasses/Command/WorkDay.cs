using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OopStorage.StorageClasses.Command
{
    class WorkDay
    {
        Employee Employee;
        CancellationToken token;
        public WorkDay(Employee employee)
        {
            Employee = employee;
        }

        public void AddCancelationToken(CancellationToken token)
        {
            this.token = token;
        }
        public void StartDay()
        {
            foreach (Adding i in Employee.CommandQueue)
            {
                Thread.Sleep(1000);
                if (token.IsCancellationRequested)
                {
                    return;
                }
                i.Execute();
            }
        }
        public void EndDay(Task t)
        {
            foreach (Adding i in Employee.CommandQueue)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                i.Undo();
            }
        }
    }
}
