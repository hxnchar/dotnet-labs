using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr3.Classes
{
    internal class Folder : Prototype
    {
        public string Path { get; protected set; }
        public override int Size
        {
            get
            {
                return Files.Aggregate(0, (acc, elem) => acc + elem.Size);
            }
            protected set { }
        }
        public virtual List<Prototype> Files { get; protected set; }

        public Folder(Folder folder) 
            : base(folder)
        {
            Name = folder.Name;
            Size = folder.Size;
            Files = folder.Files;
            DateCreated = folder.DateCreated;
            DateModified = folder.DateModified;
            DateAccessed = folder.DateAccessed;
            ReadOnly = folder.ReadOnly;
            Hidden = folder.Hidden;
        }

        public Folder(string name)
            : base(name)
        {
            Name = name;
            Size = 0;
            Files = new List<Prototype>();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            DateAccessed = DateTime.Now;
            ReadOnly = false;
            Hidden = false;
        }

        public override Prototype Clone() => new Folder(this);
        public void AddFile(params Prototype[] files)
        {
            foreach (var file in files)
            {
                Files.Add(file);
            }
        }
        public override string ToString() => $"{Name} ({Files.Count} {(Files.Count == 1 ? "file" : "files")})";

        public void PrintPretty(string indent, bool last)
        {
            List<Folder> folders = new List<Folder>();
            List<File> files = new List<File>();
            foreach (var segment in Files)
            {
                if (segment is Folder)
                    folders.Add((Folder)segment);
                else if (segment is File)
                    files.Add((File)segment);
            }

            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(this);

            for (int i = 0; i < folders.Count; i++)
            {
                folders[i].PrintPretty(indent, i == folders.Count - 1);
            }

            for (int i = 0; i < files.Count; i++)
            {
                files[i].PrintPretty(indent, i == files.Count - 1);
            }
        }
    }
}
