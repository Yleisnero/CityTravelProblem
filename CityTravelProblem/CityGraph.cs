namespace CityTravelProblem;

public class CityGraph
{
    private readonly int[,] _cityMatrix;
    private readonly int _numOfCities;
    private readonly List<int>[] _adj;

    public CityGraph(int numOfCities)
    {
        _cityMatrix = new int[numOfCities, numOfCities];
        _numOfCities = numOfCities;
        _adj = Enumerable.Range(0, _numOfCities)
            .Select(_ => new List<int>())
            .ToArray();
    }

    public void AddEdge(int i, int j)
    {
        _cityMatrix[i, j] = 1;
        _cityMatrix[j, i] = 1;

        _adj[i].Add(j);
        _adj[j].Add(i);
    }

    public void DisplayCity()
    {
        for (var i = 0; i < _numOfCities; i++)
        {
            for (var j = 0; j < _numOfCities; j++)
            {
                Console.Write(_cityMatrix[i, j] + " ");
            }

            Console.WriteLine();
        }
    }

    public int[] ShortestPath(int start, int end)
    {
        var visited = new bool[_numOfCities];
        var prev = new int[_numOfCities];
        for (var k = 0; k < _numOfCities; k++) prev[k] = -1;

        var queue = new Queue<int>();
        queue.Enqueue(start);
        visited[start] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == end) break;
            for (var neighbor = 0; neighbor < _numOfCities; neighbor++)
            {
                if (_cityMatrix[current, neighbor] != 0 && !visited[neighbor])
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                    prev[neighbor] = current;
                }
            }
        }

        // Rebuild path from prev array
        var path = new List<int>();
        for (var at = end; at != -1; at = prev[at])
            path.Add(at);
        path.Reverse();

        // If start of path is not i, no path exists
        return path[0] != start ? [] : path.ToArray();
    }

    public int[] GetNeighbors(int place)
    {
        var neighbors = new List<int>();
        for (var i = 0; i < _numOfCities; i++)
        {
            if (i != place) continue;
            for (var j = 0; j < _numOfCities; j++)
            {
                if (_cityMatrix[i, j] == 1)
                {
                    neighbors.Add(j);
                }
            }
        }

        return neighbors.ToArray();
    }

    public Dictionary<(int, int), long> EdgeLoads(int destination)
    {
        var loads = new Dictionary<(int, int), long>();
        var seen = new bool[_numOfCities];

        // Depth-First Search (DFS)
        long Dfs(int v, int parent)
        {
            seen[v] = true;
            long sum = v; // population = label value

            foreach (var nxt in _adj[v])
            {
                if (nxt == parent) continue;
                if (seen[nxt]) continue;
                var childSum = Dfs(nxt, v); // Vertex is parent now
                var key = (Math.Min(v, nxt), Math.Max(v, nxt));
                // Edge load is the sum of the subtree
                loads[key] = childSum;
                sum += childSum;
            }

            return sum;
        }

        _ = Dfs(destination, -1);
        return loads;
    }
}