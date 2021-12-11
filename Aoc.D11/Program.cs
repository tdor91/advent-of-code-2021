Console.WriteLine("Aoc D11!");

var inputLines = await File.ReadAllLinesAsync("input.txt");
int[,] matrix = new int[inputLines.Length, inputLines[0].Length];
for (int x = 0; x < inputLines.Length; x++)
{
    for (int y = 0; y < inputLines[0].Length; y++)
    {
        matrix[x, y] = int.Parse(inputLines[x][y].ToString());
    }
}

var flashes = 0;
for (int step = 1; ; step++)
{
    matrix.Increment(matrix.Points());
    var initialFlashes = matrix.Points().Where(matrix.IsFlashing);
    Stack<Point> pointsToProcess = new(initialFlashes);
    HashSet<Point> alreadyFlashedPoints = new(initialFlashes);

    while (pointsToProcess.Count > 0)
    {
        var point = pointsToProcess.Pop();
        var adjacents = matrix.Adjacents(point);
        matrix.Increment(adjacents);

        foreach (var adjacent in adjacents)
        {
            if (matrix.IsFlashing(adjacent))
            {
                if (alreadyFlashedPoints.Add(adjacent))
                {
                    pointsToProcess.Push(adjacent);
                }
            }
        }
    }

    flashes += matrix.Points().Where(matrix.IsFlashing).Count();
    var resets = matrix.ResetFlashed();

    if (step == 100) Console.WriteLine($"Part 1: Total flashes after 100 steps: {flashes}");
    if (resets == matrix.Length)
    {
        Console.WriteLine($"Part 2: First simultanious flashes at step {step}");
        break;
    }
}
