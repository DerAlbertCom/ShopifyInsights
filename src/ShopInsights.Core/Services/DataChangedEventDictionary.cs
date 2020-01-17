using System;
using System.Collections.Generic;
using System.Linq;
using ShopInsights.Services.Events;

namespace ShopInsights.Services
{
    public class DataChangedEventDictionary
    {
        readonly DataChangedEvent _dataChangedEvent;
        readonly Dictionary<Type, Type> _dictionary;

        public DataChangedEventDictionary(DataChangedEvent dataChangedEvent)
        {
            _dataChangedEvent = dataChangedEvent;
            _dictionary = new Dictionary<Type, Type>();
        }

        public Type this[Type dataType]
        {
            get => _dictionary[dataType];
            set => Add(dataType, value);
        }


        public bool ContainsKey(Type dataType)
        {
            return _dictionary.ContainsKey(dataType);
        }

        public void Add(Type dataType, Type eventType)
        {
            EnsureEventType(dataType, eventType);
            EnsureCtor(dataType, eventType);
            _dictionary.Add(dataType, eventType);
        }

        void EnsureCtor(Type dataType, Type eventType)
        {
            var ctor = eventType.GetConstructors().FirstOrDefault();
            if (ctor != null)
            {
                var parameters = ctor.GetParameters();
                if (parameters.Length == 1)
                {
                    if (parameters[0].ParameterType == dataType)
                    {
                        return;
                    }
                }
            }
            throw new ArgumentException($"The Event {eventType.FullName} has no constructor which accepts {dataType.FullName} as a single parameter.");
        }

        void EnsureEventType(Type dataType, Type eventType)
        {
            if (eventType.IsAbstract)
            {
                throw new ArgumentException($"The Type {eventType.FullName} must be a concrete type.");
            }

            var interfaceType = GetInterfaceType(_dataChangedEvent);
            var expectedBaseInterface = interfaceType.MakeGenericType(dataType);

            if (!expectedBaseInterface.IsAssignableFrom(eventType))
            {
                throw new ArgumentException(
                    $"The Event {eventType.FullName} does not implements {GetFriendlyName(expectedBaseInterface)}.");
            }
        }

        static string GetFriendlyName(Type type)
        {
            if (type.IsGenericType)
            {
                var genericTypeNames = type.GetGenericArguments().Select(GetFriendlyName).ToArray();
                return type.Name.Split('`')[0] + "<" + string.Join(", ", genericTypeNames) + ">";
            }

            return type.Name;
        }

        static Type GetInterfaceType(DataChangedEvent dataChangedEvent)
        {
            return dataChangedEvent switch
            {
                DataChangedEvent.Added => typeof(IDataAddedEvent<>),
                DataChangedEvent.Removed => typeof(IDataRemovedEvent<>),
                DataChangedEvent.Updated => typeof(IDataUpdatedEvent<>),
                _ => throw new ArgumentException($"{dataChangedEvent} is not handled")
            };
        }

        public bool Remove(Type dataType)
        {
            return _dictionary.Remove(dataType);
        }

        public bool TryGetValue(Type dataType, out Type eventType)
        {
            return _dictionary.TryGetValue(dataType, out eventType);
        }
    }
}
