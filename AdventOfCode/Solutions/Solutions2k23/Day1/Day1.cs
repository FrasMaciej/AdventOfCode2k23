namespace AdventOfCode.Solutions.Solutions2k23.Day1;
using System.Linq;

public class Day1 : Utilities
{
    public void SolveTaskOne()
    {
        List<string> input = ReadInput();
        var sum = input
            .Select(i => new string(i.Where(char.IsDigit).ToArray()))
            .Aggregate(0, (acc, s) => acc + Int32.Parse(s[0].ToString() + s[^1]));
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
        foreach(KeyValuePair<string, string> entry in dict)
        {
            s = s.Replace(entry.Key, entry.Value);
        }
        return s;
    }
    
    private List<string> ReadInput()
    {
        return GetInputAsStringList("input_1_2k23.txt");
    }
}