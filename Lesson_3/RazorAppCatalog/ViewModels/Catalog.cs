using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RazorAppCatalog.ViewModels
{
    public sealed class Catalog
    {
        private static int _startCounter = 0;

        private ConcurrentDictionary<int, Product> _CDProducts = new ConcurrentDictionary<int, Product>();

        public ConcurrentDictionary<int, Product> CDProducts { get => _CDProducts; set => _CDProducts = value; }
                
        public void AddProduct(Product product)
        {
            if(_CDProducts.TryAdd(_startCounter, product))
            {
                _startCounter++;
            }
        }

        public void RemoveProduct(Product product)
        {            
            foreach(KeyValuePair<int, Product> kvp in _CDProducts)
            {
                if (kvp.Value.Id.Equals(product.Id) && kvp.Value.Name.Equals(product.Name))
                {
                    _CDProducts.TryRemove(kvp);
                }
            }
        }

        public void ClearCatalog()
        {
            _CDProducts.Clear();
        }

        public Catalog GetProducts()
        {
            return this;
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
