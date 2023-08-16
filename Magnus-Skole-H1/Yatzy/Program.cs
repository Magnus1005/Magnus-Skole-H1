using Dice;
using System.Data;

namespace Yatzy
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string[] linjer = new string[] { "one", "two", "three", "four", "five", "six", "onePair", "twoPair", "threeIdentical", "fourIdentical", "smallStraight", "bigStraight", "house", "chance", "yatzy" };
            Console.WriteLine("Welcome to yatzy. To start the game press any key");

            Console.WriteLine("Please enter a name and prees enter when all players are pute in type done and press enter");
            List<Players> players = GetPlayer(linjer);

            var gameResul = Game(players, linjer);

            Console.Clear();

            Console.WriteLine("The game is over. Here are the results\n");
            foreach(var player in gameResul)
            {
                Console.WriteLine($"{player.Name}: {player.Points}");
            }
        }

        public static List<Players> GetPlayer(string[] linjer)
        {
            bool isDone = false;
            List<Players> players = new List<Players>();

            while (isDone == false)
            {
                Console.Write("Name: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "done")
                {
                    if (players.Count < 2)
                    {
                        Console.WriteLine("You need to be at least 2 players for this game");
                        continue;
                    }
                    else
                    {
                        isDone = true;
                        break;
                    }                    
                }
                else
                {
                    List<Players> playerExcist = players.Where(x => x.Name.ToLower() == input.ToLower()).ToList();
                    if(playerExcist.Count > 0)
                    {
                        Console.WriteLine("This name is already in use please enter a difrend name.");
                        continue;
                    }
                    else
                    {
                        Players player = new Players();
                        player.Name = input;
                        player.Points = 0;

                        player.values.Columns.Add("key", typeof(string));
                        player.values.Columns.Add("value", typeof(int));
                        player.values.Columns.Add("status", typeof(bool));

                        foreach(string line in linjer)
                        {
                            DataRow newRow = player.values.NewRow();
                            newRow["key"] = line;
                            newRow["value"] = 0;
                            newRow["status"] = true;
                            player.values.Rows.Add(newRow);
                        }
                        players.Add(player);
                    }                    
                }
            }
            Console.Clear();
            return players;
        }

        public static List<Players> Game(List<Players> players, string[] linjer)
        {
            List<DiceClass> dices = new List<DiceClass>();

            int diceCount = 0;
            while (diceCount < 5)
            {
                DiceClass dice = new DiceClass(6);
                dices.Add(dice);
                diceCount++;
            }  

            bool winnerFound = false;

            while(winnerFound == false)
            {
                foreach (var player in players)
                {                 
                    Console.Clear();
                    
                    List<int> rollResult = new List<int>();

                    var dicesToRoll = new List<DiceClass>();
                    dicesToRoll.AddRange(dices);

                    int rollNumber = 0;
                    while(rollNumber < 3)
                    {
                        rollNumber++;
                        //Console.WriteLine($"This is roll number {rollNumber}");
                        int diceNumber = 1;
                        foreach (var dice in dicesToRoll)
                        {
                            if (rollResult.Count() < diceNumber)
                            {
                                int value = dice.Roll();
                                //Console.WriteLine($"Dice {diceNumber}: {value}");
                                int indexArray = diceNumber - 1;
                                rollResult.Add(value);                                
                            }
                            diceNumber++;
                        }
                        if (rollNumber < 3)
                        {
                            string promtStringOnRoll = "";
                            var remaningOnRoll = GetRemaning(player.values);
                            foreach (var rem in remaningOnRoll)
                            {
                                promtStringOnRoll += rem + "\n";
                            }

                            var saveResult = ChooseOptionsDise(rollResult, $"Turn: {player.Name}.\n\nThis is roll number {rollNumber}.\nPress space to hold or unhold. And press enter to confirm round.\n\n\nThis is what you have left: \n{promtStringOnRoll}");
                            rollResult.Clear();
                            rollResult.AddRange(saveResult);
                        }                        
                    }
                    string promtString = "This is the result. What would you like to put them under. Press enter to use it or press backspace to cross it out\n\n";
                    int resultCount = 1;
                    foreach(var dice in rollResult)
                    {
                        promtString += $"Dice {resultCount}: {dice}\n";
                        //Console.WriteLine($"Dice {resultCount}: {dice}");
                        resultCount++;
                    }

                    var remaning = GetRemaning(player.values);

                    if(remaning.Count() == 1)
                    {
                       winnerFound = true;
                    }

                    bool optionChossenCoreect = false;

                    while(optionChossenCoreect == false)
                    {
                        var chossenActionIndex = ChooseOptionsRemaning(remaning, promtString);
                        var chossenAction = remaning[chossenActionIndex[0]];
                        bool deliteAction = false;

                        if (chossenActionIndex[1] == 1)
                        {
                            deliteAction = true;
                        }

                        try
                        {
                            int points = 0;

                            if (deliteAction == true)
                            {
                                points = 0;
                            }
                            else
                            {
                                points = RunAction(chossenAction, rollResult);
                            }

                            int indexAction = Array.IndexOf(linjer, chossenAction);
                            player.values.Rows[indexAction]["value"] = points;
                            player.values.Rows[indexAction]["status"] = false;
                            optionChossenCoreect = true;
                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message + " Press any key to choose again");
                            Console.ReadKey();
                        }
                    }
                    Console.Clear();
                }
            }
            var gameResult = GetScorre(players);

            return gameResult;            
        }

        public static List<int> ChooseOptionsDise(List<int> Options, string Prompt)
        {
            int index = 0;

            int indexMin = 0;            
            int indexMax = Options.Count() - 1;
            ConsoleKey keyPressed;

            List<int> chossenList = new List<int>();
            do
            {
                Console.Clear();
                
                DispalyOptionsDise(Prompt, Options, index, chossenList);

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

                if (keyPressed == ConsoleKey.Spacebar)
                {
                    if (chossenList.Where(x => x == index).Count() > 0)
                    {
                        chossenList.Remove(index);
                    }
                    else
                    {
                        chossenList.Add(index);
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            Console.Clear();

            List<int> result = new List<int>();
            foreach (int i in chossenList)
            {
                result.Add(Options[i]);
            }
            return result;
        }

        public static void DispalyOptionsDise(string Prompt, List<int> Options, int SelectedIndex, List<int> chossenIndexes)
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

                    if (chossenIndexes.Where(x => x == i).Count() > 0)
                    {
                        sufix = "On Hold";
                    }
                    else
                    {
                        sufix = "";
                    }
                    
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else if (chossenIndexes.Where(x => x == i).Count() > 0)
                {
                    prefix = " ";
                    sufix = "On Hold";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    prefix = " ";
                    sufix = "";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{prefix} Dice number {i + 1}: |{currenOptions}| {sufix}");
            }
            Console.ResetColor();
        }

        public static int[] ChooseOptionsRemaning(List<string> Options, string Prompt)
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
            } while (keyPressed != ConsoleKey.Enter && keyPressed != ConsoleKey.Backspace);
            Console.Clear();

            int delite = 0;
            if(keyPressed == ConsoleKey.Backspace)
            {
                delite = 1;
            }
            int[] output = new int[] { index, delite };

            return output;
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

        public static List<string> GetRemaning(DataTable data)
        {
            List<string> remaning = new List<string>();
            int numberOfRows = data.Rows.Count;
            
            foreach (DataRow row in data.Rows)
            {
                if (Convert.ToBoolean(row.ItemArray[2]) == true)
                {
                    remaning.Add(row.ItemArray[0].ToString());
                }
            }
            return remaning;
        }

        public class Players
        {
            public string Name { get; set; }
            public int Points { get; set; } = 0;

            public DataTable values = new DataTable();
        }

        public static List<Players> GetScorre(List<Players> players)
        {
            foreach (Players p in players)
            {
                int pointsforThisPlayer = 0;

                var playerRows = p.values.Rows;
                
                for(int i = 0; i < playerRows.Count; i++)
                {
                    pointsforThisPlayer += Convert.ToInt32(playerRows[i]["value"]);
                }

                int bonus = 0;
                int value = 0;

                for (int i = 0; i < 6; i++)
                {
                    value += Convert.ToInt32(playerRows[i]["value"]);
                }
                if (value >= 63)
                {
                    bonus = 50;
                }

                p.Points = pointsforThisPlayer + bonus;
            }
            return players;
        }

        public static int RunAction(string action, List<int> list)
        {
            switch (action)
            {
                case "one":
                    try
                    {
                        return CountSame(list, 1);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }                    
                case "two":
                    try
                    {
                        return CountSame(list, 2);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                case "three":
                    try
                    {
                        return CountSame(list, 3);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                case "four":
                    try
                    {
                        return CountSame(list, 4);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                case "five":
                    try
                    {
                        return CountSame(list, 5);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                case "six":
                    try
                    {
                        return CountSame(list, 6);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                case "onePair":
                    try
                    {
                        return Pair(list, 1);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                case "twoPair":
                    try
                    {
                        return Pair(list, 2);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                case "threeIdentical":
                    try
                    {
                        return Identical(list, 3);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                case "fourIdentical":
                    try
                    {
                        return Identical(list, 4);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    };

                case "smallStraight":
                    try
                    {
                        return Straight(list, "small");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                case "bigStraight":
                    try
                    {
                        return Straight(list, "big");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                case "house":
                    try
                    {
                        return House(list);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                case "chance":
                    try
                    {
                        return Chance(list);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                case "yatzy":
                    try
                    {
                        return Yatzy(list);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                default: throw new Exception($"Kunne ikke finde den: {action}.");
            }
        }

        public static int CountSame(List<int> list, int number)
        {
            var listWithTheNumbers = list.Where(x => x == number).ToList();
            if (listWithTheNumbers.Count > 0)
            {
                return listWithTheNumbers.Count() * number;
            }
            else
            {
                throw new Exception($"You don't have any {number}'s.");
            }            
        }

        public static int Pair(List<int> list, int number)
        {
            List<int> seenNumbers = new List<int>();
            List<int> pairValues = new List<int>();
            int pairsFound = 0;

            foreach (int num in list)
            {
                if (seenNumbers.Contains(num))
                {
                    pairsFound++;
                    pairValues.Add(num);
                    seenNumbers.Remove(num); // Remove one occurrence of the pair
                }
                else
                {
                    seenNumbers.Add(num);
                }
            }
            int points = 0;
            if (pairsFound != number && pairsFound < number )
            {
                if (number == 1)
                {
                    throw new Exception($"You don't have {number} pair.");
                }
                else
                {
                    throw new Exception($"You don't have {number} pairs.");
                }                
            }
            if (number == 1)
            {
                points += pairValues.Max() * 2;
            }
            else
            {
                foreach (int pair in pairValues)
                {
                    points += pair * 2;
                }
            }            
            return points;
        }

        public static int Identical(List<int> list, int number)
        {
            List<int> seenNumbers = new List<int>();
            int theNumber = 0;
            foreach (int num in list)
            {
                if (seenNumbers.Where(x => x == num).Count() >= number - 1)
                {
                    theNumber = num; // Found a duplicate pair
                    break;
                }
                seenNumbers.Add(num);
            }
            if (theNumber == 0)
            {
                throw new Exception("You don't have the correct number of identical numbers.");
            }
            return theNumber * number;
        }

        public static int Straight(List<int> list, string type)
        {
            if (type == "small")
            {
                list.Sort();

                for (int i = 0; i <= list.Count - 4; i++)
                {
                    if (list[i] == list[i + 1] - 1 &&
                        list[i + 1] == list[i + 2] - 1 &&
                        list[i + 2] == list[i + 3] - 1)
                    {
                        int point = 0;
                        foreach(int num in list)
                        {
                            point += num;
                        }
                        return point;
                    }
                }
                throw new Exception("You don't have a small straight.");
            }
            else if (type == "big")
            {
                list.Sort();

                for (int i = 0; i <= list.Count - 5; i++)
                {
                    if (list[i] == list[i + 1] - 1 &&
                        list[i + 1] == list[i + 2] - 1 &&
                        list[i + 2] == list[i + 3] - 1 &&
                        list[i + 3] == list[i + 4] - 1)
                    {
                        int point = 0;
                        foreach (int num in list)
                        {
                            point += num;
                        }
                        return point;
                    }
                }
                throw new Exception("You don't have a big straight.");
            }
            throw new Exception("Not found. Not big or small.");
        }

        public static int House(List<int> list)
        {
            var grouped = list.GroupBy(value => value);
            bool isHouse = grouped.Any(group => group.Count() == 3) && grouped.Any(group => group.Count() == 2);
            if (isHouse == true)
            {
                int points = 0;
                foreach(int num in list)
                {
                    points += num;
                }
                return points;
            }
            else
            {
                throw new Exception("You don't have a house.");
            }
        }

        public static int Chance(List<int> list)
        {
            int points = 0;
            foreach(int num in list)
            {
                points += num;
            }
            return points;
        }

        public static int Yatzy(List<int> list)
        {
            bool isYatzy = list.All(value => value == list[0]);

            if (isYatzy == true)
            {
                int points = 50;
                //foreach (int num in list)
                //{
                //    points += num;
                //}
                return points;
            }
            else
            {
                throw new Exception("You don't have a yatzy.");
            }
        }
    }
}