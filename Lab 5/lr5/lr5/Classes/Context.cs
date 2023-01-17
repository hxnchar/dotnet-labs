using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5.Classes
{
    public class Context
    {
        public Dictionary<string, Complex> Variables { get; set; }
        public Context()
        {
            Variables = new Dictionary<string, Complex>();
        }
        public void AddVariables(params KeyValuePair<string, Complex>[] keyValuePairs)
        {
            foreach (var pair in keyValuePairs)
            {
                Variables.Add(pair.Key, pair.Value);
            }
        }
    }
}
