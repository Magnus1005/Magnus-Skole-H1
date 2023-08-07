namespace Loops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MultiplicationTable());

            int[] numbers1 = new int[] { -5, -8, -50 };
            Console.WriteLine(BiggestNumber(numbers1));

            int[] numbers2 = new int[] { 9, 4, 5, 3, 7, 7, 7, 3, 2, 5, 7, 7 };
            Console.WriteLine(SevenNextToEachOther(numbers2));

            int[] numbers3 = new int[] { 7, 3, 5, 8, 9, 3, 1, 4 };
            Console.WriteLine(ThreeIncreasingAdjacent(numbers3));

            List<int> primeNumbers = SieveOfEratosthenes(20);
            foreach(int number in primeNumbers)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine("");

            Console.WriteLine(ExtractString("12####78"));

            Console.WriteLine(FullSequenceOfLetters("sz"));

            double[] resSumAndAverage = SumAndAverage(11, 66);
            Console.WriteLine($"Sum: {resSumAndAverage[1]}. Average: {resSumAndAverage[0]}.");

            Console.WriteLine(DrawTriangle());

            Console.WriteLine(PowerOf(-2, 3));
        }

        public static string MultiplicationTable()
        {
            string output = "";

            for (int i = 1; i <= 10; i++)
            {
                int addNum = i;

                int num = 0;

                string tempstrng = "";


                while (num != addNum * 10)
                {
                    num += addNum;
                    tempstrng += $"{num, 3} ";
                }

                output += tempstrng + "\n";
            }
            return output;
        }

        public static int BiggestNumber(int[] array)
        {
            int maxNumber = array.Max(x => x);
            return maxNumber;
        }

        public static int SevenNextToEachOther(int[] array)
        {
            int count = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == 7 && array[i] == 7)
                {
                    count++;
                }
            }

            return count;
        }

        public static bool ThreeIncreasingAdjacent(int[] array)
        {
            for (int i = 0; i < array.Length - 2; i++)
            {
                if (array[i + 1] == array[i] + 1 && array[i + 2] == array[i + 1] + 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<int> SieveOfEratosthenes(int number)
        {
            List<int> primes = new List<int>();

            for (int num = 2; num <= number; num++)
            {
                bool isPrime = true;

                for (int divisor = 2; divisor <= Math.Sqrt(num); divisor++)
                {
                    if (num % divisor == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    primes.Add(num);
                }
            }

            return primes;
        }

        public static string ExtractString(string str)
        {
            int startIndex = str.IndexOf("##");
            if (startIndex == -1)
            {
                return string.Empty;
            }

            int endIndex = str.IndexOf("##", startIndex + 2);
            if (endIndex == -1)
            {
                return string.Empty;
            }

            startIndex += 2;
            string output = str.Substring(startIndex, endIndex - startIndex);
            return output;

        }

        public static string FullSequenceOfLetters(string letters)
        {
            string alpha = "abcdefghijklmnopqrstuvwxyzæøå";

            string firstLetter = letters.Substring(0, 1);
            string lastLetter = letters.Substring(letters.Length - 1, 1);

            
            int firstLetterIndex = alpha.IndexOf(firstLetter);
            int lastLetterIndex = alpha.IndexOf(lastLetter); ;

            return alpha.Substring(firstLetterIndex, lastLetterIndex - firstLetterIndex + 1);
        }

        public static double[] SumAndAverage(double num1, double num2)
        {
            double[] result = new double[2];


            result[0] = (num1 + num2) / 2;
            double sum = 0;

            for(double i = num1; i <= num2; i++)
            {
                sum += i;
            }
            result[1] = sum;

            return result;
        }

        public static string DrawTriangle()
        {
            int antalLinjer = 50;
            string output = "";

            int i, j;
            i = j = 0;
            for (i = 1; i <= antalLinjer; i++)
            {
                for (j = 1; j <= (antalLinjer - i)/2; j++)
                {
                    output += " ";
                }
                for (j = 1; j <= i; j++)
                {
                    output += "*";
                }
                output += "\n";
            }
            return output;
        }

        public static double PowerOf(double num1, double  num2)
        {
            return Math.Pow(num1, num2);
        }



    }
}