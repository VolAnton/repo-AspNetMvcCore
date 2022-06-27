using Microsoft.AspNetCore.Mvc;
using RazorAppCatalog.ViewModels;
using RazorAppCatalog.Services;

namespace RazorAppCatalog.Controllers
{
    public class CatalogController : Controller
    {        
        private static Catalog _products = new Catalog();

        [HttpGet]
        public IActionResult Products()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_products.GetProducts());
        }        

        [HttpGet]
        public IActionResult RemoveProduct()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;            
            return View(_products.GetProducts());
        }

        [HttpPost]
        public IActionResult RemoveProduct(Product model)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _products.RemoveProduct(model);            
            return View(_products.GetProducts());
        }

        [HttpGet]
        public IActionResult ClearCatalog()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;            
            return View(_products.GetProducts());
        }

        [HttpPost]
        public IActionResult ClearCatalog(Object obj)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _products.ClearCatalog();            
            return View(_products.GetProducts());
        }

        [HttpGet]
        public IActionResult AddProduct()
        {            
            ViewData[VDKeys.Footer] = VDKeys.footerValue;            
            return View(_products.GetProducts());
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _products.AddProduct(model);            
            return View(_products.GetProducts());
        }

    }
}
