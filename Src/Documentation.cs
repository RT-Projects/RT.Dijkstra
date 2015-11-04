
namespace RT.Dijkstra
{
    /// <summary>
    ///     <para>
    ///         This library allows you to find the shortest path through any kind of graph in which the edges have weights.</para>
    ///     <heading>
    ///         Usage example</heading>
    ///     <para>
    ///         Suppose you have a network of roads that connect cities:</para>
    ///     <code monospace="true">
    ///          From         To            Distance
    ///         ═════════════════════════════════════
    ///          New York     Chicago            789
    ///          New York     Philadelphia        97
    ///          Los Angeles  Phoenix            372
    ///          Chicago      Houston           1083
    ///          Chicago      Philadelphia       759
    ///          Houston      Phoenix           1176</code>
    ///     <para>
    ///         Suppose also that this information is stored in a dictionary such as the following:</para>
    ///     <code>
    ///         public static class CityInfo
    ///         {
    ///             public static Dictionary&lt;string, Dictionary&lt;string, int&gt;&gt; Distances;
    ///         
    ///             static CityInfo()
    ///             {
    ///                 // Populate Distances here
    ///             }
    ///         }</code>
    ///     <para>
    ///         Now Dijkstra’s Algorithm lets you find the shortest route to get from any city to any other. First, declare a
    ///         type similar to the following:</para>
    ///     <code>
    ///         public sealed class CityNode : Node&lt;int, string&gt;
    ///         {
    ///             public string City { get; private set; }
    ///             public string Destination { get; private set; }
    ///         
    ///             public CityNode(string city, string destination)
    ///             {
    ///                 City = city;
    ///                 Destination = destination;
    ///             }
    ///         
    ///             // This function must return true if the two nodes represent the same city.
    ///             public override bool Equals(Node&lt;int, string&gt; other)
    ///             {
    ///                 return City.Equals(((CityNode) other).City);
    ///             }
    ///         
    ///             public override int GetHashCode() { return City.GetHashCode(); }
    ///         
    ///             // This function determines whether we’ve arrived at our intended destination.
    ///             public override bool IsFinal { get { return City == Destination; } }
    ///         
    ///             // This function returns the direct connections from the current city.
    ///             public override IEnumerable&lt;Edge&lt;int, string&gt;&gt; Edges
    ///             {
    ///                 get
    ///                 {
    ///                     return CityInfo.Distances[City].Select(toCity =&gt; new Edge&lt;int, string&gt;(
    ///                         // The weight of this edge is the distance between the cities.
    ///                         toCity.Value,
    ///                         // The label of this edge is the travel route.
    ///                         string.Format("{0} to {1}", City, toCity.Key),
    ///                         // The other city the edge leads to.
    ///                         new CityNode(toCity.Key, Destination)));
    ///                 }
    ///             }
    ///         }</code>
    ///     <para>
    ///         And then a single call to <see cref="DijkstrasAlgorithm.Run"/> does the actual work. Let’s create a function
    ///         that returns a human-readable route:</para>
    ///     <code>
    ///         static string GetRoute(string from, string to)
    ///         {
    ///             int totalDistance;
    ///             var route = DijkstrasAlgorithm.Run(
    ///                 // The start node to begin our search.
    ///                 new CityNode(from, to),
    ///                 // The initial value for the distance traveled.
    ///                 0,
    ///                 // How to add two distances.
    ///                 (a, b) =&gt; a + b,
    ///                 // The variable to receive the total distance traveled.
    ///                 out totalDistance);
    ///         
    ///             return string.Format("{0} ({1} miles)", string.Join(", ", route), totalDistance);
    ///         }
    ///         
    ///         // This example outputs:
    ///         // New York to Chicago, Chicago to Houston, Houston to Phoenix (3048 miles)
    ///         Console.WriteLine(GetRoute("New York", "Phoenix"));
    ///         
    ///         // This example outputs:
    ///         // Los Angeles to Phoenix, Phoenix to Houston, Houston to Chicago (2631 miles)
    ///         Console.WriteLine(GetRoute("Los Angeles", "Chicago"));
    ///         
    ///         // This example outputs:
    ///         // Phoenix to Houston (1176 miles)
    ///         Console.WriteLine(GetRoute("Phoenix", "Houston"));
    ///         
    ///         // This example outputs an empty route, because we’re already at the destination:
    ///         //  (0 miles)
    ///         Console.WriteLine(GetRoute("Phoenix", "Phoenix"));</code></summary>
    sealed class NamespaceDocumentation { }
}
