Console.WriteLine("Aoc D5");

var inputLines = await File.ReadAllLinesAsync("input.txt");

var lines = inputLines.Select(il => new Line(il)).ToList();
var max = lines.Select(l => l.Max()).Max() + 1;

var result = new Board(max);

var orthogonalLines = lines.Where(l => l.IsOrthogonal).ToList();
foreach (var line in orthogonalLines)
{
    result.Add(line);
}
Console.WriteLine("Part 1: overlapping orthogonal fields: " + result.Count(x => x >= 2));

var diagonalLines = lines.Where(l => l.IsDiagonal).ToList();
foreach (var line in diagonalLines)
{
    result.Add(line);
}
Console.WriteLine("Part 2: overlapping orthogonal+diagonal fields: " + result.Count(x => x >= 2));
