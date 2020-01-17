using MediatR;

namespace ShopInsights.Services.Events
{
    public interface IDataUpdatedEvent<T> : IRequest
    {
    }
}