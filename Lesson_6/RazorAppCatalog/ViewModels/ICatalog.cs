using RazorAppCatalog.Services.Email;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RazorAppCatalog.ViewModels
{
    public interface ICatalog
    {
        public void Add(Product product, IEmailService emailService);

        public void Remove(Product product);

        public void Clear();

        public IReadOnlyCollection<Product> GetAll();

    }
}