using System.ComponentModel;
using Microsoft.VisualBasic.FileIO;

namespace AdventOfCode.Solutions.Solutions2k22.Day1;
using System.Linq;

public class Day1 : Utilities
{
    private IList<string> _input;
    public Day1()
    {
        _input = ReadInput();
    }
    
    public int SolveTaskOne()
    {
        int currSum = 0;
        int biggestSum = 0;
        foreach (var val in _input)
        {
            if (val == "")
            {
                biggestSum = currSum > biggestSum ? currSum : biggestSum;
                currSum = 0;
            }
            else
            {
                currSum += Int32.Parse(val);
            }
        }
        return biggestSum;
    }

    public int SolveTaskOneWithLinq()
    {
        int biggestSum = _input
            .Select(val => val == "" ? -1 : Int32.Parse(val))
            .Aggregate(new { CurrentSum = 0, BiggestSum = 0 }, (acc, curr) => new
            {
                CurrentSum = curr == -1 ? 0 : acc.CurrentSum + curr,
                BiggestSum = curr == -1 ? Math.Max(acc.BiggestSum, acc.CurrentSum) : acc.BiggestSum
            })
            .BiggestSum;
        return biggestSum;
    }

    public void SolveTaskTwo()
    {
        throw new NotImplementedException();
    }
    
    public List<string> ReadInput()
    {
        StreamReader sr = GetFileReaderByPath("input_1_2k22.txt");
        var lines = new List<string>();
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() ?? throw new InvalidOperationException();
            lines.Add(line);
        }
        return lines;
    }
}