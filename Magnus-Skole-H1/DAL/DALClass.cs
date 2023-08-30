using Newtonsoft.Json;
namespace DAL
{
    public class DALClass
    {
        public static string databasePath = "C:\\Users\\MagnusNissen\\OneDrive - HOUSE4IT A S\\Dokumenter\\GitHub\\Magnus-Skole-H1\\Magnus-Skole-H1\\BLL\\Database.json";
        public static List<Kunde> HentData()
        {            
            string JsonData = File.ReadAllText(databasePath);
            var data = JsonConvert.DeserializeObject<List<Kunde>>(JsonData);
            return data;
        }
        public static void RetLinje(Kunde data)
        {
            var altData = HentData();
            altData.Remove(altData.Where(x => x.cprNummer == data.cprNummer).FirstOrDefault());
            altData.Add(data);

            var JsonData = JsonConvert.SerializeObject(altData);
            File.WriteAllText(databasePath, JsonData);
        }
        public static void SletLinje(Kunde data)
        {
            var altData = HentData();
            altData.Remove(altData.Where(x => x.cprNummer == data.cprNummer).FirstOrDefault());

            var JsonData = JsonConvert.SerializeObject(altData);
            File.WriteAllText(databasePath, JsonData);
        }
        public static void SletAlt(List<Kunde> data)
        {
            var altData = HentData();
            altData.Clear();

            var JsonData = JsonConvert.SerializeObject(altData);
            File.WriteAllText(databasePath, JsonData);
        }
        public static void OpretLinje(Kunde data)
        {
            List<Kunde> altData = new List<Kunde>();
            try
            {
                var hentetData = HentData();
                if (hentetData != null)
                {
                    altData = hentetData;
                }
            }
            catch (Exception)
            {
            }
            altData.Add(data);

            var JsonData = JsonConvert.SerializeObject(altData);
            File.WriteAllText(databasePath, JsonData);
        }

    }
    public class Kunde
    {
        public long cprNummer { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public decimal totalSaldo { get; set; }
        public List<konti> konti { get; set; }
    }

    public class konti
    {
        public string navn { get; set; }
        public decimal saldo { get; set; }
    }
}