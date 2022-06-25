using Microsoft.AspNetCore.Mvc;
using RazorAppCatalog.Models;
using RazorAppCatalog.Services;

namespace RazorAppCatalog.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _products = new Catalog();

        [HttpGet]
        public IActionResult Products()
        {
            ViewData["Footer"] = Footer.footer;
            return View(_products.GetProducts());
        }        

        [HttpGet]
        public IActionResult RemoveProduct()
        {
            ViewData["Footer"] = Footer.footer;
            return View(_products.GetProducts());
        }

        [HttpPost]
        public IActionResult RemoveProduct(Product model)
        {
            ViewData["Footer"] = Footer.footer;
            _products.RemoveProduct(model);
            return View(_products.GetProducts());
        }

        [HttpGet]
        public IActionResult ClearCatalog()
        {
            ViewData["Footer"] = Footer.footer;
            return View(_products.GetProducts());
        }

        [HttpPost]
        public IActionResult ClearCatalog(Object obj)
        {
            ViewData["Footer"] = Footer.footer;
            _products.ClearCatalog();
            return View(_products.GetProducts());
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData["Footer"] = Footer.footer;
            return View(_products.GetProducts());
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            ViewData["Footer"] = Footer.footer;
            _products.AddProduct(model);
            return View(_products.GetProducts());
        }

    }
}
