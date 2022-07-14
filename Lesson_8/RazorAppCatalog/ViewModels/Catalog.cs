using RazorAppCatalog.DomainEvents;
using RazorAppCatalog.Services.Email;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RazorAppCatalog.ViewModels
{
    public sealed class Catalog : ICatalog
    {
        private readonly ConcurrentDictionary<int, Product> _productDict = new();

        public int Count => _productDict.Count;

        public async Task Add(Product product, IEmailService emailService, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (_productDict.TryAdd(product.Id, product))
                {
                    DomainEventsManager.Raise(new ProductAdded(product, cancellationToken));

                    //Так было раньше.
                    //await emailService.SendEmailAsync(product, cancellationToken);
                }

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция отменена");
            }

        }

        public void Remove(Product product) => _productDict.TryRemove(product.Id, out _);

        public void Clear() => _productDict.Clear();

        public IReadOnlyCollection<Product> GetAll() => _productDict.Values.ToArray();

    }
}