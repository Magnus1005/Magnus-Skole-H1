using System;
using System.Data.SqlTypes;
using System.Reflection;

namespace Plukliste;

class PluklisteProgram
{
    public static BLL _bll;
    public static char readKey;
    static void Main()
    {
        string importPath = "import";
        string exportPath = "export";
        string letterPath = "print";
        string letterTemplatePath = "letterTemplates";

        pathCheck(importPath);
        pathCheck(exportPath);
        pathCheck(letterPath);
        _bll = new BLL(importPath, exportPath);
        //Arrange
        readKey = ' ';


        //ACT
        while (readKey != 'Q')
        {
            if (_bll.readListOfFiles().Count == 0)
            {
                Console.WriteLine("No files found.");
            }
            else
            {
                displayPlukliste();
            }

            //Print options
            dispalyOptions();

            readKey = Console.ReadKey().KeyChar;
            if (readKey >= 'a') readKey -= (char)('a' - 'A'); //HACK: To upper
            Console.Clear();

            runAction();

        }
    }
    public static void pathCheck(string path)
    {
        if (!Directory.Exists(path))
        {
            Console.WriteLine($"Directory \"{path}\" not found. It has now been created.");
            Directory.CreateDirectory(path);
        }
    }
    public static void displayPlukliste()
    {

        Pluklist plukliste = _bll.getFile();
        Console.WriteLine($"Plukliste {plukliste.Status.fileIndex + 1} af {plukliste.Status.totalFileCount}");
        Console.WriteLine($"\nFile: {plukliste.Status.fileName}");

        //print plukliste
        if (plukliste != null && plukliste.Lines != null)
        {
            Console.WriteLine(plukliste.ToString());

            Console.WriteLine("\n{0,-7}{1,-9}{2,-20}{3}", "Antal", "Type", "Produktnr.", "Navn");
            foreach (var item in plukliste.Lines)
            {
                Console.WriteLine(item.ToString());
            }
        }
        //file.Close();
    }
    public static void dispalyOptions()
    {
        Console.WriteLine("\n\nOptions:");
        List<string> options = _bll.getOptions();

        foreach (var item in options)
        {
            char firstLetter = item[0];
            string restOfWord = item.Substring(1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(firstLetter);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(restOfWord);
        }
    }
    public static void runAction()
    {        
        List<string> options = _bll.getOptions();

        bool validOption = false;

        foreach (var item in options)
        {
            if (item[0] == readKey)
            {
                validOption = true;
            }
        }
        if (validOption == false)
        {
            return;
        }
        Console.ForegroundColor = ConsoleColor.Red; //status in red

        switch (readKey)
        {
            case 'G':
                _bll.reloadFiles();
                Console.WriteLine("Pluklister genindlæst");
                break;
            case 'F':
                _bll.previous();
                break;
            case 'N':
                _bll.next();
                break;
            case 'A':
                //Move files to import directory
                string response = _bll.moveFile();
                Console.WriteLine(response);
                break;
        }
        Console.ForegroundColor = ConsoleColor.White; //reset color
        return;
    }
}
