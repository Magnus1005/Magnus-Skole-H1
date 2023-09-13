using Plukliste;

public interface IPluklistReader
{
    Pluklist ReadFile(string filename);
}

public class XmlReader : IPluklistReader
{
    public Pluklist ReadFile(string filename)
    {
        //int indexOfFile = files.IndexOf(filename);
        FileStream file = File.OpenRead(filename); // files[indexOfFile]
        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Pluklist));
        var plukliste = (Pluklist?)xmlSerializer.Deserialize(file);
        file.Close();
        return plukliste; // Replace with actual logic
    }
}

public class CsvReader : IPluklistReader
{
    public Pluklist ReadFile(string filename)
    {
        string[] csvLines = File.ReadAllLines(filename);

        // Skip the header row if it exists
        if (csvLines.Length > 0)
        {
            csvLines = csvLines.Skip(1).ToArray();
        }

        // Parse each line into a Person object
        Pluklist pluklistTemp = new Pluklist();
        List<Item> items = new List<Item>();
        foreach (string csvLine in csvLines)
        {
            string[] csvValues = csvLine.Split(';');
            if (csvValues.Length == 4)
            {
                var tempItem = new Item
                {
                    ProductID = csvValues[0],
                    Type = (ItemType)Enum.Parse(typeof(ItemType), csvValues[1]),
                    Title = csvValues[2],
                    Amount = Convert.ToInt32(csvValues[3])
                };


                items.Add(tempItem);
            }
            else
            {
                Console.WriteLine($"Skipping invalid csvLine: {csvLine}");
            }
        }

        pluklistTemp.Lines = items;
        pluklistTemp.Adresse = "Firma bil";
        pluklistTemp.Forsendelse = "Pickup";
        pluklistTemp.Name = "";

        int indexOfUnderscore = filename.IndexOf('_') + 1;
        int indexOfDot = filename.IndexOf('.');

        pluklistTemp.Name = filename.Substring(indexOfUnderscore, indexOfDot - indexOfUnderscore).Replace("_", " ");
        return pluklistTemp;
    }
}

public static class PluklisteReader
{
    public static IPluklistReader GetReader(string filename)
    {
        string fileExtension = Path.GetExtension(filename).ToLower();
        if (fileExtension == ".xml")
        {
            return new XmlReader();
        }
        else if (fileExtension == ".csv")
        {
            return new CsvReader();
        }
        else
        {
            throw new Exception("Unknown file type");
        }
    }
}