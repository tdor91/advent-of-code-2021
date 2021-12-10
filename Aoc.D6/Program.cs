Console.WriteLine("Aoc D6");

var inputLines = await File.ReadAllLinesAsync("input.txt");
var fishes = inputLines.First().Split(",").Select(int.Parse).ToList();

var fishPop = GetNewFishPop();
foreach (var fish in fishes)
{
    fishPop[fish]++;
}

for (int day = 1; day <= 256; day++)
{
    var nextPop = GetNewFishPop();
    foreach (var kvp in fishPop.OrderByDescending(kvp => kvp.Key))
    {
        if (kvp.Key == 0)
        {
            nextPop[6] += kvp.Value;
            nextPop[8] = kvp.Value;
        }
        else if (kvp.Key > 0)
        {
            nextPop[kvp.Key - 1] += kvp.Value;
        }
    }
    fishPop = nextPop;

    if (day == 80) Console.WriteLine($"Part 1: Fish population after 80 days: {fishPop.Values.Sum()}");
    if (day == 256) Console.WriteLine($"Part 2: Fish population after 256 days: {fishPop.Values.Sum()}");
}

Dictionary<int, long> GetNewFishPop()
{
    var fishDict = new Dictionary<int, long>();
    for (int i = 0; i <= 8; i++)
    {
        fishDict.Add(i, 0);
    }

    return fishDict;
}