using Microsoft.AspNetCore.Mvc;
using RazorAppCatalog.ViewModels;
using RazorAppCatalog.Services;
using RazorAppCatalog.Services.Email;

namespace RazorAppCatalog.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _products = new Catalog();

        private readonly IEmailService _emailService;

        public CatalogController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Products()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_products);
        }

        [HttpGet]
        public IActionResult RemoveProduct()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_products);
        }

        [HttpPost]
        public IActionResult RemoveProduct(Product model)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _products.Remove(model);
            return View(_products);
        }

        [HttpGet]
        public IActionResult ClearCatalog()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_products);
        }

        [HttpPost]
        public IActionResult ClearCatalog(Object obj)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _products.Clear();
            return View(_products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _products.Add(model, _emailService);
            return View(_products);
        }

    }
}
