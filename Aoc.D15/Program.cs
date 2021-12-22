Console.WriteLine("Aoc D15");

var lines = await File.ReadAllLinesAsync("input.txt");

int width = lines[0].Length;
int height = lines.Length;
int[,] matrix = new int[width, height];
for (int x = 0; x < width; x++)
{
    for (int y = 0; y < height; y++)
    {
        matrix[x, y] = Convert.ToInt32(lines[x][y].ToString());
    }
}

var destination = new Point(width - 1, height - 1);
Console.WriteLine($"Part 1: Shortest path is {FindShortestPath(matrix, destination)}");

const int dimensions = 5;
int[,] fullMatrix = new int[width * dimensions, height * dimensions];

for (int xDim = 0; xDim < dimensions; xDim++)
{
    for (int yDim = 0; yDim < dimensions; yDim++)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var newValue = (matrix[x, y] + xDim + yDim - 1) % 9 + 1;
                fullMatrix[width * xDim + x, height * yDim + y] = newValue;
            }
        }
    }
}

destination = new Point(fullMatrix.GetLength(0) - 1, fullMatrix.GetLength(1) - 1);
Console.WriteLine($"Part 2: Shortest path is {FindShortestPath(fullMatrix, destination)}");

// Dijkstra's Algorithm
int FindShortestPath(int[,] matrix, Point destination)
{
    var origin = new Point(0, 0);
    PriorityQueue<Point, int> queue = new();
    queue.Enqueue(origin, 0);

    Dictionary<Point, Path> shortestPaths = new();
    shortestPaths[origin] = new Path(new Point[0], 0);

    while (queue.Count > 0)
    {
        var point = queue.Dequeue();
        var neighbors = matrix.GetNeighbors(point);

        foreach (var neighbor in neighbors)
        {
            var cost = shortestPaths[point].Cost + matrix[neighbor.X, neighbor.Y];
            if (neighbor == destination)
            {
                return cost;
            }

            if (!shortestPaths.ContainsKey(neighbor) || cost < shortestPaths[neighbor].Cost)
            {
                shortestPaths[neighbor] = new Path(shortestPaths[point].Positions.Append(neighbor), cost);
                queue.Enqueue(neighbor, cost);
            }
        }
    }

    return -1;
}
