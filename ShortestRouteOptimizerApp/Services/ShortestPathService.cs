using ShortestRouteOptimizerApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShortestRouteOptimizerApp.Services
{
    public class ShortestPathService
    {
        public ShortestPathData FindShortestPath(string fromNode, string toNode, List<Node> graph)
        {
            var distances = new Dictionary<string, int>();
            var previousNodes = new Dictionary<string, string>();
            var unvisitedNodes = new List<Node>(graph);
            var path = new List<string>();

            foreach (var node in graph)
                distances[node.Name] = int.MaxValue;
            distances[fromNode] = 0;

            while (unvisitedNodes.Count > 0)
            {
                var currentNode = unvisitedNodes.OrderBy(n => distances[n.Name]).First();
                unvisitedNodes.Remove(currentNode);

                if (currentNode.Name == toNode) break;

                foreach (var edge in currentNode.Edges)
                {
                    int newDist = distances[currentNode.Name] + edge.Distance;
                    if (newDist < distances[edge.Destination.Name])
                    {
                        distances[edge.Destination.Name] = newDist;
                        previousNodes[edge.Destination.Name] = currentNode.Name;
                    }
                }
            }

            string step = toNode;
            while (previousNodes.ContainsKey(step))
            {
                path.Insert(0, step);
                step = previousNodes[step];
            }
            path.Insert(0, fromNode);

            return new ShortestPathData { NodeNames = path, Distance = distances[toNode] };
        }
    }

    public class ShortestPathData
    {
        public List<string> NodeNames { get; set; } = new List<string>();
        public int Distance { get; set; }
    }
}

