using System.Collections.Concurrent;

namespace RazorAppCatalog.Middleware.MetricMiddleware.MetricManager
{
    public interface IMetric
    {

        public void AddCount(string path);

        public IReadOnlyList<string> GetAll();
        
    }
}
