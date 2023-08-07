using System;
using System.Globalization;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace FirstProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Kalder min metode AddAndMultiply
            double resultAddAndMultiply = AddAndMultiply(2, 2, 4);
            // Skriver resultatet til consolen
            Console.WriteLine(resultAddAndMultiply);

            // Kalder min metode
            string resultCelsiusToFahrenheit = CelsiusToFahrenheit(-250);
            // Skriver resultatet til consolen
            Console.WriteLine(resultCelsiusToFahrenheit);

            // Kalder min metode
            double[] resultElementary = Elementary(18, 2);
            // Skriver resultatet til consolen
            Console.WriteLine($"Plus: {resultElementary[0]}. Minus: {resultElementary[1]}. Multiply: {resultElementary[2]}. Divide: {resultElementary[3]}");

            // Kalder min metode
            bool resultIsResultTheSame = IsResultTheSame(2 + 2, 2 * 2);
            // Skriver resultatet til consolen
            Console.WriteLine(resultIsResultTheSame);

            // Kalder min metode
            int resultModuloOperations = ModuloOperations(8, 5, 2);
            // Skriver resultatet til consolen
            Console.WriteLine(resultModuloOperations);

            // Kalder min metode
            double resultCubeOfNumber = CubeOfNumber(5);
            // Skriver resultatet til consolen
            Console.WriteLine(resultCubeOfNumber);

            // Definere værdierne for nummer 1 og 2
            double number1 = 2;
            double number2 = 4;

            // Laver et array med værdierne
            double[] numbers = new double[] { number1, number2 };
            // Kalder min metode
            double[] resultSwapNumbers = SwapNumbers(numbers);
            // Skriver resultatet til consolen
            Console.WriteLine($"Before: a = {number1}, b = {number2}; After: a = {resultSwapNumbers[0]}, b = {resultSwapNumbers[1]}");
        }

        public static double AddAndMultiply(double num1, double num2, double num3)
        {
            // Lægger de forskellige tal sammen og ganger. derefter returner jeg værdien
            return (num1 + num2) * num3;
        }
        

        public static string CelsiusToFahrenheit(double temperature)
        {
            // Definere output
            string output = "";

            // Tjekker at temperaturen er over -271.15
            if (temperature >= -271.15)
            {
                // Beregner den nye temperatur
                double value = (temperature * 1.8) + 32;
                // Sætter værdien til output
                output = $"{temperature} C = {Math.Round(value, 2)} F";
            }
            else
            {
                // Hvis at den er under skriver vi absolute zero.
                output = "Temperature below absolute zero!";
            }
            // Returner værdien af output
            return output;            
        }

        public static double[] Elementary(int number1, int number2)
        {
            // Laver beregningerne for plus, minus og gange.
            double plus = number1 + number2;
            double minus = number1 - number2;
            double multiply = number1 * number2;
            double divide = 0;

            // Tjekker at værdien ikke er null da du ike kan gange med null
            if (number1 != 0 && number2 != 0)
            {
                divide = number1 / number2;
            }
            // Sætter værierne ind i et array
            double[] output = new double[] { plus, minus, multiply, divide };

            // Returner arrayet
            return output;
        }

        public static bool IsResultTheSame(double number1, double number2)
        {
            // Tjekker om talene er det samme
            if (number1 == number2)
            {
                return true;
            }
            else { return false; }
        }

        public static int ModuloOperations(int number1, int number2, int number3)
        {
            // Laver modulo operationen og retunere det
            return (number1 % number2) % number3;
        }

        public static double CubeOfNumber(double number)
        {
            // Ganger tallet med sig selv og retunere det
            return number * number * number;
        }

        public static double[] SwapNumbers(double[] numbers)
        {
            // Laver en reverse på arrayet
            Array.Reverse(numbers);
            // Retunere arrayet
            return numbers;
        }

    }
}