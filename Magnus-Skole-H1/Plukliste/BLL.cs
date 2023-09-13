using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using CsvHelper;
using CsvHelper.Configuration;

namespace Plukliste
{
    public class BLL
    {
        internal string importPath;
        internal string exportPath;
        internal string letterPath;
        internal string letterTemplatePath;
        internal List<string> files;
        internal int index;

        public BLL(string importPath = "import", string exportPath = "export", string letterPath = "print", string letterTemplatePath = "letterTemplates")
        {
            this.importPath = importPath;
            this.exportPath = exportPath;
            this.letterPath = letterPath;
            this.letterTemplatePath = letterTemplatePath;
            this.index = 0;
            this.files = this.readListOfFiles();
        }
        public List<string> readListOfFiles()
        {
            files = Directory.EnumerateFiles(exportPath).ToList();
            return files;
        }

        public void next()
        {
            if (index < files.Count() - 1)
            {
                index++;
            }
        }
        public void previous()
        {
            if (index > 0)
            {
                index--;
            }
        }
        public void reloadFiles()
        {
            files = readListOfFiles();
        }
        public Pluklist getFile()
        {
            return readFile(files[index]);
        }
        public Pluklist readFile(string filename)
        {
            int indexOfFile = files.IndexOf(filename);

            string fileExtension = Path.GetExtension(filename).ToLower();


            Pluklist plukliste = new Pluklist();

            IPluklistReader reader = PluklisteReader.GetReader(filename);
            plukliste = reader.ReadFile(filename);


            //if (fileExtension == ".xml")
            //{
            //    FileStream file = File.OpenRead(files[indexOfFile]);
            //    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Pluklist));
            //    plukliste = (Pluklist?)xmlSerializer.Deserialize(file);
            //    file.Close();
            //}
            //else if(fileExtension == ".csv")
            //{
            //    string[] csvLines = File.ReadAllLines(filename);

            //    // Skip the header row if it exists
            //    if (csvLines.Length > 0)
            //    {
            //        csvLines = csvLines.Skip(1).ToArray();
            //    }

            //    // Parse each line into a Person object
            //    Pluklist pluklistTemp = new Pluklist();
            //    List<Item> items = new List<Item>();
            //    foreach (string csvLine in csvLines)
            //    {
            //        string[] csvValues = csvLine.Split(';');
            //        if (csvValues.Length == 4)
            //        {
            //            var tempItem = new Item
            //            {
            //                ProductID = csvValues[0],
            //                Type = (ItemType)Enum.Parse(typeof(ItemType), csvValues[1]),
            //                Title = csvValues[2],
            //                Amount = Convert.ToInt32(csvValues[3])
            //            };


            //            items.Add(tempItem);
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Skipping invalid csvLine: {csvLine}");
            //        }
            //    }

            //    pluklistTemp.Lines = items;
            //    pluklistTemp.Adresse = "Firma bil";
            //    pluklistTemp.Forsendelse = "Pickup";
            //    pluklistTemp.Name = "";

            //    int indexOfUnderscore = filename.IndexOf('_') + 1;
            //    int indexOfDot = filename.IndexOf('.');

            //    pluklistTemp.Name = filename.Substring(indexOfUnderscore, indexOfDot - indexOfUnderscore).Replace("_", " ");
            //    plukliste = pluklistTemp;
            //}
            

            StatusData statusData = new StatusData();
            statusData.fileIndex = indexOfFile;
            statusData.fileName = filename;
            statusData.totalFileCount = files.Count;

            plukliste.Status = statusData;

            return plukliste;
        }
        public string moveFile()
        {
            //Move files to import directory
            var filewithoutPath = files[index].Substring(files[index].LastIndexOf('\\'));

            Pluklist fileData = readFile(files[index]);

            File.Move(files[index], string.Format(@"import\\{0}", filewithoutPath));
            files.Remove(files[index]);
            if (index == files.Count) index--;

            foreach(var line in fileData.Lines)
            {
                if(line.Type == ItemType.Print)
                {
                    if (File.Exists(letterTemplatePath + "\\" + line.ProductID + ".html") == true)
                    {
                        printLetter(line.ProductID, fileData);
                    }                    
                }
            }


            return $"Plukseddel {files[index]} afsluttet.";

        }
        public List<string> getOptions()
        {
            List<string> options = new List<string>() { "Quit", "Afslut plukseddel", "Genindlæs pluksedler" };

            if (index > 0)
            {
                options.Add("Forrige plukseddel");
            }
            if (index < files.Count - 1)
            {
                options.Add("Næste plukseddel");
            }
            return options;
        }   

        internal void printLetter(string letterName, Pluklist fileData)
        {
            string letter = File.ReadAllText(letterTemplatePath + "\\" + letterName + ".html");
            letter = letter.Replace("[Name]", fileData.Name);
            letter = letter.Replace("[Adresse]", fileData.Adresse);

            string plukliste = "<style>\r\n  th, td {\r\n    padding: 8px; /* Optional: Add padding to cells for better readability */\r\n  }\r\n</style><table>\r\n  <tr>\r\n    <th style=\"text-align: left;\">Navn</th>\r\n    <th style=\"text-align: left;\">Antal</th>\r\n    <th style=\"text-align: left;\">Sku</th>\r\n  </tr>";
            foreach (var line in fileData.Lines)
            {
                if (line.Type == ItemType.Fysisk)
                {
                    plukliste += $"<tr>\r\n    <td>{line.Title}</td>\r\n    <td>{line.Amount}</td>\r\n    <td>{line.ProductID}</td>\r\n  </tr>";
                }
            }
            plukliste += "</table>";

            letter = letter.Replace("[Plukliste]", plukliste);
            File.WriteAllText(letterPath + "\\" + fileData.Name + "_" + letterName + ".html", letter);
        }   
    }
}
