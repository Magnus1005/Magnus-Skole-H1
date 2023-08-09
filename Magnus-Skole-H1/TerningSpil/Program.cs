using Dice;
namespace TerningSpil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Give a number of dices: ");
            int numberOfDices = Convert.ToInt32(Console.ReadLine());

            List<DiceClass> dices = new List<DiceClass>();

            for (int i = 0; i < numberOfDices; i++)
            {
                DiceClass dice = new DiceClass(6);
                dices.Add(dice);
            }

            bool found = false;
            int count = 0;
            do
            {
                count++;
                bool broke = false;
                foreach (DiceClass dice in dices)
                {
                    int res = dice.Roll();
                    if (res != 6)
                    {
                        broke = true;
                        break;
                    }
                }                
                if (broke == false)
                {
                    Console.WriteLine("FOUND "+ count);
                    found = true;
                }

            }
            while (found == false);

            


            
        }
    }
}