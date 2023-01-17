using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using lr1.Classes;
namespace lr1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = "storages.xml";


            var storages = new List<Storage>();
            var products = new List<Product>();
            var manufactures = new List<Manufacturer>();
            Console.WriteLine("Would you like to\n1 - type data manualy\n2 - insert data from the file?");
            string answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    Utilities.GetStorages(ref storages);
                    Utilities.GetManufactures(ref manufactures);
                    Utilities.GetProducts(ref products);
                    Utilities.PrintData(storages, products, manufactures);
                    Utilities.ProductsToManufactures(ref manufactures, products);
                    Utilities.ManufacturesToStorage(ref storages, manufactures, products);
                    break;
                case "2":
                    Utilities.InsertFromFile(ref storages, ref manufactures, ref products);
                    Utilities.PrintData(storages, products, manufactures);
                    break;
                default:
                    Utilities.InsertFromFile(ref storages, ref manufactures, ref products);
                    break;
            }

            // Utilities.InsertFromFile(ref storages, ref manufactures, ref products);
            Utilities.PrintData(storages, products, manufactures);

            Utilities.SaveToXML(storages, products, manufactures, fileName);

            Utilities.PrintXML(fileName);

            XDocument doc = XDocument.Load(fileName);

            var query1 = from b in doc.Root.Elements("storage")
                         orderby b.Elements("products").Elements("product").Count() descending
                         select new
                         {
                             Name = b.Attribute("name").Value,
                             Location = b.Element("location").Value,
                             ProductsCount = b.Elements("products").Elements("product").Count()
                         };

            Utilities.OutText("1. List of storages sorted by product count");
            foreach (var item in query1)
            {
                Console.WriteLine($"{item.Name} ({item.Location}) - {item.ProductsCount}pcs");
            }


            var query2 = (from b in doc.Root.Elements("storage")
                         select new
                         {
                             Name = b.Attribute("name").Value,
                             Location = b.Element("location").Value,
                             Products = b.Elements("products").Elements("product").Where(t => t.Attribute("name").Value.ToUpper()
                             .StartsWith("A")).Attributes("name").ToList(),
                         }).OrderByDescending(t => t.Products.Count);

            Utilities.OutText("2. List of products with name starts with <A> by storages");
            foreach (var item in query2)
            {
                string tempProducts = string.Empty;
                foreach (var prod in item.Products)
                {
                    tempProducts += $" {prod.Value},";
                }
                Console.WriteLine($"{item.Name} ({item.Location}) - {item.Products.Count()}pcs:{tempProducts}");
            }


            var query3 = from b in doc.Root.Elements("storage")
                         where b.Attribute("name").Value.Length == (
                            from c in doc.Root.Elements("storage")
                            select c.Attribute("name").Value.Length
                         ).Max()
                         select new
                         {
                             Name = b.Attribute("name").Value,
                             Location = b.Element("location").Value,
                             Products = b.Elements("products").Elements("product")
                             .Attributes("name").ToList(),
                         };
            
            Utilities.OutText("3. Products from storage with the longest name");
            foreach (var item in query3)
            {
                string tempProducts = string.Empty;
                foreach (var prod in item.Products)
                {
                    tempProducts += $"\n- {prod.Value}";
                }
                Console.WriteLine($"{item.Name} ({item.Location}) - {item.Products.Count()}pcs:{tempProducts}");
            }


            var query4 = from b in doc.Root.Elements("storage")
                         select new
                         {
                             Name = b.Attribute("name").Value,
                             TotalSum = Math.Round(b.Elements("products").Elements("product")
                             .ToList()
                             .Aggregate(0.0, (acc, x) => acc + (double)x.Element("price")* (int)x.Element("count")), 2)
                         };

            Utilities.OutText("4. Storages and total prices");
            foreach (var item in query4)
            {
                Console.WriteLine($"{item.Name} - {item.TotalSum}hrn");
            }


            var query5 = (from b in doc.Root.Elements("storage").Elements("products").Elements("product")
                         select new
                         {
                             Storage = $"{b.Parent.Parent.Attribute("name").Value} ({b.Parent.Parent.Element("location").Value})",
                             Name = b.Attribute("name").Value,
                             Price = (double)b.Element("price"),
                             Count = (int)b.Element("count"),
                             TotalPrice = Math.Round((double)b.Element("price") * (int)b.Element("count"), 2)
                         }).OrderByDescending(t => t.TotalPrice).ToList().Take(3);

            Utilities.OutText("5. Top 3 most expensive products");
            for (int i = 0; i < query5.Count(); i++)
            {
                int count = query5.ElementAt(i).Count;
                double price = query5.ElementAt(i).Price;
                Console.WriteLine($"{i + 1}. {query5.ElementAt(i).Name}: " +
                    $"{count}pcs {price}hrn each " +
                    $"(TOTAL: {query5.ElementAt(i).TotalPrice}hrn) - storing at {query5.ElementAt(i).Storage}");
            }


            var query6 = (from b in doc.Root.Elements("storage").Elements("products").Elements("product")
                          select new
                          {
                              Storage = $"{b.Parent.Parent.Attribute("name").Value} ({b.Parent.Parent.Element("location").Value})",
                              Name = b.Attribute("name").Value,
                              Dates = b.Element("dates").Elements("date").Select(t => t.Value).ToList(),
                          }).OrderByDescending(t => t.Dates.Count).Take(3);

            Utilities.OutText("6. Top 3 products by delivery dates count");
            foreach (var item in query6)
            {
                string tempDates = string.Empty;
                foreach (var date in item.Dates)
                {
                    tempDates += $"{date}, ";
                }
                Console.WriteLine($"{item.Name} ({item.Storage}): {tempDates}");
            }


            var query7 = Math.Round((from b in doc.Root.Elements("storage").Elements("products").Elements("product")
                          select new
                         {
                             Name = b.Attribute("name").Value,
                             Price = double.Parse(b.Element("price").Value)
                         }).Average(t => t.Price), 2);

            Utilities.OutText("7. Calculate the avarage price of products");
            Console.WriteLine($"The avarage price is {query7}hrn");


            int inputtedCount = 25;
            // Console.WriteLine("Input count of the products");
            // inputtedCount = int.Parse(Console.ReadLine());
            var query8 = from b in doc.Root.Elements("storage").Elements("products").Elements("product")
                          where int.Parse(b.Element("count").Value) > inputtedCount
                          select new
                        {
                            StorageName = b.Parent.Parent.Attribute("name").Value,
                            ProductName = b.Attribute("name").Value,
                            Count = int.Parse(b.Element("count").Value),
                            Price = double.Parse(b.Element("price").Value)
                        };

            Utilities.OutText($"8. All products from storages with count more than {inputtedCount}");
            foreach (var item in query8)
            {
                Console.WriteLine($"{item.ProductName} - {item.Count}pcs ({item.StorageName})");
            }


            var query9 = from b in doc.Root.Elements("storage").Elements("products").Elements("product")
                         where b.Elements("dates").Elements("date").Count() >= 2
                         where b.Attribute("name").Value.Length == 5
                         select new
                         {
                             StorageName = b.Parent.Parent.Attribute("name").Value,
                             ProductName = b.Attribute("name").Value,
                             Dates = b.Elements("dates").Elements("date").Select(t => t.Value).ToList()
                         };

            Utilities.OutText($"9. List of drugs with name legth eq 5 and its delivery dates >= 2");
            foreach (var item in query9)
            {
                string tempDates = string.Empty;
                foreach (var date in item.Dates)
                {
                    tempDates += $"{date}, ";
                }
                Console.WriteLine($"{item.ProductName} ({item.StorageName}) - {tempDates}");
            }


            var query10 = from b in doc.Root.Elements("storage").Elements("products").Elements("product")
                         where b.Elements("dates").Elements("date").Count() == 1 || b.Attribute("name").Value.Length > 7
                         select new
                         {
                             StorageName = b.Parent.Parent.Attribute("name").Value,
                             ProductName = b.Attribute("name").Value,
                             Dates = b.Elements("dates").Elements("date").Select(t => t.Value).ToList()
                         };

            Utilities.OutText($"10. List of drugs with name legth more than 7 or its delivery dates = 1");
            foreach (var item in query10)
            {
                string tempDates = string.Empty;
                foreach (var date in item.Dates)
                {
                    tempDates += $"{date}, ";
                }
                Console.WriteLine($"{item.ProductName} ({item.StorageName}) - {tempDates}");
            }


            var query11 = (from b in doc.Root.Elements("storage").Elements("products").Elements("product")
                          where b.Elements("dates").Elements("date").Count() == 1 || b.Attribute("name").Value.Length > 7
                          select new
                          {
                              StorageName = b.Parent.Parent.Attribute("name").Value,
                              ProductName = b.Attribute("name").Value,
                              Dates = b.Elements("dates").Elements("date").Select(t => t.Value).ToList()
                          }).OrderBy(t => t.StorageName).ThenBy(tt => tt.ProductName);

            Utilities.OutText($"11. Order by Storage name, then by Product name");
            foreach (var item in query11)
            {
                string tempDates = string.Empty;
                foreach (var date in item.Dates)
                {
                    tempDates += $"{date}, ";
                }
                Console.WriteLine($"{item.StorageName}\t({item.ProductName})\t- {tempDates}");
            }

            var query12 = (from b in doc.Root.Elements("storage")
                           select new
                           {
                               Name = b.Attribute("name").Value,
                               Count = b.Elements("products").Elements("product").Elements("dates").Elements("date").Count()
                           }).OrderByDescending(t => t.Count);

            Utilities.OutText($"12. Storages and dates of delivering (sorted by Descending)");
            foreach (var item in query12)
            {
                Console.WriteLine($"{item.Name} - {item.Count} dates");
            }


            var query13 = (from b in doc.Root.Elements("storage")
                           where b.Attribute("name").Value.Contains(b.Element("location").Value)
                           select new
                           {
                               Name = b.Attribute("name").Value,
                               Location = b.Element("location").Value,
                           }).OrderByDescending(t => t.Name);

            Utilities.OutText($"13. Storages with names contains city name");
            foreach (var item in query13)
            {
                Console.WriteLine($"{item.Name} ({item.Location})");
            }


            var query14 = (from b in doc.Root.Elements("storage")
                           where b.Element("location").Value.Contains("Kyiv")
                           select new
                           {
                               Name = b.Attribute("name").Value,
                               Location = b.Element("location").Value,
                           }).Union(
                from b in doc.Root.Elements("storage")
                 where b.Element("location").Value.Contains("Dnipro")
                 select new
                 {
                     Name = b.Attribute("name").Value,
                     Location = b.Element("location").Value,
                 }
                ).OrderByDescending(t => t.Name);

            Utilities.OutText($"14. Union func");
            foreach (var item in query14)
            {
                Console.WriteLine($"{item.Name} ({item.Location})");
            }

            var query15 = (from b in doc.Root.Elements("storage").Elements("products").Elements("product")

                           select new
                           {
                               StorageName = b.Parent.Parent.Attribute("name").Value,
                               ProductName = b.Attribute("name").Value,
                               Price = double.Parse(b.Element("price").Value),
                           }).Any(t => t.Price<50);

            Utilities.OutText($"15. Products cheaper than 50hrn");
            string result = query15 ? "some" : "not any";
            Console.WriteLine($"There are {result} products cheaper then than 50hrn");
        }
    }
}
