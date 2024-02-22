namespace AdventOfCode.Solutions.Solutions2k23.Day7;

using System.Data.Common;
using System.Text.RegularExpressions;

public class Day7_ : Utilities_
{
    // cr_mn: taka prosta klase ktora jest pojemnikiem niezmienniczym na dane mozna zapisac jako rekord C#

    record Card(string Hand, int Bid, int Power);

    // private class Card
    // {
    //     public string Hand;
    //     public int Bid;
    //     public int Power;

    //     public Card(string hand, int bid, int power)
    //     {
    //         Hand = hand;
    //         Bid = bid;
    //         Power = power;
    //     }
    // }

    public void SolveTaskOne()
    {
        var cardsPower = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };
        var input = ReadInput();

        // int iterator = 0;

        // cr_mn: faktycznie jak sam zaznaczyles opowiadajac o tym kodzie, ta zmienna "iterator" to bardzo nie ladne rozwiazanie
        // kod ponizej jest bardzo "funkcyjny", wszystko jest "immutable", wszystkie lambdy sa "pure", ...
        // .. tutaj nagle modyfikowanie stanu z kosmosu, a poprawka  bardzo czesto jest bardzo prosta

        var result = input.Select(i =>
            {
                var splittedString = i.Split(" ").ToList();
                return new Card(splittedString[0], int.Parse(splittedString[1]), CheckHandType(splittedString[0], false));
            })
            .GroupBy(c => c.Power)
            .OrderBy(g => g.Key)
            .SelectMany(c => c.OrderBy(card => card.Hand, new CardComparer(cardsPower)))
            .Aggregate((Total: 0, Index: 1), (acc, card) => (acc.Total + card.Bid * acc.Index, acc.Index + 1))
            .Total;

        Console.WriteLine(result);
    }

    // cr_mn: tutaj mozemy sobie wykorzystac jeszcze cieplutki element C# 12 czyli "primary constructor" dla klas
    class CardComparer(List<string> order) : IComparer<string>
    {
        // private readonly List<string> order;

        // public CardComparer(List<string> order)
        // {
        //     this.order = order;
        // }

        // cr_mn: tutaj byl taki warning
        // Nullability of reference types in type of parameter 'x' of 'int CardComparer.Compare(string x, string y)' doesn't match 
        // implicitly implemented member 'int IComparer<string>.Compare(string? x, string? y)' (possibly because of nullability attributes).
        // - nalezalo zmienic sygnature metody dodajac ? (string? x), oraz dostosowac kod

        public int Compare(string? x, string? y)
        {
            // cr_mn: super hackerski kod ;) ktory rzuca ladny wyjatek jak ktorys z argumentow jest null
            _ = x ?? throw new ArgumentNullException(nameof(x));
            _ = y ?? throw new ArgumentNullException(nameof(y));

            int minLength = Math.Min(x.Length, y.Length);

            for (int i = 0; i < minLength; i++)
            {
                int indexX = order.IndexOf(x[i].ToString());
                int indexY = order.IndexOf(y[i].ToString());

                if (indexX < indexY)
                    return -1;
                if (indexX > indexY)
                    return 1;
            }

            return x.Length.CompareTo(y.Length);
        }
    }

    private int CheckHandType(string hand, Boolean useJokers)
    {
        var counts = hand.GroupBy(x => x).ToDictionary(group => group.Key, group => group.Count());

        if (useJokers && counts.Count != 1 && counts.ContainsKey('J'))
        {
            // cr_mn: zamiast uniwersalnego Aggregate mozna uzyc dedykowanego MaxBy
            //var maxKey = counts.Where(pair => pair.Key != 'J').Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            var maxKey = counts.Where(pair => pair.Key != 'J').MaxBy(pair => pair.Value).Key;

            counts[maxKey] += counts['J'];
            counts.Remove('J');
        }

        // cr_mn: mozna sprobowac pattern matching, ale generalnie zwracanie tych kolejnych indeksow jest takie "sliskie" bo mozna latwo sie pomylic
        // - nizej w kodzie uzywany jest "collection expression", nowosc C# 12 ze mozn stworzyc kolekcje za pomoca [..,..]
        var result1 = counts switch
        {
            { Count: 1 } => 7,
            _ when counts.ContainsValue(4) => 6,
            _ when counts.ContainsValue(2) && counts.ContainsValue(3) => 5,
            _ when counts.ContainsValue(3) => 4,
            _ when counts.Values.OrderBy(x => x).SequenceEqual([1, 2, 2]) => 3,
            _ when counts.ContainsValue(2) => 2,
            _ => 1
        };

        // cr_mn: albo taki sprytny pattern matching
        var result2 = counts switch
        {
            { Count: 1 } => 7,
            _ =>
                counts.Values.OrderByDescending(x => x).ToList() switch
                {
                [4, ..] => 6,
                [3, 2, ..] => 5,
                [3, ..] => 4,
                [2, 2, 1] => 3,
                [2, ..] => 2,
                    _ => 1
                }
        };


        // cr_mn: ewentualnie aby nie pamietac liczenia tych indeksow w kodzie mozna odleciec tak
        List<Func<Dictionary<char, int>, bool>> conditions =
        [
            counts => counts.Count == 1,
            counts => counts.ContainsValue(4),
            counts => counts.ContainsValue(2) && counts.ContainsValue(3),
            counts => counts.ContainsValue(3),
            counts => counts.Values.OrderBy(x => x).SequenceEqual([1, 2, 2]),
            counts => counts.ContainsValue(2),
            counts => true,
        ];

        var result3 = 7 - conditions.FindIndex(f => f(counts));

        return result2;

        // // Five of a kind
        // if (counts.Count == 1)
        //     return 7;
        // // Four of a kind
        // if (counts.ContainsValue(4))
        //     return 6;
        // // Full house
        // if (counts.ContainsValue(2) && counts.ContainsValue(3))
        //     return 5;
        // // Three of a kind
        // if (counts.ContainsValue(3))
        //     return 4;
        // // Two pair
        // if (counts.Values.OrderBy(x => x).SequenceEqual(new List<int> { 1, 2, 2 }))
        //     return 3;
        // // One pair
        // if (counts.ContainsValue(2))
        //     return 2;
        // // High card
        // return 1;
    }

    public void SolveTaskTwo()
    {
        var cardsPower = new List<String> { "J", "2", "3", "4", "5", "6", "7", "8", "9", "T", "Q", "K", "A" };
        var input = ReadInput();
        int iterator = 0;
        var result = input.Select(i =>
            {
                var splittedString = i.Split(" ").ToList();
                return new Card(splittedString[0], Int32.Parse(splittedString[1]), CheckHandType(splittedString[0], true));
            })
            .GroupBy(c => c.Power)
            .OrderBy(g => g.Key)
            .SelectMany(c => c.OrderBy(card => card.Hand, new CardComparer(cardsPower)))
            .Aggregate(0, (acc, card) =>
            {
                iterator++;
                return acc + card.Bid * iterator;
            });

        Console.WriteLine(result);
    }

    private List<string> ReadInput()
    {
        return GetInputAsStringList("input_7_2k23.txt");
    }
}