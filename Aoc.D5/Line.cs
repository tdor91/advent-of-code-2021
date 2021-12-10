using System.Text.RegularExpressions;

internal class Line
{
    private static Regex regex = new Regex(@"(\d+),(\d+) -\> (\d+),(\d+)");
    private string original;

    public Line(string l)
    {
        original = l;
        var groups = regex.Matches(l)[0].Groups;
        X1 = int.Parse(groups[1].Value);
        Y1 = int.Parse(groups[2].Value);
        X2 = int.Parse(groups[3].Value);
        Y2 = int.Parse(groups[4].Value);
    }

    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }
    public bool IsOrthogonal => X1 == X2 || Y1 == Y2;
    public bool IsDiagonal => Math.Abs(X1 - X2) == Math.Abs(Y1 - Y2);

    public int Max() => new int[] { X1, Y1, X2, Y2 }.Max();

    public IEnumerable<(int X, int Y)> Occupies()
    {
        if (IsOrthogonal)
        {
            var x1 = Math.Min(X1, X2);
            var x2 = Math.Max(X1, X2);

            var y1 = Math.Min(Y1, Y2);
            var y2 = Math.Max(Y1, Y2);

            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    yield return (i, j);
                }
            }
        }
        else if (IsDiagonal)
        {
            Func<int, int> xOp = X1 < X2 ? a => a + 1 : a => a - 1;
            Func<int, int> yOp = Y1 < Y2 ? a => a + 1 : a => a - 1;
            int x = X1;
            int y = Y1;
            while (x != X2)
            {
                yield return (x, y);
                x = xOp(x);
                y = yOp(y);
            }
            yield return (x, y);
        }
    }

    public override string ToString()
    {
        return original;
    }
}