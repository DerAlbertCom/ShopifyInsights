using System;
using System.Linq;
using System.Reflection;
using ShopInsights.Services.Events;

namespace ShopInsights.Services
{
    public class DataChangedEventScanner
    {
        public DataChangedEventDictionary Added { get; } = new DataChangedEventDictionary(DataChangedEvent.Added);

        public DataChangedEventDictionary Updated { get; } = new DataChangedEventDictionary(DataChangedEvent.Updated);

        public DataChangedEventDictionary Removed { get; } = new DataChangedEventDictionary(DataChangedEvent.Removed);

        public void ScanAssemblies(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                ScanAssembly(assembly);
            }
        }

        void ScanAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes().Where(IsDataChangedEventType).ToArray();
            foreach (var type in types)
            {
                var interfaceEvent = FindEventInterface(type);
                var dataType = GetDataType(type);
                if (IsDataAddedEvent(interfaceEvent))
                {

                    Added.Add(dataType, type);
                } else if (IsDataUpdatedEvent(interfaceEvent))
                {
                    Updated.Add(dataType, type);
                } else if (IsDataRemovedEvent(interfaceEvent))
                {
                    Removed.Add(dataType, type);
                }

            }
        }

        internal Type GetDataType(Type type)
        {
            var interfaceEvent = FindEventInterface(type);
            return interfaceEvent.GenericTypeArguments.First();
        }

        internal bool IsDataChangedEventType(Type type)
        {
            if (type.IsAbstract)
            {
                return false;
            }

            return FindEventInterface(type) != null;
        }

        Type FindEventInterface(Type type)
        {
            var interfaces = type.GetInterfaces().Where(i => i.IsGenericType);
            return interfaces.FirstOrDefault(i =>
            {
                var genericType = i.GetGenericTypeDefinition();
                return IsDataAddedEvent(genericType) || IsDataRemovedEvent(genericType) ||
                       IsDataUpdatedEvent(genericType);
            });
        }

        bool IsDataAddedEvent(Type type)
        {
            return type.GetGenericTypeDefinition() == typeof(IDataAddedEvent<>);
        }

        bool IsDataRemovedEvent(Type type)
        {
            return type.GetGenericTypeDefinition() == typeof(IDataRemovedEvent<>);
        }

        bool IsDataUpdatedEvent(Type type)
        {
            return type.GetGenericTypeDefinition() == typeof(IDataUpdatedEvent<>);
        }
    }
}
