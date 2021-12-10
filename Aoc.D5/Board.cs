internal class Board
{
    private int[][] result;

    public Board(int max)
    {
        result = new int[max][];
        for (int i = 0; i < max; i++)
        {
            result[i] = new int[max];
        }
    }

    public void Add(Line line)
    {
        var positions = line.Occupies().ToList();
        foreach (var position in positions)
        {
            result[position.Y][position.X]++;
        }
    }

    public void Print()
    {
        File.Delete("output.txt");
        var lines = result.Select(row => string.Join("", row.Select(r => r > 0 ? r.ToString("D1") : ".")));
        File.AppendAllLines("output.txt", lines);
    }

    public int Count(Func<int, bool> predicate)
    {
        var count = 0;
        for (int i = 0; i < result.Length; i++)
        {
            for (int j = 0; j < result[i].Length; j++)
            {
                if (predicate(result[i][j]))
                {
                    count++;
                }
            }
        }

        return count;
    }
}
