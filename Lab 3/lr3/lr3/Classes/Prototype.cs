using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr3.Classes
{
    abstract class Prototype
    {
        protected readonly Random Random = new Random();
        public string Name { get; protected set; }
        public virtual int Size { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime DateModified { get; protected set; }
        public DateTime DateAccessed { get; protected set; }
        public bool ReadOnly { get; protected set; }
        public bool Hidden { get; protected set; }
        public Prototype(Prototype prototype)
        {
            Name = prototype.Name;
            Size = prototype.Size;
            DateCreated = prototype.DateCreated;
            DateModified = prototype.DateModified;
            DateAccessed = prototype.DateAccessed;
            ReadOnly = prototype.ReadOnly;
            Hidden = prototype.Hidden;
        }
        public Prototype(string name)
        {
            Name = name;
            Size = 0;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            DateAccessed = DateTime.Now;
            ReadOnly = false;
            Hidden = false;
        }
        public abstract Prototype Clone();
    }
}
