using System;
using System.Collections.Generic;
using System.Text;

namespace lr1.Classes
{
    class Manufacturer
    {
        public string Name;
        public string Address;
        public List<Product> Products;
        public Manufacturer(string name, string address)
        {
            Name = name;
            Address = address;
            Products = new List<Product>();
        }

        public void AddProducts(params Product[] productArray)
        {
            foreach (var product in productArray)
            {
                Products.Add(product);
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Address})";
        }
    }
}
