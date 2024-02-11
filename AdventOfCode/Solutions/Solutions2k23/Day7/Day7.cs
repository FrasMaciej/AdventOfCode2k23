namespace AdventOfCode.Solutions.Solutions2k23.Day7;
using System.Text.RegularExpressions;

public class Day7 : Utilities
{
    private class Card
    {
        public string Hand;
        public int Bid;
        public int Power;

        public Card(string hand, int bid, int power)
        {
            Hand = hand;
            Bid = bid;
            Power = power;
        }
    }
    public void SolveTaskOne()
    {
        var cardsPower = new List<String> {"2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A"};
        var input = ReadInput();
        int iterator = 0;
        var result = input.Select(i =>
            {
                var splittedString = i.Split(" ").ToList();
                return new Card(splittedString[0], Int32.Parse(splittedString[1]), CheckHandType(splittedString[0], false));
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
    
    class CardComparer : IComparer<string>
    {
        private readonly List<string> order;

        public CardComparer(List<string> order)
        {
            this.order = order;
        }

        public int Compare(string x, string y)
        {
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
        var counts = hand.GroupBy(x => x)
            .ToDictionary(group => group.Key, group => group.Count());
        
        if (useJokers && counts.Count != 1 && counts.ContainsKey('J'))
        {
            var maxKey = counts.Where(pair => pair.Key != 'J')
                .Aggregate((x, y) => x.Value > y.Value ? x : y)
                .Key;            counts[maxKey] += counts['J'];
            counts.Remove('J');
        }
        
        // Five of a kind
        if (counts.Count == 1)
            return 7;
        // Four of a kind
        if (counts.ContainsValue(4))
            return 6;
        // Full house
        if (counts.ContainsValue(2) && counts.ContainsValue(3))
            return 5;
        // Three of a kind
        if (counts.ContainsValue(3))
            return 4;
        // Two pair
        if (counts.Values.OrderBy(x => x).SequenceEqual(new List<int> { 1, 2, 2 }))
            return 3;
        // One pair
        if (counts.ContainsValue(2))
            return 2;
        // High card
        return 1;
    }
    
    public void SolveTaskTwo()
    {
        var cardsPower = new List<String> {"J", "2", "3", "4", "5", "6", "7", "8", "9", "T", "Q", "K", "A"};
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