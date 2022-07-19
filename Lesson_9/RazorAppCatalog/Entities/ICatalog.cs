using RazorAppCatalog.Services.Email;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RazorAppCatalog.Entities
{
    public interface ICatalog
    {
        public Task Add(Product product, IEmailService emailService, CancellationToken cancellationToken);

        public void Remove(Product product);

        public void Clear();

        public IReadOnlyCollection<Product> GetAll();

    }
}