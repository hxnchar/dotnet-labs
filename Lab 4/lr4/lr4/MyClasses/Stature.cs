using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4.MyClasses
{
    internal class Stature : Decorator
    {
        public Statures PersonStature;

        public Stature(IPerson person, Statures stature) : base(person)
        {
            PersonStature = stature;
        }

        public enum Statures
        {
            Thin,
            Normal,
            Fat
        }
        private string StatureToString()
        {
            switch (PersonStature)
            {
                case Statures.Thin:
                    return "Thin";
                case Statures.Normal:
                    return "Normal";
                case Statures.Fat:
                    return "Fat";
                default:
                    return "";
            }
        }
        public override int CalculateCalories()
        {
            double coef = 1;
            switch (PersonStature)
            {
                case Statures.Thin:
                    coef = 1.05;
                    break;
                case Statures.Normal:
                    coef = 1;
                    break;
                case Statures.Fat:
                    coef = 0.95;
                    break;
                default:
                    break;
            }
            return (int)Math.Round(Person.CalculateCalories() * coef, 0);
        }
        public override string ToString() => $"{Person}, Stature: {StatureToString()}";
        public override void Print() => Console.WriteLine($"{ToString()}\nTOTAL: {CalculateCalories()}kkal");
    }
}
