This class allows you to find the shortest path through any kind of graph in which the edges have weights.

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

			public CityNode(string city, string destination) { City = city; Destination = destination; }

			// This function must return true if the two nodes represent the same city.
			public override bool Equals(Node<int, string> other) { return City.Equals(((CityNode) other).City); }
			public override int GetHashCode() { return City.GetHashCode(); }

			// This function determines whether we’ve arrived at our intended destination.
			public override bool IsFinal { get { return City == Destination; } }

			// This function returns the direct connections from the current cities to any adjacent cities.
			public override IEnumerable<Edge<int, string>> Edges {
				get {
					return CityInfo.Distances[City].Select(toCity => new Edge<int, string>(toCity.Value, string.Format("{0} to {1}", City, toCity.Key), new CityNode(toCity.Key, Destination)));
				}
			}
		}

And then a single call to `DijkstrasAlgorithm.Run` does the actual work:

		// This example outputs: New York to Chicago, Chicago to Houston, Houston to Phoenix (3048 miles)
        int totalDistance;
        var route = DijkstrasAlgorithm.Run(new CityNode("New York", "Phoenix"), 0, (a, b) => a + b, out totalDistance);
        Console.WriteLine(string.Format("{0} ({1} miles)", string.Join(", ", route), totalDistance));

		// This example outputs: Los Angeles to Phoenix, Phoenix to Houston, Houston to Chicago (2631 miles)
        int totalDistance;
        var route = DijkstrasAlgorithm.Run(new CityNode("Los Angeles", "Chicago"), 0, (a, b) => a + b, out totalDistance);
        Console.WriteLine(string.Format("{0} ({1} miles)", string.Join(", ", route), totalDistance));

		// This example outputs: Phoenix to Houston (1176 miles)
        int totalDistance;
        var route = DijkstrasAlgorithm.Run(new CityNode("Phoenix", "Houston"), 0, (a, b) => a + b, out totalDistance);
        Console.WriteLine(string.Format("{0} ({1} miles)", string.Join(", ", route), totalDistance));

		// This example outputs: (0 miles)
		// (an empty route, because we’re already at the destination to begin with)
        int totalDistance;
        var route = DijkstrasAlgorithm.Run(new CityNode("Phoenix", "Phoenix"), 0, (a, b) => a + b, out totalDistance);
        Console.WriteLine(string.Format("{0} ({1} miles)", string.Join(", ", route), totalDistance));
