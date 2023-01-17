using System;
using System.Collections.Generic;
using System.Linq;
using lr1.Classes;
namespace lr1
{
    class Program
    {
        static void Main(string[] args)
        {
            var storage = new Storage("Simple Storage", "Kyiv");

            var bionorica = new Manufacturer("Bionorica", "Bionorica st. 15");
            var ipsen = new Manufacturer("IPSEN", "IPSEN ave 3");
            var sanofi = new Manufacturer("Sanofi", "Sanofi st. 13");
            var stada = new Manufacturer("STADA", "STADA st. 24");
            var bayer = new Manufacturer("BAYER", "BAYER ave 96");

            List<Manufacturer> manufactures = new List<Manufacturer>()
            { bionorica, ipsen,sanofi, stada, bayer};

            var kanefron = new Product("Kanefron", 139, 15, Date.GenerateDates(3));
            var tinzilgon = new Product("Tinzilgon", 349, 8, Date.GenerateDates(2));
            var forlaks = new Product("Forlaks", 38, 12, Date.GenerateDates(1));
            var fortrans = new Product("Fortrans", 63, 8, Date.GenerateDates(1));
            var eziklen = new Product("Eziklen", 87, 7, Date.GenerateDates(4));
            var amaril = new Product("Amaril", 90, 4, Date.GenerateDates(1));
            var arava = new Product("Arava", 190, 6, Date.GenerateDates(2));
            var akvalor = new Product("Akvalor", 241, 7, Date.GenerateDates(2));
            var vitrum = new Product("Vitrum", 165, 3, Date.GenerateDates(2));
            var aveloks = new Product("Aveloks", 83, 4, Date.GenerateDates(2));
            var antrakol = new Product("Antrakol", 615, 2, Date.GenerateDates(1));
            var baycidal = new Product("Baycidal", 369, 2, Date.GenerateDates(1));
            
            List<Product> products = new List<Product>() { kanefron, tinzilgon, forlaks,
            fortrans, eziklen, amaril, arava, akvalor, vitrum, aveloks, antrakol, baycidal
            };

            var prikol1 = new Product("Prikol 1", 55, 10, Date.GenerateDates(5));
            var prikol2 = new Product("Prikol 2", 15, 20, Date.GenerateDates(4));

            List<Product> products2 = new List<Product>() { prikol1, prikol2 };

            bionorica.AddProducts(kanefron, tinzilgon, amaril, baycidal);
            ipsen.AddProducts(forlaks, fortrans, eziklen, akvalor, vitrum);
            sanofi.AddProducts(amaril, arava, vitrum);
            stada.AddProducts(akvalor, vitrum, forlaks);
            bayer.AddProducts(aveloks, antrakol, baycidal, tinzilgon, kanefron, prikol1, prikol2);

            storage.SetProducts(bionorica, ipsen, sanofi, stada, bayer);


            Utilities.OutText("1. List of all drugs");
            var query1 = from x in products
                        select new
                        {
                            Name = x.ToString()
                        };
            foreach (var item in query1)
            {
                Console.WriteLine($"{item.Name}");
            }

            Console.WriteLine("\nClassic method\n");
            var query1r = new List<Product>();
            foreach (var product in products)
            {
                query1r.Add(product);
            }
            foreach (var item in query1r)
            {
                Console.WriteLine($"{item}");
            }


            Utilities.OutText("2. List of drugs with name starts with <A>");
            var query2 = from x in products
                    where x.Name.ToUpper().StartsWith('A')
                    select new
                    {
                        Name = x.ToString()
                    };
            foreach (var item in query2)
            {
                Console.WriteLine($"{item.Name}");
            }
            Console.WriteLine("\nClassic method\n");
            var query2r = new List<Product>(); ;
            foreach (var product in products)
            {
                if (product.Name.ToUpper()[0] == 'A')
                {
                    query2r.Add(product);
                }
            }
            foreach (var item in query2r)
            {
                Console.WriteLine($"{item}");
            }


            Utilities.OutText("3. List of drugs and manufactures");
            var query3 = from x in products
                         join y in storage.Products on x.Name equals y.Item1.Name
                         select new
                         {
                             Name = x.ToString(),
                             Companies = y.Item2
                         };
            foreach (var item in query3)
            {
                string companies = string.Empty;
                foreach (var company in item.Companies)
                {
                    companies += $"{company} ";
                }
                Console.WriteLine($"{item.Name} ({companies})");
            }
            Console.WriteLine("\nClassic method\n");
            List<Tuple<Product, List<Manufacturer>>> query3r = new List<Tuple<Product, List<Manufacturer>>>();
            foreach (var product in products)
            {
                List<Manufacturer> manufacturers = new List<Manufacturer>();
                foreach (var manufacturer in storage.Products)
                {
                    if (product.Name == manufacturer.Item1.Name)
                    {
                        manufacturers = manufacturer.Item2;
                        break;
                    }
                }
                query3r.Add(new Tuple<Product, List<Manufacturer>>(product, manufacturers));
            }
            foreach (var item in query3r)
            {
                string companies = string.Empty;
                foreach (var company in item.Item2)
                {
                    companies += $"{company} ";
                }
                Console.WriteLine($"{item.Item1} ({companies})");
            }


            Utilities.OutText("4. List of drugs and manufactures count");
            var query4 = storage.Products.GroupBy(product => new
            {
                Name = product.Item1.Name,
                Count = product.Item2.Select(z => z.Name).Distinct().Count()
            });
            foreach (var item in query4)
            {
                Console.WriteLine($"{item.Key.Name} - {item.Key.Count} times");
            }
            List<KeyValuePair<Product, int>> query4r = new List<KeyValuePair<Product, int>>();
            foreach (var product in storage.Products)
            {
                foreach (var manufacturer in product.Item2)
                {
                    int index = query4r.FindIndex(a => a.Key.Name == product.Item1.Name);
                    if (index == -1)
                    {
                        query4r.Add(new KeyValuePair<Product, int>(product.Item1, 1));
                    }
                    else
                    {
                        int count = query4r[index].Value;
                        query4r[index] = new KeyValuePair<Product, int>(product.Item1, ++count);
                    }
                }
            }
            Console.WriteLine("\nClassic method\n");
            foreach (var item in query4r)
            {
                Console.WriteLine($"{item.Key.Name} - {item.Value} times");
            }


            Utilities.OutText("5. Sorted list of drugs and manufactures count");
            var query5 = storage.Products.GroupBy(product => new
            {
                Name = product.Item1.Name,
                Count = product.Item2.Select(z => z.Name).Distinct().Count()
            }).OrderByDescending(t => t.Key.Count);
            foreach (var item in query5)
            {
                Console.WriteLine($"{item.Key.Name} - {item.Key.Count} times");
            }
            List<KeyValuePair<Product, int>> query5r = new List<KeyValuePair<Product, int>>();
            foreach (var product in storage.Products)
            {
                foreach (var manufacturer in product.Item2)
                {
                    int index = query5r.FindIndex(a => a.Key.Name == product.Item1.Name);
                    if (index == -1)
                    {
                        query5r.Add(new KeyValuePair<Product, int>(product.Item1, 1));
                    }
                    else
                    {
                        int count = query5r[index].Value;
                        query5r[index] = new KeyValuePair<Product, int>(product.Item1, ++count);
                    }
                }
            }
            query5r.Sort((a, b) => b.Value.CompareTo(a.Value));
            Console.WriteLine("\nClassic method\n");
            foreach (var item in query5r)
            {
                Console.WriteLine($"{item.Key.Name} - {item.Value} times");
            }


            Utilities.OutText("6. Sorted list of drugs and total amount of money");
            var query6 = (from x in products
                    select new
                    {
                        Name = x.ToString(),
                        Total = x.Price * x.Count
                    }).OrderByDescending(t => t.Total);
            foreach (var item in query6)
            {
                Console.WriteLine($"{item.Name} = {item.Total} hrn");
            }
            var query6r = products;
            query6r.Sort((a, b) => (b.Count * b.Price).CompareTo(a.Count * a.Price));
            Console.WriteLine("\nClassic method\n");
            foreach (var item in query6r)
            {
                Console.WriteLine($"{item} = {item.Count * item.Price} hrn");
            }


            Utilities.OutText("7. List of drugs and delivery dates (sorted by count delivery dates)");
            var query7 = (from x in products
                          select new
                          {
                              Name = x.ToString(),
                              Dates = x.deliveryDates
                          }).OrderBy(t => t.Dates.Count);
            foreach (var item in query7)
            {
                string dates = string.Empty;
                foreach (var date in item.Dates)
                {
                    dates += $"{date} ";
                }
                Console.WriteLine($"{item.Name}: {dates}");
            }
            var query7r = products;
            query7r.Sort((a,b) => a.deliveryDates.Count.CompareTo(b.deliveryDates.Count));
            Console.WriteLine("\nClassic method\n");
            foreach (var item in query7r)
            {
                string dates = string.Empty;
                foreach (var date in item.deliveryDates)
                {
                    dates += $"{date} ";
                }
                Console.WriteLine($"{item}: {dates}");
            }

            Utilities.OutText("8. List of drugs with name legth eq 5 and its delivery dates >= 2");
            var query8 = from x in products
                         where x.deliveryDates.Count >= 2
                         where x.Name.Length == 5
                         select new
                         {
                             Name = x.ToString(),
                             Dates = x.GetDates()
                         };
            foreach (var item in query8)
            {
                Console.WriteLine($"{item.Name}: {item.Dates}");
            }
            var query8r = new List<Product>();
            foreach (var item in products)
            {
                if (item.deliveryDates.Count >= 2 && item.Name.Length == 5)
                {
                    query8r.Add(item);
                }
            }
            Console.WriteLine("\nClassic method\n");
            foreach (var item in query8r)
            {
                Console.WriteLine($"{item}: {item.GetDates()}");
            }

            Utilities.OutText("9. Order by 2 fields");
            var query9 = (from x in products
                          select new
                          {
                              Name = x.ToString(),
                              Dates = x.deliveryDates
                          }).OrderBy(t => t.Name).ThenByDescending(tt => tt.Dates.Count);
            foreach (var item in query9)
            {
                string dates = string.Empty;
                foreach (var date in item.Dates)
                {
                    dates += $"{date} ";
                }
                Console.WriteLine($"{item.Name}: {dates}");
            }
            var query9r = products;
            query9r.Sort(delegate(Product product1, Product product2)
            {
                int xdiff = product1.Name.CompareTo(product2.Name);
                if (xdiff != 0) return xdiff;
                else return product1.deliveryDates.Count.CompareTo(product2.deliveryDates.Count);
            });
            Console.WriteLine("\nClassic method\n");
            foreach (var item in query9r)
            {
                string dates = string.Empty;
                foreach (var date in item.deliveryDates)
                {
                    dates += $"{date} ";
                }
                Console.WriteLine($"{item}: {dates}");
            }


            Utilities.OutText("10. List of drugs with name length <= 5 or its delivery dates count eq 1");
            var query10 = from x in products
                         where x.deliveryDates.Count == 1 || x.Name.Length <= 5
                         select new
                         {
                             Name = x.ToString(),
                             Dates = x.GetDates()
                         };
            foreach (var item in query10)
            {
                Console.WriteLine($"{item.Name}: {item.Dates}");
            }
            var query10r = new List<Product>();
            foreach (var item in products)
            {
                if (item.deliveryDates.Count == 1 || item.Name.Length <= 5)
                {
                    query10r.Add(item);
                }
            }
            Console.WriteLine("\nClassic method\n");
            foreach (var item in query10r)
            {
                Console.WriteLine($"{item}: {item.GetDates()}");
            }


            Utilities.OutText("11. Concat func");
            var query11 = products.Concat(products2);
            foreach (var item in query11)
            {
                Console.WriteLine($"{item.Name}");
            }
            List<Product> union11r = new List<Product>();
            foreach (var item in products)
            {
                union11r.Add(item);
            }
            foreach (var item in products2)
            {
                union11r.Add(item);
            }
            Console.WriteLine("\nClassic method\n");
            foreach (var item in union11r)
            {
                Console.WriteLine($"{item.Name}");
            }

            Utilities.OutText("12. Average price");
            var query12 = Math.Round(products.Average(t => t.Price), 2);
            Console.WriteLine($"{query12} hrn");
            Console.WriteLine("\nClassic method\n");
            double result12r = 0;
            double sum12 = 0;
            int denominator12 = 0;
            foreach (var product in products)
            {
                sum12 += product.Price;
                denominator12 += 1;
            }
            result12r = Math.Round(sum12 / denominator12, 2);
            Console.WriteLine($"{result12r} hrn");

            List<Product> testProducts1 = new List<Product>() { kanefron, tinzilgon, forlaks, fortrans };
            List<Product> testProducts2 = new List<Product>() { forlaks, fortrans, eziklen, amaril };

            Utilities.OutText("13. Except of produts");
            var query13 = testProducts1.Except(testProducts2);
            Console.WriteLine("\n1st set:");
            foreach (var product in testProducts1)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("\n2nd set:");
            foreach (var product in testProducts2)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("\nResult:");
            foreach (var product in query13)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("\nClassic method\n");
            var query13r = new List<Product>();
            foreach (var product1 in testProducts1)
            {
                bool present13 = false;
                foreach (var product2 in testProducts2)
                {
                    if (product1 == product2)
                    {
                        present13 = true;
                    }
                }
                if (!present13)
                {
                    query13r.Add(product1);
                }
            }
            foreach (var product in query13r)
            {
                Console.WriteLine(product);
            }


            Utilities.OutText("14. Intersect of produts");
            var query14 = testProducts1.Intersect(testProducts2);
            Console.WriteLine("\n1st set:");
            foreach (var product in testProducts1)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("\n2nd set:");
            foreach (var product in testProducts2)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("\nResult:");
            foreach (var product in query14)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("\nClassic method\n");
            var query14r = new List<Product>();
            foreach (var product1 in testProducts1)
            {
                foreach (var product2 in testProducts2)
                {
                    if (product1 == product2)
                    {
                        query14r.Add(product2);
                    }
                }
            }
            foreach (var product in query14r)
            {
                Console.WriteLine(product);
            }

            Utilities.OutText("15. Any produts");
            if (products.Any(n => n.Price<20))
            {
                Console.WriteLine("There are some products with price < 20");
            }
            else
            {
                Console.WriteLine("There are no products with price < 20");
            }
            Console.WriteLine("\nClassic method\n");
            bool isTrue15 = false;
            foreach (var product in products)
            {
                if (product.Price<20)
                {
                    isTrue15 = true;
                }
            }
            if (isTrue15)
            {
                Console.WriteLine("There are some products with price < 20");
            }
            else
            {
                Console.WriteLine("There are no products with price < 20");
            }
        }
    }
}
