﻿using Microsoft.AspNetCore.Mvc;
using RazorAppCatalog.ViewModels;
using RazorAppCatalog.Services;
using RazorAppCatalog.Services.Email;

namespace RazorAppCatalog.Controllers
{
    public class CatalogController : Controller
    {
        private static ICatalog _catalog;

        private readonly IEmailService _emailService;

        public CatalogController(ICatalog catalog, IEmailService emailService)
        {
            ArgumentNullException.ThrowIfNull(catalog);
            _catalog = catalog;

            ArgumentNullException.ThrowIfNull(emailService);
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Products()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_catalog);
        }

        [HttpGet]
        public IActionResult RemoveProduct()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_catalog);
        }

        [HttpPost]
        public IActionResult RemoveProduct(Product model)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _catalog.Remove(model);
            return View(_catalog);
        }

        [HttpGet]
        public IActionResult ClearCatalog()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_catalog);
        }

        [HttpPost]
        public IActionResult ClearCatalog(Object obj)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _catalog.Clear();
            return View(_catalog);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_catalog);
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            _catalog.Add(model, _emailService);
            return View(_catalog);
        }

    }
}
