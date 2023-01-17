﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5.Classes.Expressions
{
    public class Addition : IExpression
    {
        public IExpression LeftPart { get; private set; }
        public IExpression RightPart { get; private set; }
        public Addition(IExpression leftPart, IExpression rightPart)
        {
            LeftPart = leftPart;
            RightPart = rightPart;
        }
        public Complex Interpret(Context context) => LeftPart.Interpret(context) + RightPart.Interpret(context);
    }
}
