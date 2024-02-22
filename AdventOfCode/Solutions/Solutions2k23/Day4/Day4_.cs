namespace AdventOfCode.Solutions.Solutions2k23.Day4;

public class Day4_ : Utilities_
{
    public void SolveTaskOne()
    {
        List<string> input = ReadInput();

        int sum = input
            .Select(line => line.Split(':')[1].Split('|'))
            .Aggregate(0, (acc, s) => acc + GetPoints(-1 + s[0].Split(' ').Intersect(s[1].Split(' ')).Count()));

        // cr_mn: moze lekko bym zmienil ten kod aby korzystac z Sum
        int sum_ = input.Sum(line =>
            {
                var numbersPair = line.Split(':')[1].Split('|').Select(numbers => numbers.Split(' ')).ToArray();
                var wins = numbersPair[0].Intersect(numbersPair[1]).Count();
                return GetPoints(-1 + wins);
            });

        Console.WriteLine(sum);
    }

    int GetPoints(int pointsCount)
    {
        if (pointsCount is 0 or 1 or 2) return pointsCount;
        if (pointsCount > 2) return (int)Math.Pow(2, pointsCount - 1);
        return 0;
    }

    // cr_mn: jak juz jest wyzej taki piekny pattern matching z "switch" to mozna tak
    int GetPoints_(int pointsCount) =>
        pointsCount switch
        {
            0 or 1 or 2 => pointsCount,
            > 2 => (int)Math.Pow(2, pointsCount - 1),
            _ => 0
        };

    public void SolveTaskTwo()
    {
        List<string> input = ReadInput();
        var cardsCounter = new int[input.Count];
        var sum = 0;

        for (int i = 0; i < input.Count; i++)
        {
            // cr_mn: tutaj zmienilem sobie wszystko na "var" i odzielilem w kodzie deklaracje zmiennych od glownej logiki
            var card = input.ElementAt(i);
            var numbers = card.Split(':')[1];
            var numbersSplited = numbers.Split('|');
            var wins = -1 + numbersSplited[0].Split(' ').Intersect(numbersSplited[1].Split(' ')).Count();

            cardsCounter[i] += 1;
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