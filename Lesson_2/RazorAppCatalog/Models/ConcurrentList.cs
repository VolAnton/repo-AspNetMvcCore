namespace RazorAppCatalog.Models
{
    public sealed class ConcurrentList<T> where T : class
    {
        private List<T> _item = new List<T>();

        private readonly object _syncItem = new object();

        public List<T> Item { get => _item; set => _item = value; }

        public void AddItem(T someItem)
        {
            lock (_syncItem)
            {
                _item.Add(someItem);
            }
        }

        public void RemoveItem(T someItem)
        {
            lock (_syncItem)
            {
                for (int i = _item.Count - 1; i >= 0; i--)
                {
                    if (_item[i].Equals(someItem))
                    {
                        _item.Remove(_item[i]);
                    }
                }
            }
        }

        public void ClearItems()
        {
            lock (_syncItem)
            {
                _item.Clear();
            }
        }

        public ConcurrentList<T> GetItems()
        {
            lock (_syncItem)
            {
                return this;
            }
        }
    }
}
