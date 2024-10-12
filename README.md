Full documentation: http://docs.timwi.de/RT.Dijkstra

This class allows you to find the shortest path through any kind of graph in which the edges have weights.

[![build](https://github.com/RT-Projects/RT.Dijkstra/actions/workflows/build.yml/badge.svg)](https://github.com/RT-Projects/RT.Dijkstra/actions/workflows/build.yml)

Usage example
=

Suppose you have a network of roads that connect cities:

     From         To            Distance
    ═════════════════════════════════════
     New York     Chicago            789
     New York     Philadelphia        97
     Los Angeles  Phoenix            372
     Chicago      Houston           1083
     Chicago      Philadelphia       759
     Houston      Phoenix           1176

Suppose also that this information is stored in a dictionary such as the following:

    public static class CityInfo
    {
        public static Dictionary<string, Dictionary<string, int>> Distances;

        static CityInfo()
        {
            // Populate Distances here
        }
    }

Now Dijkstra’s Algorithm lets you find the shortest route to get from any city to any other. First, declare a type similar to the following:

    public sealed class CityNode : Node<int, string>
    {
        public string City { get; private set; }
        public string Destination { get; private set; }

        public CityNode(string city, string destination)
        {
            City = city;
            Destination = destination;
        }

        // This function must return true if the two nodes represent the same city.
        public override bool Equals(Node<int, string> other)
        {
            return City.Equals(((CityNode) other).City);
        }

        public override int GetHashCode() { return City.GetHashCode(); }

        // This function determines whether we’ve arrived at our intended destination.
        public override bool IsFinal { get { return City == Destination; } }

        // This function returns the direct connections from the current city.
        public override IEnumerable<Edge<int, string>> Edges
        {
            get
            {
                return CityInfo.Distances[City].Select(toCity => new Edge<int, string>(
                    // The weight of this edge is the distance between the cities.
                    toCity.Value,
                    // The label of this edge is the travel route.
                    string.Format("{0} to {1}", City, toCity.Key),
                    // The other city the edge leads to.
                    new CityNode(toCity.Key, Destination)));
            }
        }
    }

And then a single call to `DijkstrasAlgorithm.Run` does the actual work. Let’s create a function that returns a human-readable route:

    static string GetRoute(string from, string to)
    {
        int totalDistance;
        var route = DijkstrasAlgorithm.Run(
			// The start node to begin our search.
			new CityNode(from, to),
			// The initial value for the distance traveled.
			0,
			// How to add two distances.
			(a, b) => a + b,
			// The variable to receive the total distance traveled.
			out totalDistance);

        return string.Format("{0} ({1} miles)", string.Join(", ", route), totalDistance);
    }

    // This example outputs:
    // New York to Chicago, Chicago to Houston, Houston to Phoenix (3048 miles)
    Console.WriteLine(GetRoute("New York", "Phoenix"));

    // This example outputs:
    // Los Angeles to Phoenix, Phoenix to Houston, Houston to Chicago (2631 miles)
    Console.WriteLine(GetRoute("Los Angeles", "Chicago"));

    // This example outputs:
    // Phoenix to Houston (1176 miles)
    Console.WriteLine(GetRoute("Phoenix", "Houston"));

    // This example outputs an empty route, because we’re already at the destination:
    //  (0 miles)
    Console.WriteLine(GetRoute("Phoenix", "Phoenix"));
