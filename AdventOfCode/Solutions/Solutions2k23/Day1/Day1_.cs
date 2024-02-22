namespace AdventOfCode.Solutions.Solutions2k23.Day1;

using System.Data;
using System.Linq;

// using static System.String;
// using static System.Int32;

public class Day1_ : Utilities_
{
    public void SolveTaskOne()
    {
        List<string> input = ReadInput();

        // cr_mn: vs code podpowiada aby w kodzie uzywac aliasow do prymitywnych typow,
        // czyli string zamiast String, int zamiast Int32, char zamiast Char, ...

        // cr_mn: funkcje string.Concat/Join sa bardzo uzyteczne wydajnie pracuja z napisami i IEnumerable<T>,
        // czyli unikamy tablic/list czy tworzenia nowych instancji napisow sklejajac napisy
        // -> mozna tak zapisc ".Select(i => string.Concat(i.Where(char.IsDigit)))"
        // -> bylo ".Select(i => new string(i.Where(char.IsDigit).ToArray()))"
        var sum = input
            .Select(i => string.Concat(i.Where(char.IsDigit)))
            .Aggregate(0, (acc, s) => acc + int.Parse(s[0].ToString() + s[^1]));

        // cr_mn: Aggregate jest bardzo ogolny i mozna nim zrobic wiele rzeczy, 
        // tutaj pewnie lepiej skorzysac z dedykownego Sum
        var sum2 = input.Sum(line =>
        {
            var digits = line.Where(char.IsDigit).ToList();
            return int.Parse($"{digits[0]}{digits[^1]}");
        });

        Console.WriteLine(sum);
    }

    public void SolveTaskTwo()
    {
        List<string> input = ReadInput();
        int sum = input
            .Select(MapStringsToNumbers)
            .Select(i => new string(i.Where(char.IsDigit).ToArray()))
            .Aggregate(0, (acc, s) => acc + Int32.Parse(s[0].ToString() + s[^1]));
        Console.WriteLine(sum);
    }

    string MapStringsToNumbers(string s)
    {
        var dict = new Dictionary<string, string>
        {
            {"one", "o1e"}, {"two", "t2o"}, {"three", "t3e"},
            {"four", "f4r"}, {"five", "f5e"}, {"six", "s6"},
            {"seven", "s7n"}, {"eight", "e8t"}, {"nine", "n9n"}
        };

        // cr_mn: tutaj wygodnie jest skorzystac z "var
        foreach (var entry in dict) // foreach (KeyValuePair<string, string> entry in dict)
        {
            s = s.Replace(entry.Key, entry.Value);
        }

        return s;
    }

    // cr_mn: lub przepisac na uzycie LINQ
    private static Dictionary<string, string> mapping = new()
    {
        {"one", "o1e"}, {"two", "t2o"}, {"three", "t3e"},
        {"four", "f4r"}, {"five", "f5e"}, {"six", "s6"},
        {"seven", "s7n"}, {"eight", "e8t"}, {"nine", "n9n"}
    };

    private string MapStringsToNumbers2(string s) =>
        mapping.Aggregate(s, (str, entry) => str.Replace(entry.Key, entry.Value));



    private List<string> ReadInput()
    {
        return GetInputAsStringList("input_1_2k23.txt");
    }
}