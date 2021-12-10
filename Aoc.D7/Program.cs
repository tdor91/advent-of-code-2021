using Aoc.D7;

Console.WriteLine("Aoc D7");
var inputLines = await File.ReadAllLinesAsync("input.txt");

var puzzle = new D7PuzzleSolver();
var part1Result = puzzle.SolvePart1(inputLines[0]);
Console.WriteLine($"Part 1: Minimum fuel to best position is {part1Result}.");

var part2Result = new D7PuzzleSolver().SolvePart2(inputLines[0]);
Console.WriteLine($"Part 2: Minimum fuel to best position is {part2Result}.");