namespace AdventOfCode.Solutions.Solutions2k23.Day5;

public class Day5 : Utilities
{
    private List<string> _input;

    public void SolveTaskOne()
    {
        _input = GetInputAsStringList("input_5_2k23.txt");
        var headers = new string[]
        {
            "seed-to-soil map:", "soil-to-fertilizer map:", "fertilizer-to-water map:", "water-to-light map:",
            "light-to-temperature map:", "temperature-to-humidity map:", "humidity-to-location map:"
        };
        
        var seeds = _input[0].Split(" ").Skip(1).Select(seed => Int64.Parse(seed)).ToList();
        var maps = new List<List<List<Int64>>>();
        
        foreach (var header in headers)
        {
            var mapList =_input
                .Skip(2)
                .SkipWhile(line => line != header)
                .Skip(1)
                .TakeWhile(line => line != "")
                .Select(line => line.Split(" ").Select(Int64.Parse).ToList())
                .ToList();
            maps.Add(mapList);
        }

        Int64 lowestValue = Int64.MaxValue;
        
        foreach (var seed in seeds)
        {
            Int64 value = seed;
            foreach (var x in maps)
            {
                foreach (var y in x)
                {
                    var (target, source, range) = (y[0], y[1], y[2]);
                    if (value >= source && value <= source + range)
                    {
                        Int64 diff = source - target;
                        value -= diff;
                        break;
                    }
                }
            }
            if (value < lowestValue) lowestValue = value;
        }

        Console.WriteLine(lowestValue);
    }
    
    public void SolveTaskTwo()
    {
        _input = GetInputAsStringList("input_5_2k23.txt");
        var headers = new string[]
        {
            "seed-to-soil map:", "soil-to-fertilizer map:", "fertilizer-to-water map:", "water-to-light map:",
            "light-to-temperature map:", "temperature-to-humidity map:", "humidity-to-location map:"
        };
        
        var seeds = _input[0].Split(" ").Skip(1).Select(seed => Int64.Parse(seed)).ToList();
        var maps = new List<List<List<Int64>>>();
        
        foreach (var header in headers)
        {
            var mapList =_input
                .Skip(2)
                .SkipWhile(line => line != header)
                .Skip(1)
                .TakeWhile(line => line != "")
                .Select(line => line.Split(" ").Select(Int64.Parse).ToList())
                .ToList();
            maps.Add(mapList);
        }

        Int64 lowestValue = Int64.MaxValue;
        
        for(int i = 0; i<seeds.Count; i+=2)
        {
            Console.WriteLine(i);
            Int64 start = seeds[i];
            Int64 seedRange = start + seeds[i + 1] - 1;
            for (long j = start; j<=seedRange; j++)
            {
                Int64 currentValue = j;
                foreach (var x in maps)
                {
                    foreach (var y in x)
                    {
                        var (target, source, range) = (y[0], y[1], y[2]);
                        if (currentValue >= source && currentValue <= source + range)
                        {
                            Int64 diff = currentValue - source;
                            currentValue = target + diff;
                            break;
                        }
                    }
                }
                lowestValue = long.Min(lowestValue, currentValue);
            }
        }

        Console.WriteLine(lowestValue);
    }
    
}