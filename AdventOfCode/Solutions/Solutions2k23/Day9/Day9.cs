namespace AdventOfCode.Solutions.Solutions2k23.Day9;

public class Day9 : Utilities
{
    public void SolveTaskOne() => SolvePuzzle(false);
    public void SolveTaskTwo() => SolvePuzzle(true);
    
    void SolvePuzzle(bool reversed)
    {
        var parsedInput = GetInputAsStringList("input_9_2k23.txt")
            .Select(line => line.Split(" ").Select(int.Parse).ToList());
        var sumOfExtrapolatedValues = parsedInput.Aggregate(0,
            (acc, sequence) => acc + FindNextNumberInSequence(reversed ? sequence.AsEnumerable().Reverse().ToList() : sequence));
        Console.WriteLine(sumOfExtrapolatedValues);
    }
    
    int FindNextNumberInSequence(List<int> sequence)
    {
        if (sequence.All(o => o == 0)) return 0;
        var newSequence = sequence.Zip(sequence.Skip(1), (current, next) => next - current).ToList();
        return sequence.Last() + FindNextNumberInSequence(newSequence);
    }
}