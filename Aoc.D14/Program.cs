using System.Text;

Console.WriteLine("Aoc D14");

var lines = await File.ReadAllLinesAsync("input.txt");

var polymer = lines[0];
var replacementMap = lines
    .Skip(2)
    .Select(line => (line.Split(" -> ")[0], line.Split(" -> ")[1]))
    .ToDictionary(x => x.Item1, x => x.Item2);

string result = polymer;
for (int i = 0; i < 10; i++)
{
    Console.WriteLine("step " + (i + 1));
    result = PerformStepPart1(result, replacementMap);
}

var groups = result.GroupBy(c => c);
var result1 = groups.Max(g => g.Count()) - groups.Min(g => g.Count());

Console.WriteLine($"Part 1: Result is {result1}.");

var result2 = SolvePart2(polymer, replacementMap);
Console.WriteLine($"Part 2: Result is {result2}.");

string PerformStepPart1(string s, Dictionary<string, string> replacementMap)
{
    StringBuilder result = new StringBuilder(); ;
    for (int i = 0; i < s.Length - 1; i++)
    {
        var pair = s[i].ToString() + s[i + 1];
        result.Append(s[i]);
        result.Append(replacementMap[pair]);
    }

    result.Append(s.Last());
    return result.ToString();
}

long SolvePart2(string polymer, Dictionary<string, string> replacementMap)
{
    var pairs = new Dictionary<string, long>();
    var characterCounts = new Dictionary<string, long>();

    for (int i = 0; i < polymer.Length - 1; i++)
    {
        Increment(pairs, polymer.Substring(i, 2));
    }

    for (int i = 0; i < polymer.Length; i++)
    {
        Increment(characterCounts, polymer[i].ToString());
    }

    for (int i = 0; i < 40; i++)
    {
        var newpairs = new Dictionary<string, long>();
        foreach (var pair in pairs.Keys)
        {
            var charToInsert = replacementMap[pair];
            var pairCount = pairs[pair];
            Increment(newpairs, pair[0] + charToInsert, pairCount);
            Increment(newpairs, charToInsert + pair[1], pairCount);
            Increment(characterCounts, charToInsert, pairCount);
        }
        pairs = newpairs;
    }

    return characterCounts.MaxBy(kvp => kvp.Value).Value - characterCounts.MinBy(kvp => kvp.Value).Value;
}

void Increment(Dictionary<string, long> dict, string key, long count = 1)
{
    dict.TryAdd(key, 0);
    dict[key] += count;
}