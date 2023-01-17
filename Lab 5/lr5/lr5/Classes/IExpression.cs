using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5.Classes
{
    public interface IExpression
    {
        public Complex Interpret(Context context);
    }
}
