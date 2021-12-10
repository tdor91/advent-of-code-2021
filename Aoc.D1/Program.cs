Console.WriteLine("Aoc D1");

var lines = await File.ReadAllLinesAsync("input.txt");

var numbers = lines.Select(int.Parse).ToArray();

int numberOfIncrements = 0;
numbers.Aggregate((a, b) =>
{
    if (b > a) numberOfIncrements++;
    return b;
});

Console.WriteLine($"Part 1: Number of increments is {numberOfIncrements}.");

int numberOfShiftingIncrements = 0;
for (int i = 0; i < numbers.Count() - 3; i++)
{
    var sum1 = numbers[i..(i + 3)].Sum();
    var sum2 = numbers[(i + 1)..(i + 4)].Sum();

    if (sum2 > sum1) numberOfShiftingIncrements++;
}

Console.WriteLine($"Part 2: Number of shifting increments is {numberOfShiftingIncrements}.");
