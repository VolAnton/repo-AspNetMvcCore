using System.Collections.Concurrent;

namespace RazorAppCatalog.DomainEvents
{
    public static class DomainEventsManager
    {
        private static readonly ConcurrentDictionary<Type, List<Delegate>> _handlers = new();

        public static void Register<T>(Action<T> eventHandler) where T : IDomainEvent
        {
            _handlers.AddOrUpdate(typeof(T),
                addValueFactory: _ => new List<Delegate>() { eventHandler },
                updateValueFactory: (_, delegates) =>
                {
                    delegates.Add(eventHandler);
                    return delegates;
                });
        }

        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            foreach (Delegate handler in _handlers[domainEvent.GetType()])
            {
                var action = (Action<T>)handler;
                action(domainEvent);
            }
        }

    }
}
