using CityTravelProblem;

var input = "0: 1, 1: 2 3 4, 4: 5";

var graph = """
                0
              / |
             /  1
             |/ | \
             2  3  4
                   |
                   5
            """;

Console.WriteLine(input + "\n");
Console.WriteLine(graph + "\n");

var places = input
    .Replace(':', ' ')
    .Replace(',', ' ')
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Distinct()
    .ToList();

Console.WriteLine("Places: " + string.Join(",", places));
Console.WriteLine();
Console.WriteLine("City Matrix:");

var edges = input
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(part => part.Trim())
    .Select(part =>
    {
        var split = part.Split(':', 2); // split into key : values
        var key = int.Parse(split[0].Trim());
        var values = split[1]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
        return (key, values);
    })
    .ToDictionary(x => x.key, x => x.values);

var city = new CityGraph(places.Count);

foreach (var edge in edges)
{
    foreach (var val in edge.Value)
    {
        city.AddEdge(edge.Key, val);
    }
}

city.DisplayCity();

Console.WriteLine("\nEverybody now goes to place 1");
var target = 1;
var edgeLoads = city.EdgeLoads(target);

Console.WriteLine($"Edge utilisation if everyone goes to {target}:");
foreach (var kvp in edgeLoads.OrderBy(k => k.Key))
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value} people");
}