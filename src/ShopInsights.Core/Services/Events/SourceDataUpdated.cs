using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ShopInsights.Services.Events
{
    public class SourceDataUpdated : IRequest
    {
        public SourceDataUpdated(object sourceData)
        {
            SourceData = sourceData ?? throw new ArgumentNullException(nameof(sourceData));
        }

        public object SourceData { get; }

        public class SourceDataUpdatedHandler : AsyncRequestHandler<SourceDataUpdated>
        {
            readonly IMediator _mediator;
            readonly IDataChangedEventRepository _repository;

            public SourceDataUpdatedHandler(IMediator mediator, IDataChangedEventRepository repository)
            {
                _mediator = mediator;
                _repository = repository;
            }
            protected override async Task Handle(SourceDataUpdated request, CancellationToken cancellationToken)
            {
                var eventInstance = _repository.FindUpdatedEvent(request.SourceData);
                if (eventInstance == null)
                {
                    return;
                }
                await _mediator.Send(eventInstance, cancellationToken);
            }
        }
    }
}
