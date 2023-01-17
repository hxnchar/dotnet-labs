using lr3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lr3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nINITIAL\n");
            Drive drive = new Drive('d');

            Folder imagesFolder = new Folder("Images");
            File image1 = new File("Kyiv.png");
            File image2 = new File("Dnipro.png");
            File image3 = new File("Kharkiv.png");
            imagesFolder.AddFile(image1, image2, image3);

            Folder eduFolder = new Folder("EDU");
            Folder tests = new Folder("Tests");
            File test1 = new File("Test 1.docx");
            File test2 = new File("Test 2.docx");
            File test3 = new File("Math test.txt");
            tests.AddFile(test1, test2, test3);
            Folder notes = new Folder("Notes");
            File note1 = new File("[NOTES]History.docx");
            File note2 = new File("[NOTES]Ukrainian literature.docx");
            File schedule = new File("Schedule.xlsx");
            notes.AddFile(note1, note2);
            eduFolder.AddFile(schedule, tests, notes);

            Folder workFolder = new Folder("Work");

            drive.AddData(workFolder, imagesFolder, eduFolder);
            drive.Print();

            Console.WriteLine("\nCOPIED FILE\n");
            drive.CopyAndPaste("D\\EDU\\Tests\\Test 2.docx", "D\\Work");
            drive.Print();

            Console.WriteLine("\nCOPIED FOLDER\n");
            drive.CopyAndPaste("D\\EDU\\Notes", "D\\Work");
            drive.Print();

            Console.WriteLine("\nCUT AND PASTE\n");
            drive.CutAndPaste("D\\EDU\\Tests\\Test 1.docx", "D\\Work");
            drive.Print();

            Console.ReadKey();
        }
    }
}
