namespace RazorAppCatalog.Models
{
    public sealed class Catalog
    {
        private List<Product> _products = new List<Product>();

        private readonly object _syncProducts = new object();

        public List<Product> Products { get => _products; set => _products = value; }

        public void AddProduct(Product product)
        {
            lock (_syncProducts)
            {
                _products.Add(product);
            }
        }

        public void RemoveProduct(Product product)
        {
            lock (_syncProducts)
            {
                for (int i = _products.Count - 1; i >= 0; i--)
                {
                    if (_products[i].Id.Equals(product.Id) && _products[i].Name.Equals(product.Name))
                    {
                        _products.Remove(_products[i]);
                    }
                }
            }
        }

        public void ClearCatalog()
        {
            lock (_syncProducts)
            {
                _products.Clear();
            }
        }

        public Catalog GetProducts()
        {
            lock (_syncProducts)
            {
                return this;
            }
        }

    }
}

public sealed class Product
{
    private int _id;

    private string _name;

    public int Id { get => _id; set => _id = value; }

    public string Name { get => _name; set => _name = value; }

}

