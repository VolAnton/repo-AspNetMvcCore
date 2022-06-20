using Microsoft.AspNetCore.Mvc;
using RazorApp_Lesson_1.Models;
using System.Diagnostics;

namespace RazorApp_Lesson_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Footer"] = "(c) VolAnton";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Footer"] = "(c) VolAnton";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}