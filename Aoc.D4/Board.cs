class Board
{
    private List<bool> marks;

    public Board()
    {
        marks = new bool[25].ToList();
    }

    public List<int> Numbers { get; set; }

    public bool Mark(int number)
    {
        var index = Numbers.FindIndex(x => x == number);
        if (index >= 0)
        {
            marks[index] = true;
            return true;
        }

        return false;
    }

    public bool HasWon()
    {
        var rows = marks.Chunk(5).ToList();
        bool anyCompleteRow = rows.Any(row => row.All(x => x));

        List<List<bool>> columns = new List<List<bool>>();
        for (int i = 0; i < 5; i++)
        {
            var column = rows.Select(row => row.Skip(i).First()).ToList();
            columns.Add(column);
        }

        bool anyCompleteColumn = columns.Any(column => column.All(x => x));

        return anyCompleteRow || anyCompleteColumn;
    }

    public int SumUnmarked()
    {
        var unmarkedIndexes = marks
            .Select((x, i) => (Marked: x, i))
            .Where(y => !y.Marked)
            .Select(y => y.i)
            .ToList();

        var unmarkedSum = Numbers
            .Where((n, i) => unmarkedIndexes.Contains(i))
            .Sum();

        return unmarkedSum;
    }
}