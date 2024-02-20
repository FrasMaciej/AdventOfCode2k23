using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Solutions2k23.Day6;

public class Day6 : Utilities
{
    public void SolveTaskOne()
    {
        var input = GetInputAsStringList("input_6_2k23.txt");
        var time = input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var distance = input[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var possibleSolutions = new List<int>();

        for(int i = 0; i < distance.Count; i++)
        {
            possibleSolutions.Add(0);
            for (int j = 0; j < time[i]; j++)
            {
                var missingTime = time[i] - j;
                var currDistance = missingTime * j;
                if (currDistance > distance[i]) possibleSolutions[i]++;
            }
        }
        
        Console.WriteLine(possibleSolutions.Aggregate((a, x) => a * x));
    }
    
    public void SolveTaskTwo()
    {
        var input = GetInputAsStringList("input_6_2k23.txt");
        var time = Int64.Parse(Regex.Replace(input[0].Split(':')[1], @"\s+", ""));
        var distance = Int64.Parse(Regex.Replace(input[1].Split(':')[1], @"\s+", ""));
        var possibleSolutions = 0;
        
        for (int i = 0; i < time; i++)
        {
            var missingTime = time - i;
            var currDistance = missingTime * i;
            if (currDistance > distance) possibleSolutions++;
        }
        
        Console.WriteLine(possibleSolutions);
    }

}