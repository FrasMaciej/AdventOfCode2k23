namespace AdventOfCode.Solutions.Solutions2k23.Day4;

public class Day4 : Utilities
{
    public void SolveTaskOne()
    {
        List<string> input = ReadInput();
        int sum = input
            .Select(line => line.Split(':')[1].Split('|'))
            .Aggregate(0, (acc, s) => acc + GetPoints(-1 + s[0].Split(' ').Intersect(s[1].Split(' ')).Count(), acc));
        Console.WriteLine(sum);
    }

    int GetPoints(int pointsCount, int acc)
    {
        if (pointsCount is 0 or 1 or 2) return pointsCount;
        if (pointsCount > 2) return (int)Math.Pow(2, pointsCount - 1);
        return 0;
    }
    
    public void SolveTaskTwo()
    {
        List<string> input = ReadInput();
        var cardsCounter = new int[input.Count];
        var sum = 0;
        
        for (int i = 0; i < input.Count; i++)
        {
            cardsCounter[i] += 1;
            string card = input.ElementAt(i);
            string numbers = card.Split(':')[1];
            var numbersSplited = numbers.Split('|');
            int wins = -1 + numbersSplited[0].Split(' ').Intersect(numbersSplited[1].Split(' ')).Count();
            for (int j = 1; j <= wins; j++)
            {
                cardsCounter[i + j] += 1 * cardsCounter[i];
            }
            sum += cardsCounter[i];
        }
        Console.WriteLine(sum);
    }
    
    private List<string> ReadInput()
    {
        return GetInputAsStringList("input_4_2k23.txt");
    }
}