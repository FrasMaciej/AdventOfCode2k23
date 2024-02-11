namespace AdventOfCode.Solutions.Solutions2k23.Day7;
using System.Text.RegularExpressions;

public class Day7 : Utilities
{
    public void SolveTaskOne()
    {
        var input = ReadInput();
        var parsedInput = input.Select(i => i.Split(" ").ToList()).ToList();
        // parsedInput.Select(p => [...p, ""]);
        // foreach (var strings in parsedInput)
        // {
        // }


        // Punkt 2 - sortowanie
        // Punkt 3 - ułożenie według mocy kolejnych kart i przypisanie odpowiedniej wagi 
        // 1. calcPowerOfSet
        // 2. orderByPower
        // 3. ifNecessaryOrderByRuleOfSingleCardsPower
        //parsedInput[0].
        var cardsPower = new char[] {'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2', '1'};
        //Power: A > K > Q > J > T > 9 > 8 > 7 > 6 > 5 > 4 > 3 > 2 > 1 


        // 1. Dla forEach.parsedInput - dokleić dodatkowe, trecie pole z wagą (mapowanie)
        // 2. Sort według trzeciego pola z wagą 
        // 3.  
    }
    
    private int CheckHandType(string hand)
    {
        string parsedHand = String.Concat(hand.OrderBy(c => c));
        return hand switch
        {
            var str when Regex.IsMatch(parsedHand, @"(.)\1{4}") => 7, // 5 identycznych znaków
            var str when Regex.IsMatch(parsedHand, @"(.)\1{3}") => 6, // 4 identyczne znaki
            var str when Regex.IsMatch(parsedHand, @"(\w)\1.*(?:\1{2}|(\w)\2{2})") => 5, // 3 + 2 identyczne znaki
            var str when Regex.IsMatch(parsedHand, @"(.)\1{2}") => 4, // 3 identyczne znaki
            var str when Regex.IsMatch(parsedHand, @"(?:([a-zA-Z0-9])\1.*){2}") => 3, // 2 + 2 identyczne znaki
            var str when Regex.IsMatch(parsedHand, @"(.)\1") => 2, // 2 identyczne znaki
            var str when Regex.IsMatch(parsedHand, @"^(?!.*(.).*\1).*$") => 1, // 5 różnych znaków
            _ => 0
        };
    }
    
    public void SolveTaskTwo()
    {
        var input = ReadInput();
    }
    
    private List<string> ReadInput()
    {
        return GetInputAsStringList("input_7_2k23.txt");
    }
}