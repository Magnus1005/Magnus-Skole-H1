using BLL;
using DAL;

namespace Front
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BllClass _bll = new BllClass();
            StartMenu(_bll);                     
        }
        public static void StartMenu(BllClass _bll)
        {
            List<string> OptionsStart = new List<string>() { "Kunder", "Opret kunde", "Exit" };

            bool stopStartMenu = false;
            while (stopStartMenu == false)
            {

                int menuValgt = ChooseOptions(OptionsStart, "Velkommen til det lille pengeinstitut.", false);
                if (menuValgt == OptionsStart.IndexOf("Exit"))
                {
                    System.Environment.Exit(1);
                }

                else
                {
                    switch (menuValgt)
                    {
                        case 0:
                            // Kunder
                            StartMenuKunder(_bll);
                            break;
                        case 1:
                            // Opret kunde
                            StartMenuOpretKunde(_bll);
                            break;
                        default:
                            Console.WriteLine("Error");
                            break;
                    }


                }
            }
        }

        public static void StartMenuKunder(BllClass _bll)
        {

            

            bool stopStartMenuKunde = false;
            while (stopStartMenuKunde == false)
            {
                var kundeData = _bll.HentAltData();
                int kundeNummer = ChooseOptions(kundeData, "Vælg en kunde.\n", true);
                if (kundeNummer != -1)
                {
                    var aktuelKunde = kundeData[kundeNummer];
                    string prompt = $"Navn: {aktuelKunde.firstName} {aktuelKunde.lastName}\nCPR: {aktuelKunde.cprNummer}\nSaldo: {aktuelKunde.totalSaldo}\n";

                    List<string> optionsKundeMenu = new List<string>() { "Slet kunde", "Konti", "Opret Konti", "Slet Konti", "Indbetal", "Udbetal" };


                    bool stopKundeMenuKundeChoose = false;

                    while(stopKundeMenuKundeChoose == false)
                    {
                        int kundeAction = ChooseOptions(optionsKundeMenu, prompt, true);
                        //if (kundeAction == optionsKundeMenu.IndexOf("Tilbage"))
                        //{
                        //    // Tilbage
                        //    continue;
                        //}
                        if (kundeAction == -1)
                        {
                            stopKundeMenuKundeChoose = true;
                        }
                        if (kundeAction == optionsKundeMenu.IndexOf("Udbetal"))
                        {
                            // Udbetal
                            IndbetalUdbetal("minus", aktuelKunde, _bll);
                        }
                        if (kundeAction == optionsKundeMenu.IndexOf("Indbetal"))
                        {
                            // Indbetal
                            IndbetalUdbetal("plus", aktuelKunde, _bll);
                        }
                        if (kundeAction == optionsKundeMenu.IndexOf("Slet Konti"))
                        {
                            // Slet konti
                            SletKonti(aktuelKunde, _bll);

                        }
                        if (kundeAction == optionsKundeMenu.IndexOf("Opret Konti"))
                        {
                            // Opret konti
                            OpretKonti(aktuelKunde, _bll);
                        }
                        if (kundeAction == optionsKundeMenu.IndexOf("Konti"))
                        {
                            // Overblik over konti
                            Konti(aktuelKunde);

                        }
                        if (kundeAction == optionsKundeMenu.IndexOf("Slet kunde"))
                        {
                            // Slet kunde
                            _bll.SletLinje(aktuelKunde);
                            return;
                        }

                    }
                    

                }
                else
                {
                    stopStartMenuKunde = true;
                }
            }
        }
        public static void SletKonti(Kunde aktuelKunde, BllClass _bll)
        {
            var kontoTodeliteIndex = ChooseOptions(aktuelKunde.konti, "Vælg en konto der skal slettes.\n", true);
            if (kontoTodeliteIndex == -1)
            {
                return;
            }
            var kontoTodelite = aktuelKunde.konti[kontoTodeliteIndex];
            aktuelKunde.konti.Remove(kontoTodelite);
            _bll.RetLinje(aktuelKunde);
        }
        public static void OpretKonti(Kunde aktuelKunde, BllClass _bll)
        {
            Console.WriteLine("Oprettelse af konto");
            Console.Write("Navn på kontoen: ");
            string navn = Console.ReadLine();

            konti nyKonto = new konti();
            nyKonto.navn = navn;
            nyKonto.saldo = 0;
            aktuelKunde.konti.Add(nyKonto);
            _bll.RetLinje(aktuelKunde);

            Console.WriteLine("Kono er oprettet, tryk på en tast for at komme tilbage");
            Console.ReadKey();
        }
        public static void Konti(Kunde aktuelKunde)
        {
            Console.WriteLine("Her er alle konti\n");
            if (aktuelKunde.konti != null)
            {
                foreach (var konti in aktuelKunde.konti)
                {
                    Console.WriteLine($"Konti: {konti.navn}. Saldo: {konti.saldo}");
                }
            }
            else
            {
                Console.WriteLine("Der er ikke nogen konto");
            }

            Console.WriteLine("\nTryk på en key for at komme tilbage");
            Console.ReadKey();
        }

        public static void IndbetalUdbetal(string status, Kunde aktuelKunde, BllClass _bll)
        {
            var kontoIndex = ChooseOptions(aktuelKunde.konti, "Vælg en konto der skal udbetalse fra.\n", true);
            if (kontoIndex == -1)
            {
                return;
            }

            var konto = aktuelKunde.konti[kontoIndex];
            if (status == "plus")
            {
                Console.WriteLine($"Du har valgt kontoen {konto.navn}. Hvor meget skal der indbetales?\n");
            }
            else if (status == "minus")
            {
                Console.WriteLine($"Du har valgt kontoen {konto.navn}. Hvor meget skal der udbetales?\n");
            }
            Console.Write("Beløb: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            if (status == "plus")
            {
                konto.saldo += amount;
            }
            else if (status == "minus")
            {
                konto.saldo -= amount;
            }
            

            decimal totalSaldo = 0;
            foreach (var konti in aktuelKunde.konti)
            {
                totalSaldo += konti.saldo;
            }
            aktuelKunde.totalSaldo = totalSaldo;

            _bll.RetLinje(aktuelKunde);
        }
        public static void StartMenuOpretKunde(BllClass _bll)
        {
            Console.WriteLine("Opret en ny kunde\n");
            Console.Write("First name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last name: ");
            string lastName = Console.ReadLine();

            bool correctCpr = false;
            long cpr = 0;
            while (correctCpr == false)
            {
                Console.Write("CPR: ");
                string cprString = Console.ReadLine();
                cpr = Convert.ToInt64(cprString);
                if (cprString.Length == 10)
                {
                    correctCpr = true;
                }
                else
                {
                    Console.WriteLine("CPR skal være 10 tal langt");
                }
            }
            Kunde newCustomer = new Kunde();
            newCustomer.firstName = firstName;
            newCustomer.lastName = lastName;
            newCustomer.cprNummer = cpr;
            newCustomer.totalSaldo = 0;
            newCustomer.konti = new List<konti>();

            _bll.OpretLinje(newCustomer);


            Console.WriteLine("Kunde er oprettet, try på en tast for at komme tilbage.");
            Console.ReadKey();
        }

        public static int ChooseOptions<T>(List<T> Options, string Prompt, bool withBack)
        {
            int index = 0;

            int indexMin = -1;
            int indexMax = Options.Count() - 1;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DispalyOptions(Prompt, Options, index, withBack);


                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow && index != indexMin)
                {
                    index--;
                }
                else if (keyPressed == ConsoleKey.DownArrow && index != indexMax)
                {
                    index++;
                }
            } while (keyPressed != ConsoleKey.Enter);
            Console.Clear();

            int delite = 0;
            if (keyPressed == ConsoleKey.Backspace)
            {
                delite = 1;
            }
            int[] output = new int[] { index, delite };

            return index;
        }


        public static void DispalyOptions<T>(string Prompt, List<T> Options, int SelectedIndex, bool withBack)
        {
            Console.WriteLine(Prompt);

            for (int i = -1; i < Options.Count(); i++)
            {

                string prefix;
                string sufix;

                string currenOptions = "";
                if (i == -1)
                {
                    if (withBack)
                    {
                        currenOptions = "Tilbage";
                    }                    
                }
                else
                {
                    currenOptions = Options[i].ToString();
                }

                if (i == SelectedIndex)
                {
                    prefix = "*";

                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{prefix} {currenOptions}");
            }
            Console.ResetColor();
        }
    }
}