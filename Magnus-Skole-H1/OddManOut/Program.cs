using System;
namespace OddManOut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // ulige lise/array find det tal der ikke har makker

            //int[] numbers = new int[] { 9, 3, 9, 3, 9, 7, 9 };
            List<int> numbers = new List<int> { 9, 3, 9, 3, 9, 7, 9 };

            foreach (int number in numbers)
            {
                int count = numbers.Where(x => x == number).ToList().Count;
            }
        }
    }
}