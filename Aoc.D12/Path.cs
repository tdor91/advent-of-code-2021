class Path
{
    public Path(IEnumerable<Node> nodes)
    {
        Nodes = nodes.ToList();
    }

    public List<Node> Nodes { get; set; }

    public override string ToString()
    {
        return string.Join(",", Nodes.Select(n => n.Name));
    }
}
