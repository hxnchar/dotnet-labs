using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5.Classes.Expressions
{
    public class Equality : IExpression
    {
        public IExpression LeftPart { get; private set; }
        public IExpression RightPart { get; private set; }
        public Equality(IExpression leftPart, IExpression rightPart)
        {
            LeftPart = leftPart;
            RightPart = rightPart;
        }
        public bool Interpret (Context context) => LeftPart.Interpret(context) == RightPart.Interpret(context);
        Complex IExpression.Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
