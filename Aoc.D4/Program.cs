Console.WriteLine("Aoc D4");

List<Board> boards = new List<Board>();

var lines = await File.ReadAllLinesAsync("input-boards.txt");
var chunks = lines.Chunk(6);

foreach (var boardLines in chunks)
{
    var numbers = boardLines
        .SelectMany(line => line
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse))
        .ToList();

    var board = new Board()
    {
        Numbers = numbers
    };

    boards.Add(board);
}

var drawnNumbersLines = await File.ReadAllLinesAsync("input-numbers.txt");
var drawnNumbers = drawnNumbersLines.First().Split(",").Select(int.Parse).ToList();

bool winnerFound = false;
foreach (var number in drawnNumbers)
{
    foreach (var board in boards)
    {
        board.Mark(number);
        if (board.HasWon())
        {
            Console.WriteLine("");
            var score = board.SumUnmarked() * number;
            Console.WriteLine("Part 1: we have a winner! The score is: " + score);
            winnerFound = true;
            break;
        }
    }

    if (winnerFound) break;
}

var remainingBoards = boards.Select(board => new Board { Numbers = board.Numbers }).ToList();
Board? lastBoard = null;
foreach (var number in drawnNumbers)
{
    foreach (var board in remainingBoards)
    {
        board.Mark(number);
    }

    remainingBoards = remainingBoards.Where(board => !board.HasWon()).ToList();
    if (remainingBoards.Count() == 1)
    {
        lastBoard = remainingBoards.Single();
    }

    if (remainingBoards.Count() == 0)
    {
        var score = lastBoard.SumUnmarked() * number;
        Console.WriteLine("Part 2: we have a loser! The score is: " + score);
        break;
    }
}
