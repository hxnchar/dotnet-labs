using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4.MyClasses
{
    public class Sex : Decorator
    {
        public Sexs PersonSex;

        public Sex(IPerson person, Sexs sex) : base(person)
        {
            PersonSex = sex;
        }

        public enum Sexs
        {
            Man,
            Woman,
            Unknown
        }
        private string SexToString()
        {
            switch (PersonSex)
            {
                case Sexs.Man:
                    return "Man";
                case Sexs.Woman:
                    return "Woman";
                case Sexs.Unknown:
                    return "Unknown";
                default:
                    return "";
            }
        }
        public override int CalculateCalories()
        {
            double coef = 1;
            switch (PersonSex)
            {
                case Sexs.Man:
                    coef = 1.1;
                    break;
                case Sexs.Woman:
                    coef = 1.05;
                    break;
                case Sexs.Unknown:
                    coef = 1.07;
                    break;
                default:
                    break;
            }
            return (int)Math.Round(Person.CalculateCalories() * coef, 0);
        }

        public override string ToString() => $"{Person}, Sex: {SexToString()}";
        public override void Print() => Console.WriteLine($"{ToString()}\nTOTAL: {CalculateCalories()}kkal");
    }
}
