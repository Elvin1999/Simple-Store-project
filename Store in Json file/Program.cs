using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
namespace Store_in_Json_file
{
    class Product
    {
        public Product() { }
        public Product(string name, decimal price, int count)
        {
            Name = name;
            Price = price;
            Count = count;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public void ShowProduct()
        {
            Console.WriteLine("====================");
            Console.WriteLine($"Name - {Name}");
            Console.WriteLine($"Price - {Price}");
            Console.WriteLine($"Count - {Count}");
        }
    }
    class Store
    {
        JsonSerializer json = new JsonSerializer();
        List<Product> list = new List<Product>()
        {
            new Product("Xbox", 650.895m, 30),
            new Product("Ps4", 450.789m, 15),
            new Product("IphoneX", 1500.56m, 80)
        };
        public Store()
        {
            FileInfo fileinfo = new FileInfo("list555.json");
            if (!fileinfo.Exists)
            {
                SerializerToJASON();
            }
            else if (fileinfo.Exists)
            {
                list = DeserializerFromJason();
            }
        }
        public void SerializerToJASON()
        {
            using (StreamWriter sw = new StreamWriter("list555.json"))
            {
                json.Serialize(sw, list);
            }
        }
        public List<Product> DeserializerFromJason()
        {
            using (StreamReader sr = new StreamReader("list555.json"))
            {
                string reader = sr.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<Product>>(reader);
                return list;
            }
        }
        public void BuyProduct(string name, int count)
        {
            list = DeserializerFromJason();
            var item = list.SingleOrDefault(x => x.Name == name);
            if (count <= item.Count)
            {
                item.Count -= count;
            }
            SerializerToJASON();
        }
        public void Run()
        {
            foreach (var item in list)
            {
                item.ShowProduct();
            }
            Console.Write("Write Product name - >");
            string name = Console.ReadLine();
            Console.WriteLine("Count - >");
            int count = Convert.ToInt32(Console.ReadLine());
            BuyProduct(name, count);
            Console.Clear();
            list = DeserializerFromJason();
            foreach (var item in list)
            {
                item.ShowProduct();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            store.Run();
        }
    }
}
