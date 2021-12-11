using System.Text;

public static class MatrixExtensions
{
    public static IEnumerable<Point> Adjacents<T>(this T[,] matrix, Point p)
    {
        bool left = p.X > 0;
        bool top = p.Y > 0;
        bool right = p.X < matrix.GetLength(0) - 1;
        bool bottom = p.Y < matrix.GetLength(1) - 1;

        if (left) yield return new Point(p.X - 1, p.Y);
        if (top) yield return new Point(p.X, p.Y - 1);
        if (right) yield return new Point(p.X + 1, p.Y);
        if (bottom) yield return new Point(p.X, p.Y + 1);

        if (left && top) yield return new Point(p.X - 1, p.Y - 1);
        if (left && bottom) yield return new Point(p.X - 1, p.Y + 1);
        if (right && top) yield return new Point(p.X + 1, p.Y - 1);
        if (right && bottom) yield return new Point(p.X + 1, p.Y + 1);
    }

    public static bool IsFlashing(this int[,] matrix, Point point)
    {
        return matrix[point.X, point.Y] > 9;
    }


    public static IEnumerable<Point> Points<T>(this T[,] matrix)
    {
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                yield return new Point(x, y);
            }
        }
    }

    public static void Increment(this int[,] matrix, IEnumerable<Point> points)
    {
        foreach (var point in points)
        {
            matrix[point.X, point.Y]++;
        }
    }

    public static int ResetFlashed(this int[,] matrix)
    {
        var resets = 0;

        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                if (matrix[x, y] > 9)
                {
                    matrix[x, y] = 0;
                    resets++;
                }
            }
        }

        return resets;
    }

    public static string Print(this int[,] matrix)
    {
        StringBuilder sb = new();

        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                string c = matrix[x, y] <= 9 ? matrix[x, y].ToString() : "+";
                sb.Append(c);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
