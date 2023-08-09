using System;
namespace ConditionalStatements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Køre de forskellige metoder med deres værdier
            Console.WriteLine(AbsoluteValue(-2));

            Console.WriteLine(DivisibleBy2Or3(7, 12));

            Console.WriteLine(IsUpperString("TEST"));

            int[] numbers = new int[] { -5, -8, 50 };
            Console.WriteLine(GreaterThanThird(numbers));

            Console.WriteLine(IsEven(3));

            int[] numbersIsSorted = new int[] { 5, 10, 50 };
            Console.WriteLine(IsSorted(numbersIsSorted));

            Console.WriteLine(PositiveNegativeZero(0));

            Console.WriteLine(IsLeapYear(2015));

        }
        public static int AbsoluteValue(int value)
        {
            // Tager den absolute værdi og retunere den
            return Math.Abs(value);
        }

        public static int DivisibleBy2Or3(int number1, int number2)
        {
            // Tjekker om nummeret kan divideres med 2 eller 3 og hvis den kan så ganges de to tal og retuneres ellers lægges de sammen
            if ((number1 % 2 == 0 || number1 % 3 == 0) && (number2 % 2 == 0 || number2 % 3 == 0))
            {
                return number1 * number2;
            }
            else
            {
                return number1 + number2;
            }
        }

        public static bool IsUpperString(string value)
        {
            // Tjekker om alle bokstaverne i strinen er strore og retunere true hvis de er
            bool isAllUppercase = value.All(char.IsUpper);
            return isAllUppercase;            
        }

        public static bool GreaterThanThird(int[] numbers)
        {
            // Tjekker om tal1 og tal2 lagt sammen eller ganget er større end tal3 
            if (numbers[0] + numbers[1] > numbers[2] || numbers[0] * numbers[1] > numbers[2])
            {
                return true;
            }
            else { return false; }
        }

        // Tjekker om tallet er lige ved at se om der er en rest og den er lige returner true
        public static bool IsEven(int number)
        {
            if (number % 2 == 0)
            {
                return true;
            }
            else { return false; }

        }

        public static bool IsSorted(int[] numbers)
        {
            // Tjekker om den har en stigende rækkefølge og hvis den har retunere true
            bool value = numbers[0] <= numbers[1] && numbers[1] <= numbers[2];
            return value;
        }

        public static string PositiveNegativeZero(decimal number)
        {
            // Retunere om tallet er større, mindre eller ligg med 0
            if(number < 0)
            {
                return "Negative";
            }
            else if(number > 0)
            {
                return "Positive";
            }
            else
            {
                return "Zero";
            }
        }

        public static bool IsLeapYear(int year)
        {
            // Tjekker om året er et skudår ved at se om 4 går op i året
            if ((year % 4) == 0)
            {
                if ((year % 100) == 0)
                {
                    if(year % 400 == 0)
                    {
                        return true;
                    }
                }
                else { return true; }
            }
            return false;
        }
    }
}