namespace AdventOfCode.Solutions.Solutions2k23.Day2;

public class Day2_ : Utilities_
{
    // cr_mn: w sumie to sam nie wiem ale chyba w C# wszystkie zmienne definiowalbym za pomoca "var",
    // np chyba ze bardziej czytelne byloby podanie jawnie typu
    // - tak pomyslalem o tym gdy czytalem kod i mamy wiele zmiennych pod soba i kazda jest innego typu
    // to nie ma takich "rownych wciec" od lewej 
    public void SolveTaskOne()
    {
        var input = ReadInput();
        var sum = 0;

        foreach (var line in input)
        {
            var substrings = line.Split(':');
            var index = int.Parse(substrings[0].Where(char.IsDigit).ToArray());
            var picks = substrings[1].Split(';');

            var isPossible = true;
            foreach (var pick in picks)
            {
                if (!IsPickPossible(pick))
                {
                    isPossible = false;
                    // cr_mn: tutaj chyba mozna przerwac petle za pomoca break;
                    break;
                }
            }

            if (isPossible)
            {
                sum += index;
            }
        }
        Console.WriteLine(sum);
    }

    private static bool IsPickPossible(string pick)
    {
        var choices = pick.Split(',');
        foreach (var choice in choices)
        {
            var s = choice.Split(' ');
            // cr_mn: tak dla sportu zapisalem sobie to za pomoca pattern matching

            if ((s[2], int.Parse(s[1])) is ("red", > 12) or ("green", > 13) or ("blue", > 14))
            {
                return false;
            }

            // int num = Int32.Parse(s[1]);
            // string color = s[2];
            // if (color == "red" && num > 12)
            // {
            //     return false;
            // }
            // if (color == "green" && num > 13)
            // {
            //     return false;
            // }
            // if (color == "blue" && num > 14)
            // {
            //     return false;
            // }
        }
        return true;
    }

    public void SolveTaskTwo()
    {
        var input = ReadInput();
        var sum = 0;

        foreach (var line in input)
        {
            var substrings = line.Split(':');
            var picks = substrings[1].Split(';');
            var maxPairs = new Dictionary<string, int>() { { "red", 0 }, { "green", 0 }, { "blue", 0 }, };
            foreach (var pick in picks)
            {
                var currPairs = GetCubesNumbers(pick);
                foreach (var pair in currPairs)
                {
                    if (pair.Value > maxPairs[pair.Key])
                    {
                        maxPairs[pair.Key] = pair.Value;
                    }
                }
            }

            //sum += maxPairs["red"] * maxPairs["green"] * maxPairs["blue"];
            // cr_mn: mozna ewentualnie przeleciec po elementach slownika
            sum += maxPairs.Values.Aggregate(1, (a, b) => a * b);
        }

        Console.WriteLine(sum);
    }

    private Dictionary<string, int> GetCubesNumbers(string pick)
    {
        var choices = pick.Split(',');
        var pairs = new Dictionary<string, int>();
        foreach (var choice in choices)
        {
            var s = choice.Split(' ');
            var num = int.Parse(s[1]);
            var color = s[2];
            pairs[color] = num;
        }
        return pairs;
    }

    public List<string> ReadInput()
    {
        return GetInputAsStringList("input_2_2k23.txt");
    }
}