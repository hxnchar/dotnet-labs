using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4.MyClasses
{
    public class Decorator : IPerson
    {
        public IPerson Person { get; private set; }
        public Decorator (IPerson person)
        {
            Person = person;
        }
        public virtual int CalculateCalories() => Person.CalculateCalories();
        public virtual void Print() => Console.WriteLine($"{Person}\nTOTAL:{CalculateCalories()}kkal");
    }   
}
