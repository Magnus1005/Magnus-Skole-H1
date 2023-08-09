using System.Security.Cryptography.X509Certificates;

namespace Strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(AddSeperaor("TEST", ";"));

            Console.WriteLine(IsPalindrome("EYE"));

            Console.WriteLine(LengthOfString("ice cream"));

            Console.WriteLine(ReverseString("TEST"));

            Console.WriteLine(CountWords("Hej"));

            Console.WriteLine(ReverseWord("Hej med dig"));
            
            Console.WriteLine(HowManyOccurrences("HEj med dig dig dig", "dig"));

            var SortDescendingResult = SortDescending("onomatopoeia");
            foreach(char c in SortDescendingResult)
            {
                Console.Write(c);
            }
            Console.WriteLine("");

            Console.WriteLine(CompressString("kkkktttrrrrrrrrrr"));
        }
        public static string AddSeperaor(string tekst, string seperaor)
        {
            string output = "";
            int count = 1;
            foreach(char c in tekst)
            {
                if (count == tekst.Length)
                {
                    output += c;
                }
                else
                {
                    output += c + seperaor;
                }
                count++;
                
            }
            return output;
        }

        public static bool IsPalindrome(string tekst)
        {
            char[] stringArray = tekst.ToCharArray();
            Array.Reverse(stringArray);
            string reversed = new string(stringArray);

            if (reversed.ToLower() == tekst.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int LengthOfString(string tekst)
        {
            int length = 0;
            foreach (char c in tekst)
            {
                length++;
            }
            return length;
        }

        public static string ReverseString(string tekst)
        {
            char[] stringArray = tekst.ToCharArray();
            Array.Reverse(stringArray);
            string reversed = new string(stringArray);
            return reversed;
        }

        public static int CountWords(string tekst)
        {
            string trimmedText = tekst.Trim();
            string[] splitText = trimmedText.Split(" ");
            return splitText.Count();
        }

        public static string ReverseWord(string tekst)
        {
            string trimmedText = tekst.Trim();
            string[] splitText = trimmedText.Split(" ");
            Array.Reverse(splitText);
            return String.Join(" ", splitText);
        }

        public static int HowManyOccurrences(string tekst, string word)
        {
            string trimmedText = tekst.Trim();
            string[] splitText = trimmedText.Split(" ");
            return splitText.Where(x => x == word).ToList().Count();
        }

        public static char[] SortDescending(string tekst)
        {
            char[] charArray = tekst.ToCharArray();
            return  charArray.OrderByDescending(x => x).ToArray();
        }

        public static string CompressString(string tekst)
        {
            if (string.IsNullOrEmpty(tekst))
            {
                return tekst;
            }                

            char currentChar = tekst[0];
            int charCount = 1;
            string compressedString = "";

            for (int i = 1; i < tekst.Length; i++)
            {
                if (tekst[i] == currentChar)
                {
                    charCount++;
                }
                else
                {
                    compressedString += currentChar;
                    if (charCount > 0)
                    {
                        compressedString += charCount.ToString();
                    }                        

                    currentChar = tekst[i];
                    charCount = 1;
                }
            }

            compressedString += currentChar;
            if (charCount > 1)
            {
                compressedString += charCount.ToString();
            }                

            return compressedString;
        }
    }
}