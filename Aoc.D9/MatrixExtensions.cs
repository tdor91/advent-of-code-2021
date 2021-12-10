public static class MatrixExtensions
{
    public static IEnumerable<Point> Adjacents<T>(this T[,] matrix, Point p)
    {
        if (p.X > 0) yield return new Point(p.X - 1, p.Y);                       // left
        if (p.Y > 0) yield return new Point(p.X, p.Y - 1);                       // top
        if (p.X < matrix.GetLength(0) - 1) yield return new Point(p.X + 1, p.Y); // right
        if (p.Y < matrix.GetLength(1) - 1) yield return new Point(p.X, p.Y + 1); // bottom
    }

    public static bool IsLowPoint<T>(this T[,] matrix,Point p) where T : IComparable<T>
    {
        return matrix
            .Adjacents(p)
            .Select(a => matrix[a.X, a.Y])
            .All(value => matrix[p.X, p.Y].CompareTo(value) < 0);
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
}
