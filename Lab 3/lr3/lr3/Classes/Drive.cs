using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr3.Classes
{
    internal class Drive
    {
        public char Letter { get; private set; }
        public List<Prototype> Data { get; private set; }
        public Drive (char letter)
        {
            Letter = letter.ToString().ToUpper().ToCharArray()[0];
            Data = new List<Prototype> ();
        }
        public void AddData(params Prototype[] files)
        {
            foreach (var file in files)
            {
                Data.Add(file);
            }
        }
        public void Print()
        {
            Console.Write($"\\-{Letter}\n");
            PrintPretty("  ", true);
        }
        public void PrintPretty(string indent, bool last)
        {
            List<Folder> folders = new List<Folder>();
            List<File> files = new List<File>();
            foreach (var segment in Data)
            {
                if (segment is Folder)
                    folders.Add((Folder)segment);
                else if (segment is File)
                    files.Add((File)segment);
            }
            foreach (Folder folder in folders)
            {
                folder.PrintPretty(indent, true);
            }
            foreach (var file in files)
            {
                file.PrintPretty(indent, true);
            }
        }
        public void RemoveFile(string path)
        {
            // D\\EDU\\Notes\\[NOTES]History
            List<string> folders = path.Split('\\').ToList();
            string fileName = folders[folders.Count - 1];
            folders.RemoveAt(folders.Count - 1);
            folders.RemoveAt(0);
            Prototype current;
            /*while (folders.Count != 0)
            {
                current = Data.ElementAt(Data.FindIndex(t => t.Name == folders[0]));
                folders.RemoveAt(0);
            }
            int indexOfFile = Files.FindIndex(t => t.Name == fileName);
            if (indexOfFile == -1)
            {
                Console.WriteLine("No such file in this folder");
            }
            else
            {
                Files.RemoveAt(indexOfFile);
            }*/
        }
        public void CopyAndPaste(string pathFrom, string pathTo)
        {
            List<string> pathFromSplitted = pathFrom.Split('\\').ToList();
            string prototypeName = pathFromSplitted[pathFromSplitted.Count - 1].Split('.')[0];
            pathFromSplitted.RemoveAt(pathFromSplitted.Count - 1);
            pathFromSplitted.RemoveAt(0);
            int tempIndex;
            Prototype currentFolder = null;
            do
            {
                tempIndex = (((Folder)currentFolder)?.Files ?? Data).FindIndex(t => t.Name == pathFromSplitted[0]);
                currentFolder = (((Folder)currentFolder)?.Files ?? Data)[tempIndex];
                pathFromSplitted.RemoveAt(0);
            } while (pathFromSplitted.Count != 0);
            
            int prototypeIndex = ((Folder)currentFolder).Files.FindIndex(t => t.Name == prototypeName);
            Prototype prototype = ((Folder)currentFolder).Files[prototypeIndex];
            List<string> pathToSplitted = pathTo.Split('\\').ToList();
            pathToSplitted.RemoveAt(0);
            currentFolder = null;
            do
            {
                tempIndex = (((Folder)currentFolder)?.Files ?? Data).FindIndex(t => t.Name == pathToSplitted[0]);
                currentFolder = (((Folder)currentFolder)?.Files ?? Data)[tempIndex];
                pathToSplitted.RemoveAt(0);
            } while (pathToSplitted.Count != 0);
            ((Folder)currentFolder).AddFile(prototype.Clone());
        }

        public void CutAndPaste(string pathFrom, string pathTo)
        {
            List<string> pathFromSplitted = pathFrom.Split('\\').ToList();
            string prototypeName = pathFromSplitted[pathFromSplitted.Count - 1].Split('.')[0];
            pathFromSplitted.RemoveAt(pathFromSplitted.Count - 1);
            pathFromSplitted.RemoveAt(0);
            int tempIndex;
            Prototype currentFolder = null;
            do
            {
                tempIndex = (((Folder)currentFolder)?.Files ?? Data).FindIndex(t => t.Name == pathFromSplitted[0]);
                currentFolder = (((Folder)currentFolder)?.Files ?? Data)[tempIndex];
                pathFromSplitted.RemoveAt(0);
            } while (pathFromSplitted.Count != 0);
            int prototypeIndex = ((Folder)currentFolder).Files.FindIndex(t => t.Name == prototypeName);
            Prototype prototype = ((Folder)currentFolder).Files[prototypeIndex].Clone();
            ((Folder)currentFolder).Files.RemoveAt(prototypeIndex);
            List<string> pathToSplitted = pathTo.Split('\\').ToList();
            pathToSplitted.RemoveAt(0);
            currentFolder = null;
            do
            {
                tempIndex = (((Folder)currentFolder)?.Files ?? Data).FindIndex(t => t.Name == pathToSplitted[0]);
                currentFolder = (((Folder)currentFolder)?.Files ?? Data)[tempIndex];
                pathToSplitted.RemoveAt(0);
            } while (pathToSplitted.Count != 0);
            ((Folder)currentFolder).AddFile(prototype);
        }
    }
}
