Console.WriteLine("Aoc D9!");

var inputLines = await File.ReadAllLinesAsync("input.txt");
int[,] matrix = new int[inputLines.Length, inputLines[0].Length];
for (int x = 0; x < inputLines.Length; x++)
{
    for (int y = 0; y < inputLines[0].Length; y++)
    {
        matrix[x, y] = int.Parse(inputLines[x][y].ToString());
    }
}

var risk = matrix
    .Points()
    .Where(p => matrix.IsLowPoint(p))
    .Select(lp => matrix[lp.X, lp.Y] + 1)
    .Sum();
Console.WriteLine($"Part 1: Sum of risk levels is {risk}.");

var basinScore = matrix
    .Points()
    .Where(p => matrix.IsLowPoint(p))
    .Select(lp => GetBasinSize(matrix, lp))
    .OrderByDescending(s => s)
    .Take(3)
    .Aggregate((product, next) => product * next);
Console.WriteLine($"Part 2: Product of the three largest basins is {basinScore}.");


int GetBasinSize(int[,] matrix, Point lowPoint)
{
    var result = new HashSet<Point>(new[] { lowPoint });
    var stack = new Stack<Point>(new[] { lowPoint });

    while (stack.TryPop(out var point))
    {
        var basinPoints = matrix
            .Adjacents(point)
            .Where(p => matrix[p.X, p.Y] != 9);

        foreach (var basinPoint in basinPoints)
        {
            if (result.Add(basinPoint))
            {
                stack.Push(basinPoint);
            }
        }
    }

    return result.Count();
}
