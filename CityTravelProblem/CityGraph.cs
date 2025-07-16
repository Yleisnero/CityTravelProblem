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