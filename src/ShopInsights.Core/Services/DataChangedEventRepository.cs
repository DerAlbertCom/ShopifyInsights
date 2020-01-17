using System;
using MediatR;
using ShopInsights.Services.Events;

namespace ShopInsights.Services
{
    public interface IDataChangedEventRepository
    {
        IRequest FindAddedEvent(object item);

        IRequest FindUpdatedEvent(object item);

        IRequest FindRemovedEvent(object item);
    }

    public class DataChangedEventRepository : IDataChangedEventRepository
    {
        readonly DataChangedEventDictionary _added;
        readonly DataChangedEventDictionary _updated;
        readonly DataChangedEventDictionary _removed;

        public DataChangedEventRepository(DataChangedEventDictionary added, DataChangedEventDictionary updated,
            DataChangedEventDictionary removed)
        {
            _added = added;
            _updated = updated;
            _removed = removed;
        }

        public IRequest FindAddedEvent(object item)
        {
            var dataType = item.GetType();
            if (_added.TryGetValue(dataType, out var type))
            {
                return (IRequest) CreateInstance(type, item);
            }

            return null;
        }


        public IRequest FindUpdatedEvent(object item)
        {
            var dataType = item.GetType();
            if (_updated.TryGetValue(dataType, out var type))
            {
                return (IRequest) CreateInstance(type, item);
            }

            return null;
        }

        public IRequest FindRemovedEvent(object item)
        {
            var dataType = item.GetType();
            if (_removed.TryGetValue(dataType, out var type))
            {
                return (IRequest) CreateInstance(type, item);
            }

            return null;
        }

        object CreateInstance<T>(Type type, T item)
        {
            return Activator.CreateInstance(type, item);
        }
    }
}
