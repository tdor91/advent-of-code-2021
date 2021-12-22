internal class Path
{
    public Path(IEnumerable<Point> positions, int totalCost)
    {
        Positions = positions.ToList();
        Cost = totalCost;
    }

    public List<Point> Positions { get; }
    public int Cost { get; }
}