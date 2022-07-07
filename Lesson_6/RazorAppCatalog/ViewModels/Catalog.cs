using RazorAppCatalog.Services.Email;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RazorAppCatalog.ViewModels
{
    public sealed class Catalog : ICatalog
    {
        private readonly ConcurrentDictionary<int, Product> _productDict = new();

        public int Count => _productDict.Count;

        public async void Add(Product product, IEmailService emailService)
        {
            if (_productDict.TryAdd(product.Id, product))
            {
                await emailService.SendEmailAsync(product);
            }
        }

        public void Remove(Product product) => _productDict.TryRemove(product.Id, out _);

        public void Clear() => _productDict.Clear();

        public IReadOnlyCollection<Product> GetAll() => _productDict.Values.ToArray();

    }
}