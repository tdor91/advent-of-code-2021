using System.Text.RegularExpressions;

public class DisplayNote
{
    private static Regex regex = new Regex("[a-z]+");

    public DisplayNote(string input)
    {
        var matches = regex.Matches(input).ToArray();
        Patterns = matches[0..10].Select(match => match.Value).ToList();
        Outputs = matches[10..14].Select(match => match.Value).ToList();
    }

    public List<string> Patterns { get; private set; }
    public List<string> Outputs { get; private set; }

    public int NumberOfUniqueOutputValues()
    {
        var uniqueOutputs = Outputs.Where(
            x => x.Length == 2 ||        // '1' => 2 segments
                 x.Length == 3 ||        // '7' => 3 segments
                 x.Length == 4 ||        // '4' => 4 segments
                 x.Length == 7);         // '8' => 7 segments

        return uniqueOutputs.Count();
    }

    public int GetOutput()
    {
        var map = CalculateDigitMap();

        string resultString = "";

        foreach (var output in Outputs)
        {
            var digit = map.First(kvp => kvp.Key.SetEquals(output)).Value;
            resultString += digit;
        }

        return int.Parse(resultString);
    }

    private Dictionary<HashSet<char>, char> CalculateDigitMap()
    {
        var patternsCopy = Patterns.ToList();

        var one = patternsCopy.ExtractSingle(pattern => pattern.Length == 2).ToHashSet();
        var four = patternsCopy.ExtractSingle(pattern => pattern.Length == 4).ToHashSet();
        var seven = patternsCopy.ExtractSingle(pattern => pattern.Length == 3).ToHashSet();
        var eight = patternsCopy.ExtractSingle(pattern => pattern.Length == 7).ToHashSet();

        var nine = patternsCopy.ExtractFirst(pattern => pattern.Length == 6 && four.IsSubsetOf(pattern)).ToHashSet();
        var zero = patternsCopy.ExtractFirst(pattern => pattern.Length == 6 && one.IsSubsetOf(pattern)).ToHashSet();
        var six = patternsCopy.ExtractSingle(pattern => pattern.Length == 6).ToHashSet();

        var three = patternsCopy.ExtractFirst(pattern => pattern.Length == 5 && seven.IsSubsetOf(pattern)).ToHashSet();
        var two = patternsCopy.ExtractFirst(pattern => pattern.Length == 5 && pattern.Contains(eight.Except(nine).Single())).ToHashSet();
        var five = patternsCopy.ExtractSingle(pattern => pattern.Length == 5).ToHashSet();

        if (patternsCopy.Any())
        {
            throw new Exception("Invalid patterns.");
        }

        return new Dictionary<HashSet<char>, char>
        {
            { zero,  '0' }, { one,   '1' },
            { two,   '2' }, { three, '3' },
            { four,  '4' }, { five,  '5' },
            { six,   '6' }, { seven, '7' },
            { eight, '8' }, { nine,  '9' }
        };
    }
}
