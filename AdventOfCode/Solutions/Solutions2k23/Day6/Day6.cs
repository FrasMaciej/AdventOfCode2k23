using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Solutions2k23.Day6;

public class Day6 : Utilities
{
    public void SolveTaskOne()
    {
        var input = ReadInput();
        var time = input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        var distance = input[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        var possibleSolutions = new List<int>();

        for(int i = 0; i < distance.Count(); i++)
        {
            possibleSolutions.Add(0);
            for (int j = 0; j < time.ElementAt(i); j++)
            {
                int missingTime = time.ElementAt(i) - j;
                int currDistance = missingTime * j;
                if (currDistance > distance.ElementAt(i))
                {
                    possibleSolutions[i]++;
                }
            }
        }
        
        Console.WriteLine(possibleSolutions.Aggregate((a, x) => a * x));
    }
    
    public void SolveTaskTwo()
    {
        var input = ReadInput();
        Int64 time = Int64.Parse(Regex.Replace(input[0].Split(':')[1], @"\s+", ""));
        Int64 distance = Int64.Parse(Regex.Replace(input[1].Split(':')[1], @"\s+", ""));
        int possibleSolutions = 0;
        
        for (int i = 0; i < time; i++)
        {
            Int64 missingTime = time - i;
            Int64 currDistance = missingTime * i;
            if (currDistance > distance)
            {
                possibleSolutions++;
            }
        }
        
        Console.WriteLine(possibleSolutions);
    }
    
    private List<string> ReadInput()
    {
        return GetInputAsStringList("input_6_2k23.txt");
    }
}