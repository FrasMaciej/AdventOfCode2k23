namespace AdventOfCode.Solutions.Solutions2k23.Day10;

public class Day10 : Utilities
{
    public void SolveTaskOne()
    {
        var input = GetInputAsStringList("input_10_2k23.txt").Select(line => line.ToArray()).ToArray();
        int startX = Array.FindIndex(input, row => Array.IndexOf(row, 'S') != -1);
        int startY = Array.IndexOf(input[startX], 'S');
        // To-do -> znaleźć najkrtótszą drogę do najbardziej oddalonego punktu w grafie na podstawie połączeń
        // | is a vertical pipe connecting north and south.
        // - is a horizontal pipe connecting east and west.
        // L is a 90-degree bend connecting north and east.
        // J is a 90-degree bend connecting north and west.
        // 7 is a 90-degree bend connecting south and west.
        // F is a 90-degree bend connecting south and east.
        // . is ground; there is no pipe in this tile.
        // S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
    }
    
    public void SolveTaskTwo()
    {
        var input = GetInputAsStringList("input_10_2k23.txt");
    }
}