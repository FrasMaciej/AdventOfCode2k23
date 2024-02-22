namespace AdventOfCode.Solutions.Solutions2k23.Day3;

public class Day3_ : Utilities_
{
    private List<string> _input;

    public Day3_()
    {
        _input = GetInputAsStringList("input_3_2k23.txt");
    }

    // cr_mn: ja chyba lubie oddzielic enterem deklaracje zmiennych metody od dalszego, pewnie nie zawsze
    // ale jak jest duzo kod nonstop to zlewa mi sie jakos

    public void SolveTaskOne()
    {
        int sum = 0;

        for (int i = 0; i < _input.Count; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (char.IsDigit(_input[i][j]))
                {
                    (int valueToAdd, int y) = CheckIfIsPartNumber(i, j);
                    sum += valueToAdd;
                    j = y;
                }
            }
        }

        Console.WriteLine(sum);
    }

    (int, int) CheckIfIsPartNumber(int x, int y)
    {
        // cr_mn: tutaj dla sportu nizej pokazalem jak mozna wygodnie skorzystac z nowosci C#12 (.net8, 2023) Collection expressions
        //var neigboursToCheck = new List<(int, int)> { (-1, 1), (0, 1), (1, 1), (-1, 0), (1, 0), (-1, -1), (0, -1), (1, -1) };        
        List<(int, int)> neigboursToCheck = [(-1, 1), (0, 1), (1, 1), (-1, 0), (1, 0), (-1, -1), (0, -1), (1, -1)];
        var isPartNumber = false;
        var value = 0;
        var mergedNumber = "";

        while (y < _input[x].Length && char.IsDigit(_input[x][y]))
        {
            mergedNumber += _input[x][y];
            foreach (var neighbour in neigboursToCheck)
            {
                (int toX, int toY) = neighbour;
                int currX = x + toX;
                int currY = y + toY;


                if (currX >= 0 && currY >= 0 && currX < _input.Count && currY < _input[currX].Length &&
                    !char.IsDigit(_input[currX][currY]) && _input[currX][currY] != '.')
                {
                    isPartNumber = true;
                    // cr_mn: tutaj chyba takze mozna przerwac petle za pomoca break;
                    break;
                }
            }
            y++;
        }

        if (isPartNumber)
        {
            value = int.Parse(mergedNumber);
        }

        return (value, y);
    }

    public void SolveTaskTwo()
    {
        int sum = 0;

        for (int i = 0; i < _input.Count; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (_input[i][j] == '*')
                {
                    int gearRatio = CheckIfIsGear(i, j);
                    sum += gearRatio;
                }
            }
        }

        Console.WriteLine(sum);
    }

    // cr_mn: odpuszcze chyba szczegolowe analizowanie funkcji nizek :) jest git

    int CheckIfIsGear(int x, int y)
    {
        var numbers = new List<int>();

        // ---- Sprawdzenie lewej strony od *
        string mergedNumber = "";
        for (int i = -1; y + i >= 0 && y + i < _input[x].Length && char.IsDigit(_input[x][y + i]); i--)
        {
            mergedNumber = mergedNumber.Insert(0, _input[x][y + i].ToString());
        }
        if (mergedNumber.Length > 0) numbers.Add(Int32.Parse(mergedNumber));

        // ---- Sprawdzenie prawej strony od *
        mergedNumber = "";
        for (int i = 1; y + i >= 0 && y + i < _input[x].Length && char.IsDigit(_input[x][y + i]); i++)
        {
            mergedNumber += _input[x][y + i];
        }
        if (mergedNumber.Length > 0) numbers.Add(Int32.Parse(mergedNumber));

        // ---- Sprawdzenie górnej części (powyżej *)
        int leftPointer = 0;
        int rightPointer = 0;

        // ---- Sprawdzenie części powyżej * "od środka"
        mergedNumber = "";
        if (x - 1 >= 0 && x - 1 < _input.Count && char.IsDigit(_input[x - 1][y]))
        {
            mergedNumber += _input[x - 1][y];
            for (int i = 1; y + i >= 0 && y + i < _input[x].Length && char.IsDigit(_input[x - 1][y + i]); i++, rightPointer = i)
            {
                mergedNumber += _input[x - 1][y + i];
            }

            for (int i = -1; y + i >= 0 && y + i < _input[x].Length && char.IsDigit(_input[x - 1][y + i]); i--, leftPointer = i)
            {
                mergedNumber = mergedNumber.Insert(0, _input[x - 1][y + i].ToString());
            }
        }
        if (mergedNumber.Length > 0) numbers.Add(Int32.Parse(mergedNumber));

        // ---- sprawdzenie tego co zostało po prawej stronie
        mergedNumber = "";
        if (rightPointer + 1 < 2)
        {
            for (int i = rightPointer + 1; (y + i >= 0 && y + i < _input[x].Length) && (char.IsDigit(_input[x - 1][y + i]) || i < 1); i++)
            {
                if (char.IsDigit(_input[x - 1][y + i])) mergedNumber += _input[x - 1][y + i];
            }
            if (mergedNumber.Length > 0) numbers.Add(Int32.Parse(mergedNumber));
        }

        // ---- sprawdzenie tego co zostało po lewej stronie
        mergedNumber = "";
        if (leftPointer - 1 > -2)
        {
            for (int i = leftPointer - 1; (y + i >= 0 && y + i < _input[x].Length) && (char.IsDigit(_input[x - 1][y + i]) || i > -1); i--)
            {
                if (char.IsDigit(_input[x - 1][y + i])) mergedNumber = mergedNumber.Insert(0, _input[x - 1][y + i].ToString());
            }
            if (mergedNumber.Length > 0) numbers.Add(Int32.Parse(mergedNumber));
        }

        // ---- Sprawdzenie dolnej części (powyżej *)
        leftPointer = 0;
        rightPointer = 0;

        // ---- Sprawdzenie części ponizej * "od środka"
        mergedNumber = "";
        if (char.IsDigit(_input[x + 1][y]))
        {
            mergedNumber += _input[x + 1][y];
            for (int i = 1; (y + i >= 0 && y + i < _input[x].Length) && char.IsDigit(_input[x + 1][y + i]); i++, rightPointer = i)
            {
                mergedNumber += _input[x + 1][y + i];
            }

            for (int i = -1; (y + i >= 0 && y + i < _input[x].Length) && char.IsDigit(_input[x + 1][y + i]); i--, leftPointer = i)
            {
                mergedNumber = mergedNumber.Insert(0, _input[x + 1][y + i].ToString());
            }
        }
        if (mergedNumber.Length > 0) numbers.Add(Int32.Parse(mergedNumber));

        // ---- sprawdzenie tego co zostało po prawej stronie
        mergedNumber = "";
        if (rightPointer + 1 < 2)
        {
            for (int i = rightPointer + 1; y + i >= 0 && y + i < _input[x].Length && (char.IsDigit(_input[x + 1][y + i]) || i < 1); i++)
            {
                if (char.IsDigit(_input[x + 1][y + i])) mergedNumber += _input[x + 1][y + i];
            }
            if (mergedNumber.Length > 0) numbers.Add(Int32.Parse(mergedNumber));
        }

        // ---- sprawdzenie tego co zostało po lewej stronie
        mergedNumber = "";
        if (leftPointer - 1 > -2)
        {
            for (int i = leftPointer - 1; y + i >= 0 && y + i < _input[x].Length && (char.IsDigit(_input[x + 1][y + i]) || i > -1); i--)
            {
                if (char.IsDigit(_input[x + 1][y + i])) mergedNumber = mergedNumber.Insert(0, _input[x + 1][y + i].ToString());
            }
            if (mergedNumber.Length > 0) numbers.Add(Int32.Parse(mergedNumber));
        }


        int value = 0;
        if (numbers.Count == 2)
        {
            value = numbers[0] * numbers[1];
        }

        return value;
    }

}