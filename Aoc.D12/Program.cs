Console.WriteLine("Aoc D12");

var inputLines = await File.ReadAllLinesAsync("input.txt");
var nodes = new HashSet<Node>();
foreach (var line in inputLines)
{
    var parts = line.Split("-");
    var source = nodes.FirstOrDefault(n => n.Name == parts[0]) ?? new Node(parts[0]);
    var destination = nodes.FirstOrDefault(n => n.Name == parts[1]) ?? new Node(parts[1]);

    source.ConnectedNodes.Add(destination);
    destination.ConnectedNodes.Add(source);
    nodes.Add(source);
    nodes.Add(destination);
}

var startNode = nodes.Single(n => n.Name == "start");
var endNode = nodes.Single(n => n.Name == "end");

var paths = FindPart1Paths(startNode);
Console.WriteLine($"Part 1: Number of paths is {paths.Count}.");

paths = FindPart2Paths(startNode);
paths = paths.DistinctBy(p => p.ToString()).ToList();
Console.WriteLine($"Part 2: Number of paths is {paths.Count}.");

List<Path> FindPart1Paths(Node start)
{
    List<Path> paths = new();
    FindPart1PathsRec(start, new(), paths);
    return paths;
}

void FindPart1PathsRec(Node node, Stack<Node> currentPath, List<Path> foundPaths)
{
    currentPath.Push(node);
    if (node == endNode)
    {
        foundPaths.Add(new Path(currentPath.Reverse()));
        currentPath.Pop();
        return;
    }

    foreach (var connectedNode in node.ConnectedNodes)
    {
        if (!connectedNode.IsSmall || !currentPath.Contains(connectedNode))
        {
            FindPart1PathsRec(connectedNode, currentPath, foundPaths);
        }
    }

    currentPath.Pop();
}

List<Path> FindPart2Paths(Node start)
{
    List<Path> paths = new();
    FindPart2PathsRec(start, new(), paths);
    return paths;
}

void FindPart2PathsRec(Node node, Stack<Node> currentPath, List<Path> foundPaths)
{
    currentPath.Push(node);
    if (node == endNode)
    {
        foundPaths.Add(new Path(currentPath.Reverse()));
        currentPath.Pop();
        return;
    }

    foreach (var connectedNode in node.ConnectedNodes)
    {
        if (!connectedNode.IsSmall || !currentPath.Contains(connectedNode))
        {
            FindPart2PathsRec(connectedNode, currentPath, foundPaths);
        }
        else if (connectedNode != startNode)
        {
            bool anySmallNodeVisitedTwice = currentPath
                .Where(n => n.IsSmall)
                .GroupBy(n => n.Name)
                .Any(grp => grp.Count() > 1);

            if (!anySmallNodeVisitedTwice)
            {
                FindPart2PathsRec(connectedNode, currentPath, foundPaths);
            }
        }
    }

    currentPath.Pop();
}