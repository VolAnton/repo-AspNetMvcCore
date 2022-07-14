namespace RazorAppCatalog.DomainEvents
{
    public class ProductAdded : IDomainEvent
    {
        public Product Product { get; }

        public CancellationToken CancellationToken { get; }

        public ProductAdded(Product product, CancellationToken cancellationToken)
        {
            Product = product;
            CancellationToken = cancellationToken;
        }

    }
}
