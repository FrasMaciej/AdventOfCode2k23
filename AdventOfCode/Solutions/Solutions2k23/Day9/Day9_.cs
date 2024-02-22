namespace AdventOfCode.Solutions.Solutions2k23.Day9;

public class Day9_ : Utilities_
{
    public void SolveTaskOne() => Console.WriteLine(SolvePuzzle(false));
    public void SolveTaskTwo() => Console.WriteLine(SolvePuzzle(true));

    int SolvePuzzle(bool reversed) => GetInputAsStringList("input_9_2k23.txt")
        .Select(line => line.Split(" ").Select(int.Parse).ToList())
        // cr_mn: Sum zamiast Aggregate :)
        //.Aggregate(0, (acc, sequence) => acc + FindNextNum(reversed ? sequence.AsEnumerable().Reverse().ToList() : sequence));
        .Sum(sequence => FindNextNum(reversed ? sequence.AsEnumerable().Reverse().ToList() : sequence));

    int FindNextNum(List<int> sequence) =>
        sequence.All(o => o == 0) ? 0 : sequence.Last() + FindNextNum(sequence.Zip(sequence.Skip(1), (current, next) => next - current).ToList());
}