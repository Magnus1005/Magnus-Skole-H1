using System.Xml.Linq;

namespace Plukliste;
public class Pluklist
{
    public string Name;
    public string Forsendelse;
    public string Adresse;
    public List<Item> Lines = new List<Item>();
    public void AddItem(Item item) { Lines.Add(item); }

    public override string ToString()
    {
        string name = String.Format("\n{0, -13}{1}", "Name:", Name);
        string forsendelse = String.Format("\n{0, -13}{1}", "Forsendelse:", Forsendelse);
        return name + forsendelse;
    }
}

public class Item
{
    public string ProductID;
    public string Title;
    public ItemType Type;
    public int Amount;
    public override string ToString()
    {
        return String.Format("{0,-7}{1,-9}{2,-20}{3}", Amount, Type, ProductID, Title); ;
    }
}

public enum ItemType
{
    Fysisk, Print
}



