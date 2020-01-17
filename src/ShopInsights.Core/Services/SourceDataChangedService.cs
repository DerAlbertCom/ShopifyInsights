using System.ComponentModel;
using System.Threading.Tasks;
using MediatR;
using ShopInsights.Services.Events;

namespace ShopInsights.Services
{
    public class SourceDataChangedService : ISourceDataChangedService
    {
        readonly IMediator _mediator;

        public SourceDataChangedService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Added<T>(T item) where T : class
        {
            return _mediator.Send(new SourceDataAdded(item));
        }

        public Task Updated<T>(T item) where T : class
        {
            return _mediator.Send(new SourceDataUpdated(item));
        }

        public Task Removed<T>(T item) where T : class
        {
            return _mediator.Send(new SourceDataRemoved(item));
        }
    }
}
