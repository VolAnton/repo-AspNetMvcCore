using System.Collections.Concurrent;

namespace RazorAppCatalog.Middleware.MetricMiddleware.MetricManager
{
    public class Metric : IMetric
    {        
        private readonly object _syncObj = new();

        private int[] _metrics = new int[6];            

        public Metric()
        {
            lock (_syncObj)
            {
                for (int i = 0; i < 6; i++)
                {
                    _metrics[i] = 0;
                }
            }
        }

        public void AddCount(string path)
        {
            lock (_syncObj)
            {
                switch (path)
                {
                    case @"/":
                        _metrics[0] = _metrics[0] + 1;
                        break;
                    case @"/Home/Privacy":
                        _metrics[1] = _metrics[1] + 1;
                        break;
                    case @"/Catalog/Products":
                        _metrics[2] = _metrics[2] + 1;
                        break;
                    case @"/Catalog/AddProduct":
                        _metrics[3] = _metrics[3] + 1;
                        break;
                    case @"/Catalog/RemoveProduct":
                        _metrics[4] = _metrics[4] + 1;
                        break;
                    case @"/Catalog/ClearCatalog":
                        _metrics[5] = _metrics[5] + 1;
                        break;
                    default:
                        break;
                }
            }
        }

        public IReadOnlyList<string> GetAll()
        {
            lock (_syncObj)
            {
                List<string> results = new List<string>();

                for (int i = 0; i < 6; i++)
                {
                    switch (i)
                    {
                        case 0:
                            results.Add($"https://localhost:7004/: {_metrics[i]}");
                            break;
                        case 1:
                            results.Add($"https://localhost:7004/Home/Privacy: {_metrics[i]}");
                            break;
                        case 2:
                            results.Add($"https://localhost:7004/Catalog/Products: {_metrics[i]}");
                            break;
                        case 3:
                            results.Add($"https://localhost:7004/Catalog/AddProduct: {_metrics[i]}");
                            break;
                        case 4:
                            results.Add($"https://localhost:7004/Catalog/RemoveProduct: {_metrics[i]}");
                            break;
                        case 5:
                            results.Add($"https://localhost:7004/Catalog/ClearCatalog: {_metrics[i]}");
                            break;
                        default:
                            break;
                    }

                }

                return results;
            }
        }
    }

    // home https://localhost:7004/
    // privacy https://localhost:7004/Home/Privacy
    // список товаров https://localhost:7004/Catalog/Products
    // добавление товаров https://localhost:7004/Catalog/AddProduct
    // удаление товаров https://localhost:7004/Catalog/RemoveProduct
    // очистить список товаров https://localhost:7004/Catalog/ClearCatalog
}
