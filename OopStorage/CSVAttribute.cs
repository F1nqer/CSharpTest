using OopStorage.StorageClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OopStorage
{
    public class CSVAttribute : System.Attribute
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string SKU { get; set; }
        public string Definition { get; set; }
        public string Price { get; set; }
        public string Count { get; set; }
        public CSVAttribute(string Name, string Type, string SKU, string Definition, string Price, string Count)
        { this.Name = Name; this.Type = Type; this.SKU = SKU; this.Definition = Definition; this.Price = Price; this.Count = Count; }
    }
    [CSVAttribute("Name", "Type", "SKU", null, "Price", "Quantity")]
    public class CSVtest
    {
        public static void CSVwriter(Storage a, string path)
        {

            if (Directory.Exists(path))
            {
                int count = 1;
                Type CSVType = typeof(CSVtest);

                var pi = CSVType.GetProperty("Name");
                object[] aaaaaaaaa = CSVType.GetProperties();

                var k = typeof(CSVtest).GetCustomAttributes(typeof(CSVAttribute), false);
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(CSVType);

                using (StreamWriter sw = new StreamWriter($@"{path}\test.csv", true, System.Text.Encoding.Default))
                {
                    foreach (System.Attribute i in attrs)
                    {
                        CSVAttribute test = (CSVAttribute)i;
                        sw.Write($"{test.Name};{test.Type};{test.SKU};{test.Definition};{test.Price};{test.Count};");
                    }
                    sw.WriteLine();
                }
                foreach (IProduct pr in a.Products)
                {
                    using (StreamWriter sw = new StreamWriter($@"{path}\test.csv", true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine($"{count};{pr.Name};{pr.Type};{pr.SKU};{pr.Definition};{pr.Price};{pr.Count};");
                    }
                    count++;
                }
            }
            else
            {
                Console.WriteLine("File not saved");
            }
        }
    }
}
