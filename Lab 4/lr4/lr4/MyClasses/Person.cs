using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4.MyClasses
{
    internal class Person : IPerson
    {
        private static int CaloriesCoef = 129;
        public string Name { get; private set; }
        public int Age { get; private set; }
        public int Height { get; private set; }
        public double Weight { get; private set; }
        public Person(string name, int age, int height, double weight)
        {
            Name = name;
            Age = age;
            Height = height;
            Weight = weight;
        }
        public int CalculateCalories()
        {
            return (int)Math.Round(Height * Weight * Age / CaloriesCoef, 0);
        }

        public void Print() => Console.WriteLine($"{ToString()}\nTOTAL: {CalculateCalories()}kkal");

        public override string ToString() => $"Person {Name} ({Age}yo) - {Height}sm, {Weight}kg";
         
    }
}
