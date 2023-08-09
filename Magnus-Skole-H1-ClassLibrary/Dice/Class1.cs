namespace Dice
{
    public class DiceClass
    {
        Random random = new Random();
        private int numberOfSides;

        public int NumberOfSides
        {
            get { return numberOfSides; }
            set { numberOfSides = value; }
        }

        public DiceClass(int numberOfStartSides)
        {
            numberOfSides = numberOfStartSides;
        }

        public int Roll()
        {
            if (numberOfSides >= 2)
            {
                return random.Next(1, numberOfSides + 1);
            }
            else
            {
                return 0;
            }
        }
    }
}