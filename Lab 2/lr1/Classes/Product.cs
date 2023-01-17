using System;
using System.Collections.Generic;
using System.Text;

namespace lr1.Classes
{
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public List<Date> deliveryDates { get; set; }

        public Product(string name, double price, int count, List<Date> dates)
        {
            Name = name;
            Price = price;
            Count = count;
            deliveryDates = dates;
        }

        public override string ToString()
        {
            return $"{Name} ({Count} x {Price}hrn)";
        }


        public string GetDates()
        {
            string result = String.Empty;
            for (int i = 0; i < deliveryDates.Count - 1; i++)
            {
                result += $"{deliveryDates[i]} / ";
            }
            result += $"{deliveryDates[^1]}";
            return result;
        }
    }
}
