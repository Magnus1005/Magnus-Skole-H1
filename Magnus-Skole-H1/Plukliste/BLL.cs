using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plukliste
{
    public class BLL
    {
        internal string importPath;
        internal string exportPath;
        internal List<string> files;
        internal int index;

        public BLL(string importPath = "import", string exportPath = "export")
        {
            this.importPath = importPath;
            this.exportPath = exportPath;
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
            FileStream file = File.OpenRead(files[indexOfFile]);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Pluklist));
            Pluklist plukliste = (Pluklist?)xmlSerializer.Deserialize(file);
            file.Close();

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
            File.Move(files[index], string.Format(@"import\\{0}", filewithoutPath));
            files.Remove(files[index]);
            if (index == files.Count) index--;
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
    }
}
