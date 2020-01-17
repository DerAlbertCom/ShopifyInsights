using System;
using System.Threading.Tasks;
using DerAlbert.Extensions.Fakes;
using MediatR;
using NSubstitute;
using ShopInsights.Services;
using ShopInsights.Services.Events;
using Xunit;

namespace ShopInsights.Core.Tests.Services
{
    public class SourceDataChangedServiceTests : WithSubject<SourceDataChangedService>
    {
        [Fact]
        public async Task Emits_SourceDataAdded_Event()
        {
            var testObject = new TestObject(1);
            await Subject.Added(testObject);
            await The<IMediator>().Received().Send(Arg.Any<SourceDataAdded>());
        }

        [Fact]
        public async Task Emits_SourceDataAdded_Event_with_the_same_source_data()
        {
            var testObject = new TestObject(10);
            await Subject.Added(testObject);
            await The<IMediator>().Received()
                .Send(Arg.Is<SourceDataAdded>(e => ReferenceEquals(testObject, e.SourceData)));
        }

        [Fact]
        public async Task Emits_SourceDataUpdated_Event()
        {
            var testObject = new TestObject(2);
            await Subject.Updated(testObject);
            await The<IMediator>().Received().Send(Arg.Any<SourceDataUpdated>());
        }

        [Fact]
        public async Task Emits_SourceDataUpdated_Event_with_the_same_source_data()
        {
            var testObject = new TestObject(20);
            await Subject.Updated(testObject);
            await The<IMediator>().Received()
                .Send(Arg.Is<SourceDataUpdated>(e => ReferenceEquals(testObject, e.SourceData)));
        }

        [Fact]
        public async Task Emits_SourceDataRemoved_Event()
        {
            var testObject = new TestObject(3);
            await Subject.Removed(testObject);
            await The<IMediator>().Received().Send(Arg.Any<SourceDataRemoved>());
        }

        [Fact]
        public async Task Emits_SourceDataRemoved_Event_with_the_same_source_data()
        {
            var testObject = new TestObject(30);
            await Subject.Removed(testObject);
            await The<IMediator>().Received()
                .Send(Arg.Is<SourceDataRemoved>(e => ReferenceEquals(testObject, e.SourceData)));
        }
    }

    public class TestObject
    {
        readonly int _id;

        public TestObject(int id)
        {
            _id = id;
        }
    }
}
