using RazorAppCatalog.Middleware.MetricMiddleware.MetricManager;

namespace RazorAppCatalog.Middleware.MetricMiddleware
{
    public class MetricMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<MetricMiddleware> _logger;        

        public MetricMiddleware(RequestDelegate next, ILogger<MetricMiddleware> logger)
        {
            _next = next;
            _logger = logger;            
        }

        public async Task InvokeAsync(HttpContext context, IMetric metric)
        {
            PathString path = context.Request.Path;

            _logger.LogInformation("Request Method: {Method}", context.Request.Method);                        

            metric.AddCount(context.Request.Path);

            await _next(context);
        }
    }
}
