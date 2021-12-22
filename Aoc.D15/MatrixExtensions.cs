internal static class MatrixExtensions
{
    public static IEnumerable<Point> GetNeighbors<T>(this T[,] matrix, Point p)
    {
        if (p.X > 0) yield return new Point(p.X - 1, p.Y);
        if (p.Y > 0) yield return new Point(p.X, p.Y - 1);
        if (p.X < matrix.GetLength(0) - 1) yield return new Point(p.X + 1, p.Y);
        if (p.Y < matrix.GetLength(1) - 1) yield return new Point(p.X, p.Y + 1);
    }
}
