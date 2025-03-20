using ShortestRouteOptimizerApp.Models;
using ShortestRouteOptimizerApp.Services;
using System.Collections.Generic;
using Xunit;

namespace ShortestRouteOptimizerApp.Tests.Services
{
    public class ShortestPathServiceTests
    {
        private readonly ShortestPathService _shortestPathService;

        public ShortestPathServiceTests()
        {
            _shortestPathService = new ShortestPathService();
        }

        private List<Node> CreateTestGraph()
        {
            var a = new Node { Name = "A" };
            var b = new Node { Name = "B" };
            var c = new Node { Name = "C" };
            var d = new Node { Name = "D" };
            var e = new Node { Name = "E" };
            var f = new Node { Name = "F" };
            var g = new Node { Name = "G" };
            var h = new Node { Name = "H" };
            var i = new Node { Name = "I" };

            a.Edges.Add(new Edge { Destination = b, Distance = 4 });
            a.Edges.Add(new Edge { Destination = c, Distance = 6 });

            b.Edges.Add(new Edge { Destination = a, Distance = 4 });
            b.Edges.Add(new Edge { Destination = f, Distance = 2 });

            c.Edges.Add(new Edge { Destination = a, Distance = 6 });
            c.Edges.Add(new Edge { Destination = d, Distance = 8 });

            d.Edges.Add(new Edge { Destination = c, Distance = 8 });
            d.Edges.Add(new Edge { Destination = e, Distance = 4 });
            d.Edges.Add(new Edge { Destination = g, Distance = 1 });

            e.Edges.Add(new Edge { Destination = b, Distance = 2 });
            e.Edges.Add(new Edge { Destination = d, Distance = 4 });
            e.Edges.Add(new Edge { Destination = f, Distance = 3 });
            e.Edges.Add(new Edge { Destination = i, Distance = 8 });

            f.Edges.Add(new Edge { Destination = b, Distance = 2 });
            f.Edges.Add(new Edge { Destination = e, Distance = 3 });
            f.Edges.Add(new Edge { Destination = g, Distance = 4 });
            f.Edges.Add(new Edge { Destination = h, Distance = 6 });

            g.Edges.Add(new Edge { Destination = h, Distance = 5 });
            g.Edges.Add(new Edge { Destination = d, Distance = 1 });
            g.Edges.Add(new Edge { Destination = f, Distance = 4 });
            g.Edges.Add(new Edge { Destination = i, Distance = 5 });

            h.Edges.Add(new Edge { Destination = f, Distance = 6 });
            h.Edges.Add(new Edge { Destination = g, Distance = 5 });

            i.Edges.Add(new Edge { Destination = e, Distance = 8 });
            i.Edges.Add(new Edge { Destination = g, Distance = 5 });

            return new List<Node> { a, b, c, d, e, f, g, h, i };
        }

        [Fact]
        public void FindShortestPath_ShouldReturnCorrectPath()
        {
            // Arrange
            var graph = CreateTestGraph();

            // Act
            var result = _shortestPathService.FindShortestPath("A", "D", graph);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(new List<string> { "A", "B", "F", "G", "D" }, result.NodeNames);
            Assert.Equal(11, result.Distance);
        }

        [Fact]
        public void FindShortestPath_SameNode_ShouldReturnZeroDistance()
        {
            // Arrange
            var graph = CreateTestGraph();

            // Act
            var result = _shortestPathService.FindShortestPath("A", "A", graph);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.NodeNames);
            Assert.Equal("A", result.NodeNames[0]);
            Assert.Equal(0, result.Distance);
        }

        [Fact]
        public void FindShortestPath_UnreachableNode_ShouldReturnMaxDistance()
        {
            // Arrange
            var graph = CreateTestGraph();
            var j = new Node { Name = "J" };  // Not connected to any node
            graph.Add(j);

            // Act
            var result = _shortestPathService.FindShortestPath("A", "J", graph);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(int.MaxValue, result.Distance); // Represents infinity
        }
    }
}
