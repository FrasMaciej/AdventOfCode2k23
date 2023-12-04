namespace AdventOfCode.Solutions.Solutions2k23.Day3;

public class Day3 : Utilities
{
    private List<string> input;

    public Day3()
    {
        ReadInput();
    }
    
    public void SolveTaskOne()
    {
        int sum = 0;
        for(int i=0; i<input.Count; i++)
        {
            for(int j=0; j<input[i].Length; j++)
            {
                if (char.IsDigit(input[i][j]))
                {
                    (int valueToAdd, int y) = checkIfIsPartNumber(i, j);
                    sum += valueToAdd;
                    j = y;
                }
            }
        }
        
        Console.WriteLine(sum);
    }

    (int, int) checkIfIsPartNumber(int x, int y)
    {
        var neigboursToCheck = new List<(int, int)>
        {
            (-1, 1), (0, 1), (1, 1), (-1, 0), 
            (1, 0), (-1, -1),(0, -1), (1, -1)
        };
        bool isPartNumber = false;
        int value = 0;
        string mergedNumber = "";
        while (y < input[x].Length && char.IsDigit(input[x][y]))
        {
            mergedNumber += input[x][y];
            foreach (var neighbour in neigboursToCheck)
            {
                (int toX, int toY) = neighbour;
                int currX = x + toX;
                int currY = y + toY;
                if (currX >= 0 && currY >=0 && currX < input.Count && currY < input[currX].Length  && !char.IsDigit(input[currX][currY]) && input[currX][currY] != '.')
                {
                    isPartNumber = true;
                }
            }
            y++;
        }

        if (isPartNumber)
        {
            value = Int32.Parse(mergedNumber);
        }
        return (value, y);
    }

    public void SolveTaskTwo()
    {
        //To zostawione na później do przemyślenia jakiegoś sensownego rozwiązania
        int sum = 0;
        for(int i=0; i<input.Count; i++)
        {
            for(int j=0; j<input[i].Length; j++)
            {
                if (input[i][j] == '*')
                {
                    int gearRatio = checkIfIsGear(i, j);
                    sum += gearRatio;
                }
            }
        }
        
        Console.WriteLine(sum);
    }

    int checkIfIsGear(int x, int y)
    {
        var neigboursToCheck = new List<(int, int, string)>
        {
            (-1, 1, "LU"), (0, 1, "U"), (1, 1, "RU"), (-1, 0, "L"), 
            (1, 0, "R"), (-1, -1, "LD"), (0, -1, "D"), (1, -1, "RD")
        };
        
        int value = 0;
        bool isGear = false;
        var numbers = new List<string>();
        var neighboursDigits = new List<(int, int, string)> { };

        foreach (var neighbour in neigboursToCheck)
        {
            (int toX, int toY, string pos) = neighbour;
            int currX = x + toX;
            int currY = y + toY;
            if (currX >= 0 && currY >=0 && currX < input.Count && currY < input[currX].Length)
            {
                if (Char.IsDigit(input[currX][currY]))
                {
                    neighboursDigits.Add((currX, currY, pos));
                }
            }
        }
        
        // bool isR = neighboursDigits.Any(n => n.Item3 == "R");
        // bool isL = neighboursDigits.Any(n => n.Item3 == "L");
        // bool isU = neighboursDigits.Any(n => n.Item3 == "U");
        // bool isD = neighboursDigits.Any(n => n.Item3 == "D");
        // bool isLu = neighboursDigits.Any(n => n.Item3 == "LU");
        // bool isRu = neighboursDigits.Any(n => n.Item3 == "RU");
        // bool isLd = neighboursDigits.Any(n => n.Item3 == "LD");
        // bool isRd = neighboursDigits.Any(n => n.Item3 == "RD");

        // if (isR)
        // {
        //     string num = "";
        //     var neighbour = neighboursDigits.Where(n => n.Item3 == "R");
        //     
        // }
        // if (isL)
        // {
        //     //loop L, save num
        // }
        //
        // if (isU && !isRu  && !isLu)
        // {
        //     // save num
        // } 
        // else if (isU && isRu && isLu)
        // {
        //     // save num
        // }
        // else if (isU && isRu && !isLu)
        // {
        //     //save num, loop R
        // }
        // else if (isU && !isRu && isLu)
        // {
        //     //save num, loop L
        // }
        // else
        // {
        //     if (isRu)
        //     {
        //         // loop R
        //     }
        //
        //     if (isLu)
        //     {
        //         // loop L
        //     }
        // }
        //
        // if (isD && !isRd && !isLd)
        // {
        //     // save num
        // } 
        // else if (isD && isRd && isLd)
        // {
        //     // save num
        // }
        // else if (isD && isRd && !isLd)
        // {
        //     //save num, loop R
        //
        // }
        // else if (isD && !isRd && isLd)
        // {
        //     //save num, loop L
        //
        // }
        // else
        // {
        //     if (isRd)
        //     {
        //         //save num, loop R
        //     }
        //
        //     if (isLd)
        //     {
        //         //save num, loop L
        //     }
        // }
        //
        // if (isGear)
        // {
        //     value = Int32.Parse(num1) * Int32.Parse(num2);
        // }

        return value;
    }
    
    public List<string> ReadInput()
    {
        input = GetInputAsStringList("input_3_2k23.txt");
        return input;
    }
}