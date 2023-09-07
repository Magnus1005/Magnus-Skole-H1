//Eksempel på funktionel kodning hvor der kun bliver brugt et model lag

using System;
using System.Data.SqlTypes;
using System.Reflection;

namespace Plukliste;

class PluklisteProgram
{
    static void Main()
    {
        //Arrange
        char readKey = ' ';
        List<string> files;
        var index = 0;
        var standardColor = Console.ForegroundColor;

        string importPath = "import";
        string exportPath = "export";

        pathCheck(importPath, exportPath);

        files = Directory.EnumerateFiles(exportPath).ToList();

        List<string> options = new List<string>() { "Quit", "Afslut plukseddel", "Forrige plukseddel", "Næste plukseddel", "Genindlæs pluksedler" };

        //ACT
        while (readKey != 'Q')
        {
            if (files.Count == 0)
            {
                Console.WriteLine("No files found.");
            }
            else
            {
                displayPlukliste(index, files);
            }

            //Print options
            dispalyOptions(index, options, files);

            readKey = Console.ReadKey().KeyChar;
            if (readKey >= 'a') readKey -= (char)('a' - 'A'); //HACK: To upper
            Console.Clear();

            index = runAction(files, index, readKey);

        }
    }
    public static void pathCheck(string importPath, string exportPath)
    {
        if (!Directory.Exists(importPath))
        {
            Console.WriteLine($"Directory \"{importPath}\" not found. It has now been created.");
            Directory.CreateDirectory(importPath);
        }

        if (!Directory.Exists(exportPath))
        {
            Console.WriteLine($"Directory \"{exportPath}\" not found. It has now been created.");
            Directory.CreateDirectory(exportPath);
        }
    }
    public static void displayPlukliste(int index, List<string> files)
    {
        Console.WriteLine($"Plukliste {index + 1} af {files.Count}");
        Console.WriteLine($"\nFile: {files[index]}");

        //read file
        FileStream file = File.OpenRead(files[index]);
        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Pluklist));
        Pluklist plukliste = (Pluklist?)xmlSerializer.Deserialize(file);

        //print plukliste
        if (plukliste != null && plukliste.Lines != null)
        {
            Console.WriteLine(plukliste.ToString());

            //TODO: Add adresse to screen print
            Console.WriteLine("\n{0,-7}{1,-9}{2,-20}{3}", "Antal", "Type", "Produktnr.", "Navn");
            foreach (var item in plukliste.Lines)
            {
                Console.WriteLine(item.ToString());
            }
        }
        file.Close();
    }
    public static void dispalyOptions(int index, List<string> options, List<string> files)
    {
        Console.WriteLine("\n\nOptions:");

        foreach (var item in options)
        {
            if (item == "Afslut plukseddel" && index < 0)
            {
                continue;
            }
            if (item == "Forrige plukseddel" && index <= 0)
            {
                continue;
            }
            if(item == "Næste plukseddel" && index >= files.Count - 1)
            {
                continue;
            }
            char firstLetter = item[0];
            string restOfWord = item.Substring(1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(firstLetter);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(restOfWord);
        }
    }
    public static int runAction(List<string> files, int index, char readKey)
    {
        Console.ForegroundColor = ConsoleColor.Red; //status in red
        switch (readKey)
        {
            case 'G':
                files = Directory.EnumerateFiles("export").ToList();
                index = 0;
                Console.WriteLine("Pluklister genindlæst");
                break;
            case 'F':
                if (index > 0) index--;
                break;
            case 'N':
                if (index < files.Count - 1) index++;
                break;
            case 'A':
                //Move files to import directory
                var filewithoutPath = files[index].Substring(files[index].LastIndexOf('\\'));
                File.Move(files[index], string.Format(@"import\\{0}", filewithoutPath));
                Console.WriteLine($"Plukseddel {files[index]} afsluttet.");
                files.Remove(files[index]);
                if (index == files.Count) index--;
                break;
        }
        Console.ForegroundColor = ConsoleColor.White; //reset color
        return index;
    }
}
