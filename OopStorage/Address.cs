using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopStorage
{
    public class Address
    {
        public string City;
        public string Street;
        public int HomeNum;
        public int HomeFloor;
        public int FlatNum;
        public Address(string City, string Street, int HomeNum, int HomeFloor, int FlatNum)
        {
            this.City = City;
            this.Street = Street;
            this.HomeNum = HomeNum;
            this.HomeFloor = HomeFloor;
            this.FlatNum = FlatNum;
        }
        public Address(string City, string Street, int HomeNum)
        {
            this.City = City;
            this.Street = Street;
            this.HomeNum = HomeNum;
        }
    }
}
