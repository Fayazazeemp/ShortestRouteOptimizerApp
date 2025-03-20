using ShortestRouteOptimizerApp.Models;
using ShortestRouteOptimizerApp.Services;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Shortest Route Optimizer (Console App)");
        var graphService = new GraphService();
        var shortestPathService = new ShortestPathService();
        var graph = graphService.GenerateGraph();

        while(true)
        {
            Console.Write("Enter start node: ");
            string from = Console.ReadLine()?.ToUpper();

            Console.Write("Enter destination node: ");
            string to = Console.ReadLine()?.ToUpper();

            if (!graph.Exists(n => n.Name == from) || !graph.Exists(n => n.Name == to))
            {
                Console.WriteLine("Invalid nodes. Please enter valid node names.");
                return;
            }

            var result = shortestPathService.FindShortestPath(from, to, graph);

            Console.WriteLine($"\nShortest Path from {from} to {to}: {string.Join(" , ", result.NodeNames)}");
            Console.WriteLine($"Total Distance: {result.Distance}");

            Console.Write("\nFind another route? (y/n): ");
            if (Console.ReadLine().Trim().ToLower() != "y")
            {
                Console.WriteLine("\nExiting program. Thank you!");
                break;
            }
        }
        
    }
}
