using System.Xml.Linq;

namespace Plukliste;
public class Pluklist
{
    public string Name { get; set; }
    public string Forsendelse { get; set; }
    public string Adresse { get; set; }
    public List<Item> Lines { get; set; } = new List<Item>();
    public StatusData Status { get; set; } = new StatusData();
    public void AddItem(Item item) { Lines.Add(item); }

    public override string ToString()
    {
        string name = String.Format("\n{0, -13}{1}", "Name:", Name);
        string forsendelse = String.Format("\n{0, -13}{1}", "Forsendelse:", Forsendelse);
        string adresse = String.Format("\n{0, -13}{1}", "Adresse:", Adresse);
        return name + forsendelse + adresse;
    }
}

public class Item
{
    public string ProductID { get; set; }
    public string Title { get; set; }
    public ItemType Type { get; set; }
    public int Amount { get; set; }
    public override string ToString()
    {
        return String.Format("{0,-7}{1,-9}{2,-20}{3}", Amount, Type, ProductID, Title); ;
    }
}
public class StatusData
{
    public int fileIndex { get; set; }
    public string fileName { get; set; }
    public string filePath { get; set; }
    public int totalFileCount { get; set; }
}

public enum ItemType
{
    Fysisk, Print
}






