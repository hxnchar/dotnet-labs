using lr4.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPerson person = new Person("Kolya", 20, 180, 75);
            person.Print();
            person = new Sex(person, Sex.Sexs.Man);
            person.Print();
            person = new Stature(person, Stature.Statures.Thin);
            person.Print();
            person = new Activity(person, Activity.Activities.None);
            person.Print();
            Console.ReadKey();
        }
    }
}
