using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5.Classes.Expressions
{
    internal class Terminal : IExpression
    {
        public string Name { get; private set; }

        public Terminal(string name)
        {
            Name = name;
        }
        public Complex Interpret(Context context)
        {
            return context.Variables[Name];
        }
    }
}
