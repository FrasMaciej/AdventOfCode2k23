namespace AdventOfCode.Solutions.Solutions2k23.Day5;

// cr_mn: wszedzie zamiast "Int64" dalem "long" 

public class Day5_ : Utilities_
{
    // private List<string> _input;
    // cr_mn: tutaj byl warning podczas kompilacji
    // warning CS8618: Non-nullable field '_input' must contain a non-null value when exiting constructor. 
    // Consider declaring the field as nullable
    // - zmienilem na zmienne w metodach nizej

    public void SolveTaskOne()
    {
        var _input = GetInputAsStringList("input_5_2k23.txt");
        var headers = new string[]
        {
            "seed-to-soil map:", "soil-to-fertilizer map:", "fertilizer-to-water map:", "water-to-light map:",
            "light-to-temperature map:", "temperature-to-humidity map:", "humidity-to-location map:"
        };

        // cr_mn: zamiast "seed => Int64.Parse(seed)" mozna zastosowac "point-free notation"
        // - zreszta troche nizej dokladnie tak napisales
        var seeds = _input[0].Split(" ").Skip(1).Select(long.Parse).ToList();
        var maps = new List<List<List<long>>>();

        foreach (var header in headers)
        {
            var mapList = _input
                .Skip(2)
                .SkipWhile(line => line != header)
                .Skip(1)
                .TakeWhile(line => line != "")
                .Select(line => line.Split(" ").Select(long.Parse).ToList())
                .ToList();
            maps.Add(mapList);
        }

        // cr_mn: alternatywa takze za pomoca LINQ ale przechodzi sekwencje lini tylko raz i nie korzystac z "headers"
        var maps_ = _input.Skip(2).Where(line => !string.IsNullOrEmpty(line)).Aggregate(
            new List<List<List<long>>>(), (maps, line) =>
            {
                if (line.EndsWith(":"))
                {
                    maps.Add(new List<List<long>>());
                }
                else
                {
                    maps.Last().Add(line.Split(" ").Select(long.Parse).ToList());
                }
                return maps;
            });


        long lowestValue = long.MaxValue;

        foreach (var seed in seeds)
        {
            long value = seed;
            foreach (var x in maps)
            {
                foreach (var y in x)
                {
                    var (target, source, range) = (y[0], y[1], y[2]);
                    if (value >= source && value <= source + range)
                    {
                        long diff = source - target;
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
        var _input = GetInputAsStringList("input_5_2k23.txt");
        var headers = new string[]
        {
            "seed-to-soil map:", "soil-to-fertilizer map:", "fertilizer-to-water map:", "water-to-light map:",
            "light-to-temperature map:", "temperature-to-humidity map:", "humidity-to-location map:"
        };

        var seeds = _input[0].Split(" ").Skip(1).Select(seed => long.Parse(seed)).ToList();
        var maps = new List<List<List<long>>>();

        foreach (var header in headers)
        {
            var mapList = _input
                .Skip(2)
                .SkipWhile(line => line != header)
                .Skip(1)
                .TakeWhile(line => line != "")
                .Select(line => line.Split(" ").Select(long.Parse).ToList())
                .ToList();
            maps.Add(mapList);
        }

        long lowestValue = long.MaxValue;

        for (int i = 0; i < seeds.Count; i += 2)
        {
            Console.WriteLine("Counting Seed: " + (i / 2 + 1));
            long start = seeds[i];
            long seedRange = start + seeds[i + 1];
            for (long j = start; j < seedRange; j++)
            {
                long currentValue = j;
                foreach (var x in maps)
                {
                    foreach (var y in x)
                    {
                        var (target, source, range) = (y[0], y[1], y[2]);
                        if (currentValue >= source && currentValue < source + range)
                        {
                            long diff = source - target;
                            currentValue -= diff;
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