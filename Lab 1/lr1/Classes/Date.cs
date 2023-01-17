using System;
using System.Collections.Generic;
using System.Text;

namespace lr1.Classes
{
    class Date
    {
        public int Day, Month, Year;
        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
        public Date(string input)
        {
            try
            {
                string[] splitted = input.Split('.');
                Day = int.Parse(splitted[0]);
                Month = int.Parse(splitted[1]);
                Year = int.Parse(splitted[2]);
            }
            catch
            {
                Day = 1;
                Month = 1;
                Year = 1970;
            }
        }

        public int CompareTo(Date date)
        {
            if (Year > date.Year)
            {
                return 1;
            }
            else if (Year < date.Year)
            {
                return -1;
            }
            else if (Month > date.Month)
            {
                return 1;
            }
            else if (Month < date.Month)
            {
                return -1;
            }
            else if (Day > date.Day)
            {
                return 1;
            }
            else if (Day < date.Day)
            {
                return -1;
            }
            else
                return 0;
        }


        public static List<Date> GenerateDates(int count)
        {
            List<Date> dates = new List<Date>();
            Random r = new Random();
            for (int i = 0; i < count; i++)
            {
                dates.Add(new Date(r.Next(1, 21), r.Next(1, 13), r.Next(2019, 2022)));
            }
            dates.Sort((a, b) => a.CompareTo(b));
            return dates;
        }

        string Format(int num) => num >= 1 && num <= 9 ? $"0{num}" : num.ToString();

        public override string ToString() => $"{Format(Day)}.{Format(Month)}.{Format(Year)}";
    }
}
