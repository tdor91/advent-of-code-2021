class Node
{
    public Node(string name)
    {
        Name = name;
        ConnectedNodes = new();
    }

    public string Name { get; set; }
    public HashSet<Node> ConnectedNodes { get; set; }

    public bool IsSmall => Name == Name.ToLower();

    public static bool operator ==(Node a, Node b) => a.Equals(b);
    public static bool operator !=(Node a, Node b) => !a.Equals(b);
    public override bool Equals(object? obj) => obj is Node other && Name == other.Name;
    public override string ToString() => $"{Name} - {ConnectedNodes.Count}";
    public override int GetHashCode() => Name.GetHashCode();
}