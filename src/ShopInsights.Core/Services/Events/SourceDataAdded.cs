using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ShopInsights.Services.Events
{
    public class SourceDataAdded : IRequest
    {
        public SourceDataAdded(object sourceData)
        {
            SourceData = sourceData ?? throw new ArgumentNullException(nameof(sourceData));
        }

        public object SourceData { get; }

        public class SourceDataAddedHandler : AsyncRequestHandler<SourceDataAdded>
        {
            readonly IMediator _mediator;
            readonly IDataChangedEventRepository _repository;

            public SourceDataAddedHandler(IMediator mediator, IDataChangedEventRepository repository)
            {
                _mediator = mediator;
                _repository = repository;
            }
            protected override async Task Handle(SourceDataAdded request, CancellationToken cancellationToken)
            {
                var eventInstance = _repository.FindAddedEvent(request.SourceData);
                if (eventInstance == null)
                {
                    return;
                }
                await _mediator.Send(eventInstance, cancellationToken);
            }
        }
    }
}
