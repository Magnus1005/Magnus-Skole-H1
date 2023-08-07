using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static System.Console;
namespace Regnespil
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string[] OptionsDisiplin = { "Addition", "Subtraktion", "Multiplikation", "Division", "Tabel", "End" };
            string[] OptionsDificulty = { "Easy", "Medium", "Hard" };
            string[] OptionsMinus = { "Plus", "Plus & Minus" };
            string[] OptionsTabel = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };


            bool contin = true;
            while(contin)
            {
                int Disiplin = ChooseOptions(OptionsDisiplin, "Chooce one");
                if (Disiplin == 5 )
                {
                    Environment.Exit(0);
                }
                else if (Disiplin == 4 )
                {
                    int tabelNumber = ChooseOptions(OptionsTabel, "Choose a tabel");
                    string result = tabel(tabelNumber);
                    if (result == "STOP")
                    {
                        contin = true;
                    }
                    else
                    {
                        Console.WriteLine($"You got {result} points. Press any key to countinue");
                        Console.ReadKey();
                    }
                }
                else
                {
                    int Dificulty = ChooseOptions(OptionsDificulty, "How hard do you want it");
                    int Minus = ChooseOptions(OptionsMinus, "Chooce one");

                    string result = Game(Disiplin, Dificulty, Minus);
                    if (result == "STOP")
                    {
                        contin = true;
                    }
                    else
                    {
                        Console.WriteLine($"You got {result} points. Press any key to countinue");
                        Console.ReadKey();
                    }
                }
                
            }            
        }

        public static int ChooseOptions(string[] Options, string Prompt)
        {
            int index = 0;
            int indexMin = 0;
            int indexMax = Options.Count() - 1;
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DispalyOptions(Prompt, Options, index);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow && index != indexMin)
                {
                    index--;
                }
                else if(keyPressed == ConsoleKey.DownArrow && index != indexMax)
                {
                    index++;
                }

            } while (keyPressed != ConsoleKey.Enter);
            Clear();
            return index;
            
        }

        public static void DispalyOptions(string Prompt, string[] Options, int SelectedIndex)
        {
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currenOptions = Options[i];
                string prefix;
                if (i == SelectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{prefix} -{currenOptions}-");
            }
            ResetColor();
        }
        public static string Game(int Disiplin, int Dificulty, int Minus)
        {
            int score = 0;
            int minValue;
            int maxValue;
            if (Dificulty == 0)
            {
                if (Minus == 0)
                {
                    minValue = 1;
                }
                else
                {
                    minValue = -10;
                }                
                maxValue = 10;
            }
            else if (Dificulty == 1)
            {
                if (Minus == 0)
                {
                    minValue = 1;
                }
                else
                {
                    minValue = -100;
                }
                maxValue = 100;
            }
            else
            {
                if (Minus == 0)
                {
                    minValue = 1;
                }
                else
                {
                    minValue = -1000;
                }
                maxValue = 1000;
            }

            string symbol = "";
            if (Disiplin == 0)
            {
                symbol = "+";
            }
            else if(Disiplin == 1)
            {
                symbol = "-";
            }
            else if (Disiplin == 2)
            {
                symbol = "*";
            }
            else if (Disiplin == 3)
            {
                symbol = "/";
            }

            int calculationNumber = 0;
            Random random = new Random();
            while (calculationNumber < 10)
            {
                int randomNumber1 = random.Next(minValue, maxValue + 1);
                int randomNumber2 = random.Next(minValue, maxValue + 1);
                int ans = 0;
                if (Disiplin == 0)
                {
                    ans = randomNumber1 + randomNumber2;
                }
                else if (Disiplin == 1)
                {
                    ans = randomNumber1 - randomNumber2;
                }
                else if (Disiplin == 2)
                {
                    ans = randomNumber1 * randomNumber2;
                }
                else if (Disiplin == 3)
                {
                    ans = randomNumber1 / randomNumber2;
                }



                int tryNum = 0;
                bool cont = true;

                while(tryNum < 3 && cont == true)
                {
                    Console.Write(randomNumber1 + " " + symbol + " " + randomNumber2 + " = ");
                    string responde = Console.ReadLine();
                    if (responde == "STOP")
                    {
                        return responde;
                    }
                    else if(responde == ans.ToString())
                    {
                        cont = false;
                    }
                    else
                    {
                        tryNum++;
                    }
                    Clear();
                }
                if (tryNum == 3)
                {
                    Console.Write(randomNumber1 + " " + symbol + " " + randomNumber2 + " = " + ans + ". Press an key to countinue");
                    Console.ReadKey();
                    Clear();
                }
                if (tryNum == 0)
                {
                    score += 3 * (Dificulty + 1);
                }
                else if (tryNum == 2)
                {
                    score += 2 * (Dificulty + 1);
                }
                else if(tryNum == 1)
                {
                    score += 1 * (Dificulty + 1);
                }

                calculationNumber++;


            }


            

            return $"{score}";
        }

        public static string tabel (int tabel)
        {
            tabel++;
            int add = tabel;
            int score = 0;

            int total = tabel;
            while (total < 100)
            {
                Console.Write(total +  " ");
                string resopnse = Console.ReadLine();
                if (resopnse == (total + add).ToString())
                {
                    total += add;
                    score += 5;
                }
                else
                {
                    score -= 2;
                }
                Clear();
            }

            return $"{score}";
        }
        
    }
}