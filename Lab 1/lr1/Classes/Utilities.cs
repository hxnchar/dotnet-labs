using System;
using System.Collections.Generic;
using System.Text;

namespace lr1.Classes
{
    class Utilities
    {
        static string OutLine(int count = 30)
        {
            string result = "\n";
            for (int i = 0; i < count; i++)
            {
                result += "=";
            }
            return result + '\n';
        }

        public static void OutText(string text)
        {
            Console.WriteLine($"{OutLine()}{text}{OutLine()}");
        }
    }
}
