using MediatR;

namespace ShopInsights.Services.Events
{
    public interface IDataAddedEvent<T> : IRequest
    {
    }
}
