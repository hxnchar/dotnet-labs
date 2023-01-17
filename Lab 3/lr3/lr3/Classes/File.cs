using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr3.Classes
{
    internal class File : Prototype
    {
        private static int WeightKoef = 394;
        public Extensions Extension { get; private set; }
        public enum Extensions
        {
            txt,
            png,
            mp4,
            mp3,
            pdf,
            docx,
            pptx,
            xlsx
        }
        public File (File file)
            : base(file)
        {
            Name = file.Name;
            Extension = file.Extension;
            Size = file.Size;
            DateCreated = file.DateCreated;
            DateModified = file.DateModified;
            DateAccessed = file.DateAccessed;
            ReadOnly = file.ReadOnly;
            Hidden = file.Hidden;
        }

        public File(string name, Extensions fileExtension)
            :base(name)
        {
            Name = name;
            Extension = fileExtension;
            Size = name.Length * WeightKoef;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            DateAccessed = DateTime.Now;
            ReadOnly = false;
            Hidden = false;
        }
        public File(string name)
            : base(name)
        {
            Random r = new Random();
            int indexOfLastPoint = name.LastIndexOf('.');
            Name = name.Substring(0, indexOfLastPoint);
            Extension = StringToExtension(name.Substring(indexOfLastPoint + 1));
            Size = name.Length * WeightKoef;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            DateAccessed = DateTime.Now;
            ReadOnly = false;
            Hidden = false;
        }
        public override Prototype Clone() => new File(this);
        private Extensions StringToExtension(string input)
        {
            switch (input)
            {
                case "txt":
                    return Extensions.txt;
                case "png":
                    return Extensions.png;
                case "mp4":
                    return Extensions.mp4;
                case "mp3":
                    return Extensions.mp3;
                case "pdf":
                    return Extensions.pdf;
                case "docx":
                    return Extensions.docx;
                case "pptx":
                    return Extensions.pptx;
                case "xlsx":
                    return Extensions.xlsx;
                default:
                    return Extensions.txt;
            }
        }
        private string ExtensionToString()
        {
            switch (Extension)
            {
                case Extensions.txt:
                    return "txt";
                case Extensions.png:
                    return "png";
                case Extensions.mp4:
                    return "mp4";
                case Extensions.mp3:
                    return "mp3";
                case Extensions.pdf:
                    return "pdf";
                case Extensions.docx:
                    return "docx";
                case Extensions.pptx:
                    return "pptx";
                case Extensions.xlsx:
                    return "xlsx";
                default:
                    return "";
            }
        }
        public override string ToString() => $"{Name}.{ExtensionToString()} ({Size}kb)";
        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
            }
            else
            {
                Console.Write("|-");
            }
            Console.WriteLine(this);
        }
    }
}
