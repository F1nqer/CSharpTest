using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaspiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[10];
            string chet;
            string prost;
            int checkerProst = 0;
            int summ = 0;


            for(int i = 0; i<10; i++)
            {
                a[i] = Convert.ToInt32(Console.ReadLine());
            }

            //я бы использовал решето эратосфена, но мне долго вспоминать всё это
            for (int i = 0; i < 10; i++)
            {
                summ += a[i];
                if (a[i]%2 == 0)
                {
                    chet = "chetnoe";
                }
                else
                {
                    chet = "nechetnoe";
                }
                //просто бегу от 1 до числа и если больше 2 делителей, то это простое
                for (int j = 1; j < a[i]+1; j++)
                {
                    if (a[i] % j == 0)
                    {
                        checkerProst++;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (checkerProst > 2)
                {
                    prost = "neprostoe";
                }
                else
                {
                    prost = "prostoe";
                }
                checkerProst = 0;
                Console.WriteLine($"{a[i]} is {prost} and {chet}");
            }
            Console.WriteLine($"summa is {summ}");
            Console.ReadKey();
        }
    }
}
