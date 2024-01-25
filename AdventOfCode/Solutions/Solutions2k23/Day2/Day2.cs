namespace AdventOfCode.Solutions.Solutions2k23.Day2;

public class Day2 : Utilities
{
    public void SolveTaskOne()
    {
        List<string> input = ReadInput();
        int sum = 0;
        foreach (var line in input)
        {
            string[] substrings = line.Split(':');
            int index = Int32.Parse(substrings[0].Where(Char.IsDigit).ToArray());
            string[] picks = substrings[1].Split(';');
            bool isPossible = true;
            foreach (var pick in picks)
            {
                if (!IsPickPossible(pick))
                {
                    isPossible = false;
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
        string[] choices = pick.Split(',');
        foreach (var choice in choices)
        {
            string[] s = choice.Split(' ');
            int num = Int32.Parse(s[1]);
            string color = s[2];
            if (color == "red" && num > 12)
            {
                return false;
            } 
            if (color == "green" && num > 13)
            {
                return false;
            } 
            if (color == "blue" && num > 14)
            {
                return false;
            }
        }
        return true;
    }

    public void SolveTaskTwo()
    {
        List<string> input = ReadInput();
        int sum = 0;
        foreach (var line in input)
        {
            string[] substrings = line.Split(':');
            string[] picks = substrings[1].Split(';');
            var maxPairs = new Dictionary<string, int>() {
                {"red", 0}, {"green", 0}, {"blue", 0}, 
            };
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

            sum += maxPairs["red"] * maxPairs["green"] * maxPairs["blue"];
        }
        Console.WriteLine(sum);
    }
    
    private Dictionary<string, int> GetCubesNumbers(string pick)
    {
        string[] choices = pick.Split(',');
        var pairs = new Dictionary<string, int>();
        foreach (var choice in choices)
        {
            string[] s = choice.Split(' ');
            int num = Int32.Parse(s[1]);
            string color = s[2];
            pairs[color] = num;
        }
        return pairs;
    }
    
    public List<string> ReadInput()
    {
        return GetInputAsStringList("input_2_2k23.txt");
    }

}