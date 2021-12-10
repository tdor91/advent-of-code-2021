Console.WriteLine("Aoc D10!");

var braceMap = new Dictionary<char, char>
{
    { '(', ')' },
    { '[', ']' },
    { '{', '}' },
    { '<', '>' },
};

var errorScoreMap = new Dictionary<char, int>
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 },
};

var completionScoreMap = new Dictionary<char, int>
{
    { ')', 1 },
    { ']', 2 },
    { '}', 3 },
    { '>', 4 },
};

var inputLines = await File.ReadAllLinesAsync("input.txt");

int syntaxErrorScore = 0;
foreach (var line in inputLines)
{
    if (IsCorrupted(line, out var invalidChar))
    {
        syntaxErrorScore += errorScoreMap[invalidChar];
    }
}
Console.WriteLine($"Part 1: Total syntax error score is {syntaxErrorScore}.");

var completionScores = new List<long>();
foreach (var line in inputLines.Where(line => !IsCorrupted(line, out var _)))
{
    var completionString = GetCompletion(line);
    var completionScore = completionString.Aggregate(0L, (a, b) => a * 5 + completionScoreMap[b]);
    completionScores.Add(completionScore);
}
var middleScore = completionScores
    .OrderBy(x => x)
    .Skip(completionScores.Count / 2)
    .First();
Console.WriteLine($"Part 2: Middle completion score is {middleScore}.");


bool IsCorrupted(string line, out char invalidChar)
{
    var stack = new Stack<char>();

    foreach (var c in line)
    {
        if (braceMap.Keys.Contains(c))
        {
            stack.Push(c);
        }

        if (braceMap.Values.Contains(c))
        {
            var opening = stack.Pop();
            if (braceMap[opening] != c)
            {
                invalidChar = c;
                return true;
            }
        }
    }

    invalidChar = ' ';
    return false;
}

string GetCompletion(string line)
{
    var stack = new Stack<char>();

    foreach (var c in line)
    {
        if (braceMap.Keys.Contains(c)) stack.Push(c);
        else if (braceMap.Values.Contains(c)) stack.Pop();
    }

    return stack.Select(c => braceMap[c]).Aggregate("", (a, b) => a + b);
}