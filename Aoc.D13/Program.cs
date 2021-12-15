using System.Text.RegularExpressions;

Console.WriteLine("Aoc D13");

var lines = await File.ReadAllLinesAsync("input.txt");
var points = lines
    .Where(line => !line.StartsWith("fold") && !string.IsNullOrWhiteSpace(line))
    .Select(line =>
    {
        var x = int.Parse(line.Split(",")[0].ToString());
        var y = int.Parse(line.Split(",")[1].ToString());
        return (x, y);
    }).ToList();

var instructions = lines
    .Where(line => line.StartsWith("fold"))
    .Select(line =>
    {
        var match = Regex.Match(line, @"fold along (x|y)=(\d+)");
        return (Axis: match.Groups[1].Value, Position: int.Parse(match.Groups[2].Value));
    }).ToList();

var part1Result = Fold(points, instructions[0]).Distinct().Count();
Console.WriteLine($"Part 1: Visible marks after first folding operation: {part1Result}");

IEnumerable<(int X, int Y)> part2Result = points;
foreach (var instruction in instructions)
{
    part2Result = Fold(part2Result, instruction).Distinct();
}
Console.WriteLine("Part 2:");
Print(part2Result);

IEnumerable<(int X, int Y)> Fold(IEnumerable<(int X, int Y)> points, (string Axis, int Position) instruction)
{
    foreach (var point in points)
    {
        if (instruction.Axis == "y")
        {
            if (point.Y < instruction.Position)
            {
                yield return point;
            }

            if (point.Y > instruction.Position)
            {
                var y = instruction.Position - (point.Y - instruction.Position);
                yield return  (point.X, y);
            }
        }

        if (instruction.Axis == "x")
        {
            if (point.X < instruction.Position)
            {
                yield return point;
            }
            if (point.X > instruction.Position)
            {
                var x = instruction.Position - (point.X - instruction.Position);
                yield return (x, point.Y);
            }
        }
    }
}

void Print(IEnumerable<(int X, int Y)> points)
{
    var maxX = points.Max(c => c.X) + 1;
    var maxY = points.Max(c => c.Y) + 1;

    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
        {
            Console.Write(points.Contains((x, y)) ? "#" : ".");
        }
        Console.WriteLine();
    }
}
