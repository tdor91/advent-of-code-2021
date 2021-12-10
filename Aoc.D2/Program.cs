Console.WriteLine("Aoc D2");

var lines = await File.ReadAllLinesAsync("input.txt");
var commands = lines.Select(line => new Command(line)).ToList();

var submarine1 = new Part1Submarine();
commands.ForEach(submarine1.Execute);
Console.WriteLine($"Part 1: Result is {submarine1.HorizontalPosition * submarine1.Depth}.");

var submarine2 = new Part2Submarine();
commands.ForEach(submarine2.Execute);
Console.WriteLine($"Part 2: Result is {submarine2.HorizontalPosition * submarine2.Depth}.");
