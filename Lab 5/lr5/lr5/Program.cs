using lr5.Classes;
using lr5.Classes.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();

            Complex a = new Complex(1, 2);
            Complex b = new Complex(5, 6);
            Complex c = new Complex(3, 4);
            Complex likeA = new Complex(1, 2);
            Complex notLikeA = new Complex(2, 1);

            context.AddVariables(
                new KeyValuePair<string, Complex>("a", a),
                new KeyValuePair<string, Complex>("b", b),
                new KeyValuePair<string, Complex>("c", c),
                new KeyValuePair<string, Complex>("likeA", likeA),
                new KeyValuePair<string, Complex>("notLikeA", notLikeA)
            );
            var result1 = new Addition(
                new Terminal("a"),
                new Subtraction(
                    new Terminal("b"),
                    new Terminal("c")
                    )
                ).Interpret(context);
            Console.WriteLine($"{a} + {b} - ({c}) = {result1}");


            var expression2 = new Equality(
                new Terminal("a"),
                new Terminal("likeA")
            ).Interpret(context);
            Console.WriteLine($"({a}) and ({likeA}) are {(expression2 ? "equal" : "not equal")}");

            var expression3 = new Equality(
                new Terminal("a"),
                new Terminal("notLikeA")
            ).Interpret(context);
            Console.WriteLine($"({a}) and ({notLikeA}) are {(expression3 ? "equal" : "not equal")}");

            Console.ReadKey();
        }
    }
}
