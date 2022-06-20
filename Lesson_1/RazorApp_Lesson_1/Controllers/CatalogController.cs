using Microsoft.AspNetCore.Mvc;
using RazorApp_Lesson_1.Models;

namespace RazorApp_Lesson_1.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _catalog = new();

        [HttpGet]
        public IActionResult Products()
        {
            ViewData["Footer"] = "(c) VolAnton";
            return View(_catalog);
        }

        [HttpGet]
        public IActionResult Categories()
        {
            ViewData["Footer"] = "(c) VolAnton";
            return View(_catalog);
        }

        [HttpPost]
        public IActionResult Categories(Category model)
        {
            ViewData["Footer"] = "(c) VolAnton";
            _catalog.Categories.Add(model);
            return View(_catalog);
        }

    }
}
