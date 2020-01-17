using FluentAssertions;
using ShopInsights.Services;
using ShopInsights.Services.Events;
using Xunit;

namespace ShopInsights.Core.Tests.Services
{
    public class Data1
    {
    }

    public class Data2
    {
    }

    public class Data1Added : IDataAddedEvent<Data1>
    {
        public Data1 Data { get; }

        public Data1Added(Data1 data)
        {
            Data = data;
        }
    }

    public class Data1Updated : IDataUpdatedEvent<Data1>
    {
        public Data1 Data { get; }

        public Data1Updated(Data1 data)
        {
            Data = data;
        }
    }

    public class Data1Removed : IDataRemovedEvent<Data1>
    {
        public Data1 Data { get; }

        public Data1Removed(Data1 data)
        {
            Data = data;
        }
    }

    public class Data2Removed : IDataRemovedEvent<Data2>
    {
        public Data2 Data { get; }

        public Data2Removed(Data2 data)
        {
            Data = data;
        }
    }

    public class DataChangedEventScannerTests
    {
        [Fact]
        public void Fill_the_store()
        {
            var scanner = new DataChangedEventScanner();
            scanner.ScanAssemblies(GetType().Assembly);


            scanner.Added.ContainsKey(typeof(Data1)).Should().BeTrue();
            scanner.Updated.ContainsKey(typeof(Data1)).Should().BeTrue();
            scanner.Removed.ContainsKey(typeof(Data1)).Should().BeTrue();
            scanner.Removed.ContainsKey(typeof(Data2)).Should().BeTrue();

            scanner.Added.TryGetValue(typeof(Data1), out var at1);
            scanner.Updated.TryGetValue(typeof(Data1), out var ut1);
            scanner.Removed.TryGetValue(typeof(Data1), out var rt1);
            scanner.Removed.TryGetValue(typeof(Data2), out var rt2);

            at1.Should().Be(typeof(Data1Added));
            ut1.Should().Be(typeof(Data1Updated));
            rt1.Should().Be(typeof(Data1Removed));
            rt2.Should().Be(typeof(Data2Removed));
        }

        [Fact]
        public void GetDataType()
        {
            var scanner = new DataChangedEventScanner();

            scanner.GetDataType(typeof(Data1Added)).Should().Be(typeof(Data1));
        }

        [Fact]
        public void GetFilteredTypes()
        {
            var types = new[]
            {
                typeof(Data1),
                typeof(Data1Added),
                typeof(Data1Updated),
                typeof(Data1Removed)
            };

            var scanner = new DataChangedEventScanner();
            scanner.IsDataChangedEventType(typeof(IDataRemovedEvent<Data1>)).Should().BeFalse();
            scanner.IsDataChangedEventType(typeof(Data1)).Should().BeFalse();
            scanner.IsDataChangedEventType(typeof(Data1Added)).Should().BeTrue();
            scanner.IsDataChangedEventType(typeof(Data1Updated)).Should().BeTrue();
            scanner.IsDataChangedEventType(typeof(Data1Updated)).Should().BeTrue();
        }
    }
}
