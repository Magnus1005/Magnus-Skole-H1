using BLL;
using DAL;

namespace Front
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BllClass bllClass = new BllClass();

            bool stop = false;
            while (stop == false)
            {
                List<string> OptionsStart = new List<string>() { "Kunder", "Opret kunde", "Exit" };

                int menuValgt = ChooseOptionsRemaning(OptionsStart, "Velkommen til det lille pengeinstitut.\n");
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
                            var kundeData = BllClass.HentAltData();
                            int kundeNummer = ChooseOptionsKunder(kundeData, "Vælg en kunde.\n");
                            if (kundeNummer != -1)
                            {
                                var aktuelKunde = kundeData[kundeNummer];
                                string prompt = $"Navn: {aktuelKunde.firstName} {aktuelKunde.lastName}\nCPR: {aktuelKunde.cprNummer}\nSaldo: {aktuelKunde.totalSaldo}\n";

                                int kundeAction = ChooseOptionsRemaning(new List<string>() { "Slet kunde", "Konti", "Opret Konti", "Slet Konti", "Indbetal", "Udbetal", "Tilbage" }, prompt);
                                if (kundeAction == 6)
                                {
                                    // Tilbage
                                    break;
                                }
                                if (kundeAction == 5)
                                {
                                    // Udbetal
                                    var kontoIndex = ChooseOptionsKonti(aktuelKunde.konti, "Vælg en konto der skal udbetalse fra.\n");
                                    var konto = aktuelKunde.konti[kontoIndex];

                                    Console.WriteLine($"Du har valgt kontoen {konto.navn}. Hvor meget skal der udbetales?\n");
                                    Console.Write("Beløb: ");
                                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                                    konto.saldo -= amount;

                                    decimal totalSaldo = 0;
                                    foreach (var konti in aktuelKunde.konti)
                                    {
                                        totalSaldo += konti.saldo;
                                    }
                                    aktuelKunde.totalSaldo = totalSaldo;

                                    BllClass.RetLinje(aktuelKunde);
                                }
                                if (kundeAction == 4)
                                {
                                    // Indbetal
                                    var kontoIndex = ChooseOptionsKonti(aktuelKunde.konti, "Vælg en konto der skal indbetales til.\n");
                                    var konto = aktuelKunde.konti[kontoIndex];

                                    Console.WriteLine($"Du har valgt kontoen {konto.navn}. Hvor meget skal der indbetales?\n");
                                    Console.Write("Beløb: ");
                                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                                    konto.saldo += amount;

                                    decimal totalSaldo = 0;
                                    foreach (var konti in aktuelKunde.konti)
                                    {
                                        totalSaldo += konti.saldo;
                                    }
                                    aktuelKunde.totalSaldo = totalSaldo;

                                    BllClass.RetLinje(aktuelKunde);

                                }
                                if (kundeAction == 3)
                                {
                                    // Slet konti
                                    var kontoTodeliteIndex = ChooseOptionsKonti(aktuelKunde.konti, "Vælg en konto der skal slettes.\n");
                                    if (kontoTodeliteIndex == -1)
                                    {
                                        break;
                                    }
                                    var kontoTodelite = aktuelKunde.konti[kontoTodeliteIndex];
                                    aktuelKunde.konti.Remove(kontoTodelite);
                                    BllClass.RetLinje(aktuelKunde);

                                }
                                if (kundeAction == 2)
                                {
                                    // Opret konti
                                    Console.WriteLine("Oprettelse af konto");
                                    Console.Write("Navn på kontoen: ");
                                    string navn = Console.ReadLine();

                                    konti nyKonto = new konti();
                                    nyKonto.navn = navn;
                                    nyKonto.saldo = 0;
                                    aktuelKunde.konti.Add(nyKonto);
                                    BllClass.RetLinje(aktuelKunde);

                                    Console.WriteLine("Kono er oprettet, tryk på en tast for at komme tilbage");
                                    Console.ReadKey();

                                }
                                if (kundeAction == 1)
                                {
                                    // Overblik over konti
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
                                if (kundeAction == 0)
                                {
                                    // Slet kunde
                                    BllClass.SletLinje(aktuelKunde);
                                    break;
                                }
                                
                            }
                            break;
                        case 1:
                            // Opret kunde
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

                            BllClass.OpretLinje(newCustomer);


                            Console.WriteLine("Kunde er oprettet, try på en tast for at komme tilbage.");
                            Console.ReadKey();
                            continue;


                            break;
                        default:
                            Console.WriteLine("Error");
                            break;
                    }


                }
            }



            


        }
        public static int ChooseOptionsKonti(List<konti> Options, string Prompt)
        {
            int index = 0;

            int indexMin = -1;
            int indexMax = Options.Count() - 1;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DispalyOptionsKonti(Prompt, Options, index);

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
        public static void DispalyOptionsKonti(string Prompt, List<konti> Options, int SelectedIndex)
        {
            Console.WriteLine(Prompt);

            for (int i = -1; i < Options.Count(); i++)
            {

                string prefix;
                string sufix;

                string currenOptions = "";
                if (i == -1)
                {
                    currenOptions = "Tilbage";
                }
                else
                {
                    currenOptions = "konto Navn: " + Options[i].navn + ". Saldo: " + Options[i].saldo;
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

        public static int ChooseOptionsKunder(List<Kunde> Options, string Prompt)
        {
            int index = 0;

            int indexMin = -1;
            int indexMax = Options.Count() - 1;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DispalyOptionsKunder(Prompt, Options, index);

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
        public static void DispalyOptionsKunder(string Prompt, List<Kunde> Options, int SelectedIndex)
        {
            Console.WriteLine(Prompt);

            for (int i = -1; i < Options.Count(); i++)
            {
                
                string prefix;
                string sufix;

                string currenOptions = "";
                if (i == -1)
                {
                    currenOptions = "Tilbage";
                }
                else
                {
                    currenOptions = "CPR Nummer: " + Options[i].cprNummer + ". Navn: " + Options[i].firstName + " " + Options[i].lastName;
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

        public static int ChooseOptionsRemaning(List<string> Options, string Prompt)
        {
            int index = 0;

            int indexMin = 0;
            int indexMax = Options.Count() - 1;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DispalyOptionsRemaning(Prompt, Options, index);

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
        public static void DispalyOptionsRemaning(string Prompt, List<string> Options, int SelectedIndex)
        {
            Console.WriteLine(Prompt);
            for (int i = 0; i < Options.Count(); i++)
            {
                string currenOptions = Options[i].ToString();
                string prefix;
                string sufix;
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