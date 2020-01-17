using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ShopInsights.Services.Events
{
    public class SourceDataRemoved : IRequest
    {
        public SourceDataRemoved(object sourceData)
        {
            SourceData = sourceData ?? throw new ArgumentNullException(nameof(sourceData));
        }

        public object SourceData { get; }

        public class SourceDataRemovedHandler : AsyncRequestHandler<SourceDataRemoved>
        {
            readonly IMediator _mediator;
            readonly IDataChangedEventRepository _repository;

            public SourceDataRemovedHandler(IMediator mediator, IDataChangedEventRepository repository)
            {
                _mediator = mediator;
                _repository = repository;
            }
            protected override async Task Handle(SourceDataRemoved request, CancellationToken cancellationToken)
            {
                var eventInstance = _repository.FindRemovedEvent(request.SourceData);
                if (eventInstance == null)
                {
                    return;
                }
                await _mediator.Send(eventInstance, cancellationToken);
            }
        }
    }
}
