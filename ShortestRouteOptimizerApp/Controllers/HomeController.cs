using Microsoft.AspNetCore.Mvc;
using ShortestRouteOptimizerApp.Models;
using ShortestRouteOptimizerApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace ShortestRouteOptimizerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShortestPathService _shortestPathService;
        private readonly GraphService _graphService;
        private readonly List<Node> _graph;

        public HomeController()
        {
            _shortestPathService = new ShortestPathService();
            _graphService = new GraphService();
            _graph = _graphService.GenerateGraph();
        }

        public IActionResult Index()
        {
            ViewBag.Nodes = _graph.Select(n => n.Name).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult FindShortestPath(string fromNode, string toNode)
        {
            var result = _shortestPathService.FindShortestPath(fromNode, toNode, _graph);
            ViewBag.SelectedFrom = fromNode;
            ViewBag.SelectedTo = toNode;
            ViewBag.Path = string.Join(", ", result.NodeNames);
            ViewBag.Distance = result.Distance;
            ViewBag.Nodes = _graph.Select(n => n.Name).ToList();
            return View("Index");
        }
    }
}
