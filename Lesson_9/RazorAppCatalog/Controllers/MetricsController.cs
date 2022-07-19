using Microsoft.AspNetCore.Mvc;
using RazorAppCatalog.Services;
using RazorAppCatalog.Services.Email;
using RazorAppCatalog.Entities;
using RazorAppCatalog.Middleware.MetricMiddleware.MetricManager;

namespace RazorAppCatalog.Controllers
{
    public class MetricsController : Controller
    {
        private static IMetric _metric;

        public MetricsController(IMetric metric)
        {
            ArgumentNullException.ThrowIfNull(metric);
            _metric = metric;
        }

        [HttpGet]
        public IActionResult metrics()
        {
            ViewData[VDKeys.Footer] = VDKeys.footerValue;
            return View(_metric);
        }

    }
}
