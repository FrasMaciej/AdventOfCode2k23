namespace AdventOfCode.Solutions.Solutions2k23.Day8;

public class Day8_ : Utilities_
{
    public void SolveTaskOne()
    {


        var input = GetInputAsStringList("input_8_2k23.txt");
        var moves = input[0].ToCharArray();
        var pairs = input
            .Skip(2)
            // cr_mn: zamiast wieli .Select().Select()... tylko jeden Select + Aggregate
            // .Select(s => s.Replace("= ", string.Empty))
            // .Select(s => s.Replace("(", string.Empty))
            // .Select(s => s.Replace(")", string.Empty))
            // .Select(s => s.Replace(",", string.Empty))
            .Select(s => new[] { "= ", "(", ")", "," }.Aggregate(s, (ss, c) => ss.Replace(c, string.Empty)))
            .Select(s => s.Split(' '))
            // cr_mn: ogolnie mamy stary typ Tuple oraz nowy ValueTyple ktory tez posiada wspatcie w C# ze piszemy (..., ...)
            //.ToDictionary(strArr => strArr[0], strArr => new Tuple<string, string>(strArr[1], strArr[2]));
            .ToDictionary(strArr => strArr[0], strArr => (strArr[1], strArr[2]));

        var position = "AAA";
        var movesMade = 0;
        var finished = false;

        while (!finished)
        {
            foreach (var move in moves)
            {
                if (move == 'L')
                    position = pairs[position].Item1;
                if (move == 'R')
                    position = pairs[position].Item2;
                movesMade++;
            }
            if (position == "ZZZ")
                finished = true;
        }

        Console.WriteLine(movesMade);

    }

    public void SolveTaskTwo()
    {
        var input = GetInputAsStringList("input_8_2k23.txt");
        var moves = input[0].ToCharArray();
        var pairs = input
            .Skip(2)
            .Select(s => s.Replace("= ", string.Empty))
            .Select(s => s.Replace("(", string.Empty))
            .Select(s => s.Replace(")", string.Empty))
            .Select(s => s.Replace(",", string.Empty))
            .Select(s => s.Split(' '))
            .ToDictionary(strArr => strArr[0], strArr => new Tuple<string, string>(strArr[1], strArr[2]));

        var positions = pairs.Where(s => s.Key[2] == 'A').Select(s => s.Key).ToList();
        int movesMade = 0;
        var allMoves = new List<int>();

        string position;

        foreach (var pos in positions)
        {
            var finished = false;
            position = pos;
            movesMade = 0;
            while (!finished)
            {
                foreach (var move in moves)
                {
                    if (move == 'L')
                        position = pairs[position].Item1;
                    if (move == 'R')
                        position = pairs[position].Item2;
                    movesMade++;
                }

                if (position[2] == 'Z')
                {
                    allMoves.Add(movesMade);
                    finished = true;
                }
            }
        }

        var valuesToCheck = allMoves.Select(m => Convert.ToInt64(m)).ToArray();
        Console.WriteLine(FindLCM(valuesToCheck));
    }

    long FindGCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    long FindLCM(long a, long b)
    {
        return a * b / FindGCD(a, b);
    }

    long FindLCM(long[] numbers)
    {
        long lcm = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            lcm = FindLCM(lcm, numbers[i]);
        }
        return lcm;
    }

}