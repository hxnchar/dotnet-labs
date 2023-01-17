using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4.MyClasses
{
    public class Activity : Decorator
    {
        public Activities PersonActivity;

        public Activity(IPerson person, Activities activity) : base(person)
        {
            PersonActivity = activity;
        }

        public enum Activities
        {
            None,
            Low,
            Medium,
            High
        }
        private string ActivityToString()
        {
            switch (PersonActivity)
            {
                case Activities.None:
                    return "None";
                case Activities.Low:
                    return "Low";
                case Activities.Medium:
                    return "Medium";
                case Activities.High:
                    return "High";
                default:
                    return "";
            }
        }
        public override int CalculateCalories()
        {
            double coef = 1;
            switch (PersonActivity)
            {
                case Activities.None:
                    coef = 0.75;
                    break;
                case Activities.Low:
                    coef = 0.85;
                    break;
                case Activities.Medium:
                    coef = 0.9;
                    break;
                case Activities.High:
                    coef = 0.95;
                    break;
                default:
                    break;
            }
            return (int)Math.Round(Person.CalculateCalories() * coef, 0);
        }
        public override string ToString() => $"{Person}, Activity: {ActivityToString()}";
        public override void Print() => Console.WriteLine($"{ToString()}\nTOTAL: {CalculateCalories()}kkal");
    }
}
