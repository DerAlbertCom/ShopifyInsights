using System.Threading;
using System.Threading.Tasks;
using DerAlbert.Extensions.Fakes;
using MediatR;
using NSubstitute;
using ShopInsights.Services;
using ShopInsights.Services.Events;
using Xunit;

namespace ShopInsights.Core.Tests.Services
{
    public class SourceDataUpdateHandlerTests : WithSubject<SourceDataUpdated.SourceDataUpdatedHandler>
    {
        [Fact]
        public async Task Should_send_event_for_update()
        {
            var item = new Data1();
            var updated = new SourceDataUpdated(item);

            var eventInstance = new Data1Updated(item);

            The<IDataChangedEventRepository>().FindUpdatedEvent(item).Returns(eventInstance);

            IRequestHandler<SourceDataUpdated> handler = Subject;

            var token = CancellationToken.None;
            await handler.Handle(updated, token);

            await The<IMediator>().Received().Send(eventInstance, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Should_not_send_an_event_no_existing_eventType()
        {
            var item = new Data1();
            var updated = new SourceDataUpdated(item);
            The<IDataChangedEventRepository>().FindUpdatedEvent(item).Returns((SourceDataUpdated) null);
            IRequestHandler<SourceDataUpdated> handler = Subject;
            await handler.Handle(updated, CancellationToken.None);

            await The<IMediator>().DidNotReceive().Send(Arg.Any<IRequest>(), Arg.Any<CancellationToken>());
        }
    }

    public class SourceDataAddedHandlerTests : WithSubject<SourceDataAdded.SourceDataAddedHandler>
    {
        [Fact]
        public async Task Should_send_event_for_added()
        {
            var item = new Data1();
            var added = new SourceDataAdded(item);

            var eventInstance = new Data1Added(item);

            The<IDataChangedEventRepository>().FindAddedEvent(item).Returns(eventInstance);

            IRequestHandler<SourceDataAdded> handler = Subject;

            var token = CancellationToken.None;
            await handler.Handle(added, token);

            await The<IMediator>().Received().Send(eventInstance, token);
        }

        [Fact]
        public async Task Should_not_send_an_event_no_existing_eventType()
        {
            var item = new Data1();
            var added = new SourceDataAdded(item);
            The<IDataChangedEventRepository>().FindAddedEvent(item).Returns((SourceDataUpdated) null);
            IRequestHandler<SourceDataAdded> handler = Subject;
            await handler.Handle(added, CancellationToken.None);

            await The<IMediator>().DidNotReceive().Send(Arg.Any<IRequest>(), Arg.Any<CancellationToken>());
        }
    }

    public class SourceDataRemovedHandlerTests : WithSubject<SourceDataRemoved.SourceDataRemovedHandler>
    {
        [Fact]
        public async Task Should_send_event_for_added()
        {
            var item = new Data1();
            var removed = new SourceDataRemoved(item);

            var eventInstance = new Data1Removed(item);

            The<IDataChangedEventRepository>().FindRemovedEvent(item).Returns(eventInstance);

            IRequestHandler<SourceDataRemoved> handler = Subject;

            var token = CancellationToken.None;
            await handler.Handle(removed, token);

            await The<IMediator>().Received().Send(eventInstance, token);
        }

        [Fact]
        public async Task Should_not_send_an_event_no_existing_eventType()
        {
            var item = new Data1();
            var removed = new SourceDataRemoved(item);
            The<IDataChangedEventRepository>().FindRemovedEvent(item).Returns((SourceDataRemoved) null);
            IRequestHandler<SourceDataRemoved> handler = Subject;
            await handler.Handle(removed, CancellationToken.None);

            await The<IMediator>().DidNotReceive().Send(Arg.Any<IRequest>(), Arg.Any<CancellationToken>());
        }
    }
}
