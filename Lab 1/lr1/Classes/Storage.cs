using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lr1.Classes
{
    class Storage
    {
        public string Name;
        public string Location;
        public List<Tuple<Product, List<Manufacturer>>> Products;

        public Storage(string name, string location)
        {
            Name = name;
            Location = location;
            Products = new List<Tuple<Product, List<Manufacturer>>>();
        }

        public string GetCompanies(string name)
        {
            int index = 0;
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Item1.Name == name)
                {
                    break;
                }
                index += 1;
            }

            string result = string.Empty;

            for (int i = 0; i < Products[index].Item2.Count-1; i++)
            {
                result += $"{Products[index].Item2[i]} / ";
            }
            result += $"{Products[index].Item2[^1]}";

            return result;
        }

        public void SetProducts(params Manufacturer[] manufacturersData)
        {
            foreach (var manufacturer in manufacturersData)
            {
                foreach (var product in manufacturer.Products)
                {
                    if (Products.Any(item => item.Item1 == product))
                    {
                        Products.Where(item => item.Item1 == product).ToList().ForEach(t => t.Item2.Add(manufacturer));
                    }
                    else
                    {
                        Products.Add(new Tuple<Product, List<Manufacturer>>(product, new List<Manufacturer> { manufacturer }));
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = String.Empty;
            foreach (var product in Products)
            {
                result += $"{product.Item1.Name} -";
                foreach (var manufacturer in product.Item2)
                {
                    result += $" {manufacturer.Name}";
                }
                result += $";\n";
            }
            return result;
        }
    }
}
