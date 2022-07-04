using Microsoft.AspNetCore.Mvc;
using RazorAppCatalog.ViewModels;
using RazorAppCatalog.Services;
using System.Diagnostics;

namespace RazorAppCatalog.Controllers
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
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}