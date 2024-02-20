namespace AdventOfCode.Solutions.Solutions2k23.Day9;

public class Day9 : Utilities
{

    public void SolveTaskOne()
    {
        var parsedInput = GetInputAsStringList("input_9_2k23.txt")
            .Select(line => line.Split(" ").Select(Int32.Parse).ToList()).ToList();
        var sumOfExtrapolatedValues = parsedInput.Aggregate(0,
            (acc, sequence) => acc + FindNextNumberInSequence(sequence));
        Console.WriteLine(sumOfExtrapolatedValues);
    }

    public int FindNextNumberInSequence(List<int> sequence)
    {
        if (sequence.All(o => o == 0)) return 0;
        var newSequence = sequence.Zip(sequence.Skip(1), (current, next) => next - current).ToList();
        return sequence.Last() + FindNextNumberInSequence(newSequence);
    }
    
    public void SolveTaskTwo()
    {
        var parsedInput = GetInputAsStringList("input_9_2k23.txt")
            .Select(line => line.Split(" ").Select(Int32.Parse).ToList()).ToList();
        var sumOfExtrapolatedValues = parsedInput.Aggregate(0,
            (acc, sequence) =>
            {
                sequence.Reverse();
                return acc + FindNextNumberInSequence(sequence);
            });
        Console.WriteLine(sumOfExtrapolatedValues);
    }
    
}