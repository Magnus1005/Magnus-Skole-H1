using Newtonsoft.Json;
using System;

namespace AgileUdvikling
{
    public class Program
    {
        static void Main(string[] args)
        {
            string databasePath = "C:\\Users\\MagnusNissen\\OneDrive - HOUSE4IT A S\\Dokumenter\\GitHub\\Magnus-Skole-H1\\Magnus-Skole-H1\\AgileUdvikling\\MedarbejderIdDB.JSON";
            string firstName = "";
            string lastName = "";

            Console.Write("First name: ");
            firstName = Console.ReadLine().ToLower();
            firstName = FirstCharToUpper(firstName);

            Console.Write("Last name: ");
            lastName = Console.ReadLine().ToLower();
            lastName = FirstCharToUpper(lastName);


            if (firstName.Length < 4 )
            {
                while ( firstName.Length < 4 )
                {
                    firstName += "X";
                }
            }
            if (lastName.Length < 4)
            {
                while (lastName.Length < 4)
                {
                    lastName += "X";
                }
            }

            string JsonData = File.ReadAllText(databasePath);

            List<string> data = new List<string>();

            if (JsonData != "")
            {
                data = JsonConvert.DeserializeObject<List<string>>(JsonData);
            }            

            Random random = new Random();

            int medarbjederNummer = 0;

            string medarbjeder = firstName.Substring(0, 4) + lastName.Substring(0, 4);

            string medarbjderId = "";

            bool isCreated = false;

            do
            {
                medarbjederNummer = random.Next(0, 100);

                int Findes = data.Where(x => x == medarbjeder + medarbjederNummer.ToString()).Count();

                if (Findes == 0)
                {
                    medarbjderId = medarbjeder + medarbjederNummer.ToString();
                    isCreated = true;
                }               
            }
            while (isCreated == false);

            data.Add(medarbjderId);

            Console.WriteLine();

            Console.WriteLine($"MedarbejderId: {medarbjderId}");

            var JsonDataToUpload = JsonConvert.SerializeObject(data);
            File.WriteAllText(databasePath, JsonDataToUpload);

        }
        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}