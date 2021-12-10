Console.WriteLine("Aoc D8");

var inputLines = await File.ReadAllLinesAsync("input.txt");

List<DisplayNote> notes = inputLines
    .Select(line => new DisplayNote(line))
    .ToList();

var part1Result = notes.Select(note => note.NumberOfUniqueOutputValues()).Sum();
Console.WriteLine($"Part 1: Result is {part1Result}.");

var part2Result = notes.Select(note => note.GetOutput()).Sum();
Console.WriteLine($"Part 2: Result is {part2Result}.");
