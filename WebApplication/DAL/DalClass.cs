using Newtonsoft.Json;

namespace DAL
{
    public class DalClass
    {
        public static List<DataList> HentData()
        {
            string JsonData = File.ReadAllText("C:\\Users\\MagnusNissen\\OneDrive - HOUSE4IT A S\\Dokumenter\\GitHub\\Magnus-Skole-H1\\WebApplication\\DAL\\MOCK_DATA.json");
            var data = JsonConvert.DeserializeObject<List<DataList>>(JsonData);
            return data;
        }

        public static void RetLinjeJson(DAL.DalClass.DataList data)
        {
            var altData = HentData();
            altData.Remove(altData.Where(x => x.id == data.id).FirstOrDefault());
            altData.Add(data);

            var JsonData = JsonConvert.SerializeObject(altData);
            File.WriteAllText("C:\\Users\\MagnusNissen\\OneDrive - HOUSE4IT A S\\Dokumenter\\GitHub\\Magnus-Skole-H1\\WebApplication\\DAL\\MOCK_DATA.json", JsonData);
        }
        public static void RetAlt(List<DAL.DalClass.DataList> data)
        {
            var JsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText("C:\\Users\\MagnusNissen\\OneDrive - HOUSE4IT A S\\Dokumenter\\GitHub\\Magnus-Skole-H1\\WebApplication\\DAL\\MOCK_DATA.json", JsonData);
        }
        public static void SletLinje(DAL.DalClass.DataList data)
        {
            var altData = HentData();
            altData.Remove(altData.Where(x => x.id == data.id).FirstOrDefault());

            var JsonData = JsonConvert.SerializeObject(altData);
            File.WriteAllText("C:\\Users\\MagnusNissen\\OneDrive - HOUSE4IT A S\\Dokumenter\\GitHub\\Magnus-Skole-H1\\WebApplication\\DAL\\MOCK_DATA.json", JsonData);
        }
        public static void SletAlt(List<DAL.DalClass.DataList> data)
        {
            var altData = HentData();
            altData.Clear();

            var JsonData = JsonConvert.SerializeObject(altData);
            File.WriteAllText("C:\\Users\\MagnusNissen\\OneDrive - HOUSE4IT A S\\Dokumenter\\GitHub\\Magnus-Skole-H1\\WebApplication\\DAL\\MOCK_DATA.json", JsonData);
        }
        public static void OpretLinje(DAL.DalClass.DataList data)
        {
            var altData = HentData();
            altData.Add(data);

            var JsonData = JsonConvert.SerializeObject(altData);
            File.WriteAllText("C:\\Users\\MagnusNissen\\OneDrive - HOUSE4IT A S\\Dokumenter\\GitHub\\Magnus-Skole-H1\\WebApplication\\DAL\\MOCK_DATA.json", JsonData);
        }

        public class DataList
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }


    }
}