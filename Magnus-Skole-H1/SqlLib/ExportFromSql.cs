using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLib
{
    public class ExportFromSql
    {
        private string templatePath;
        private string exportPath;
        public ExportFromSql(string templatePath = "C:\\PluklisteOpgave\\letterTemplates", string exportPath = "C:\\PluklisteOpgave\\export")
        {
            this.templatePath = templatePath;
            this.exportPath = exportPath;
        }
        public void Export(int id)
        {
            SQL _sql = new SQL();
            Pluklister plukliste = _sql.GetPlukliste(id);
            ExportXml(plukliste);

            if (plukliste.linjer.Count > 0)
            {
                List<string> files = Directory.EnumerateFiles(templatePath).ToList();
                if (files.Count > 0)
                {
                    foreach(var line in plukliste.linjer)
                    {
                        var exists = files.Where(x => Path.GetFileNameWithoutExtension(x) == line.vare).FirstOrDefault();
                        if (exists != null)
                        {
                            ExportPrints(plukliste, files);
                            break;
                        }
                    }
                }
            }
            
            
        }

        private void ExportXml(Pluklister plukliste)
        {
            string filePath = Path.Combine(exportPath, $"{plukliste.kunde_navn.Replace(" ", "") + "_" + plukliste.plukliste_id}.xml");
            // Create an instance of XmlSerializer for the Pluklister type
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Pluklister));

            // Create or open the file for writing
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                // Serialize the Pluklister object to XML and write it to the file
                xmlSerializer.Serialize(fileStream, plukliste);
            }
        }

        //private void ExportXml(Pluklister plukliste)
        //{
        //    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Pluklister));
        //    var upload = (Pluklister?)xmlSerializer.Serialize(file, Pluklister);
        //    file.Close();
        //}   
        private void ExportPrints(Pluklister pluklisteData, List<string> files)
        {            
            foreach (var pluklisteLine in pluklisteData.linjer)
            {               
                var filePath = files.Where(x => Path.GetFileNameWithoutExtension(x) == pluklisteLine.vare).FirstOrDefault();
                if (filePath != null)
                {
                    string letter = File.ReadAllText(filePath);

                    letter = letter.Replace("[Name]", pluklisteData.kunde_navn);
                    letter = letter.Replace("[Adresse]", pluklisteData.kunde_adresse);

                    string plukliste = "<style>\r\n  th, td {\r\n    padding: 8px; /* Optional: Add padding to cells for better readability */\r\n  }\r\n</style><table>\r\n  <tr>\r\n    <th style=\"letter-align: left;\">Navn</th>\r\n    <th style=\"letter-align: left;\">Antal</th>\r\n    <th style=\"letter-align: left;\">Sku</th>\r\n  </tr>";
                    foreach (var line in pluklisteData.linjer)
                    {
                        if (line.vare_type == "Fysisk")
                        {
                            plukliste += $"<tr>\r\n    <td>{line.navn}</td>\r\n    <td>{line.antal}</td>\r\n    <td>{line.vare}</td>\r\n  </tr>";
                        }
                    }
                    plukliste += "</table>";

                    letter = letter.Replace("[Plukliste]", plukliste);

                    File.WriteAllText(Path.Combine(exportPath, $"{pluklisteData.kunde_navn.Replace(" ", "") + "_" + pluklisteData.plukliste_id + "_" + pluklisteLine.vare}.html"), letter);
                }
            }
        }
    }
}
