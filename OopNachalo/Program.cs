using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopNachalo
{
    class Lift {
        int maxFloor;
        List<Human> People = new List<Human>();
        List<Human> inLift = new List<Human>();
        List<int> floorQueue = new List<int>();
        int maxWeight;
        int activeFloor;
        bool door;
        bool up;

        public void go()
        {/*
            People.OrderBy(x => x.onFloor).ToList();
            
            People.RemoveAll(x => x.onFloor == activeFloor);
            activeFloor = floorQueue.Find(x => x<activeFloor);
            */
            Console.WriteLine("go!");
            while(true)
            {
                if(up == true)
                {
                    for(int i = activeFloor; i<=maxFloor; i++)
                    {
                        if (floorQueue.Contains(i)) {
                            if (i == maxFloor)
                            {
                                up = false;
                                comeIn();
                                comeOut();
                                activeFloor = i;
                                break;
                            }
                            else
                            {
                                comeIn();
                                comeOut();
                                activeFloor = i;
                            }
                            Console.WriteLine($"Up {i}");
                        }
                        else
                        {
                            Console.WriteLine($"Up {i}");
                        }
                    }
                }
                else
                {
                    for (int i = activeFloor; i >=activeFloor; i--)
                    {
                        if (floorQueue.Contains(i))
                        {
                            if (i == 1)
                            {
                                up = true;
                                comeIn();
                                comeOut();
                                activeFloor = i;
                                break;
                            }
                            else
                            {
                                comeIn();
                                comeOut();
                                activeFloor = i;
                            }
                            Console.WriteLine($"Down {i}");
                        }
                        else
                        {
                            Console.WriteLine($"Down {i}"); 
                        }
                    }
                }
                if (!floorQueue.Any())
                {
                    break;
                }
            }
            
        }

       

        void openDoor()
        {
            door = true;
            Console.WriteLine("Doors open");
        }
        void closeDoor()
        {
            door = false;
            Console.WriteLine("Doors close");
        }

        void comeIn()
        {
            openDoor();
            inLift.AddRange(People.FindAll(x => x.onFloor == activeFloor));
            People.RemoveAll(x => x.onFloor == activeFloor);
            foreach(Human i in inLift)
            {
                floorQueue.Append(i.goFloor);
            }
            floorQueue = floorQueue.Distinct().ToList();
            floorQueue.Sort();
            Console.WriteLine("All in lift");
            closeDoor();
        }

        void comeOut()
        {
            openDoor();
            inLift.RemoveAll(x => x.goFloor == activeFloor);
            floorQueue.RemoveAll(x => x == activeFloor);
            floorQueue = floorQueue.Distinct().ToList();
            floorQueue.Sort();
            Console.WriteLine("All out lift");
            closeDoor();
        }

        public Lift(int maxWeight, int maxFloor, int activeFloor, List<Human> People)
        {
            this.maxFloor = maxFloor;
            this.maxWeight = maxWeight;
            this.activeFloor = activeFloor;
            this.People = People;
            foreach (Human i in People)
            {
                floorQueue.Append(i.onFloor);
            }
            floorQueue = floorQueue.Distinct().ToList();
            if (activeFloor>maxFloor || activeFloor < 1)
            {
                Console.WriteLine("Lift is crushed");
                Environment.Exit(1);
            }
            else
            {
                up = true;
            }
            Console.WriteLine("Ia zhivoi!");
            closeDoor();
        }
    }
    class Human {
        string name;
        int weight;
        public int goFloor;
        public int onFloor;
        public bool wantToGo = true;
        public Human(string name, int weight, int onFloor, int goFloor)
        {
            this.name = name;
            this.weight = weight;
            this.goFloor = goFloor;
            this.onFloor = onFloor;
            }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Human a = new Human("a", 100, 1, 9);
            Human b = new Human("b", 90, 5, 9);
            Human c = new Human("c", 70, 3, 5);
            Human d = new Human("d", 30, 1, 8);
            Human e = new Human("e", 120, 9, 1);
            List<Human> People = new List<Human>() { a, b, c, d, e };

            Lift start = new Lift(1200, 10, 3, People);
            start.go();
            Console.ReadKey();
        }
    }
}
