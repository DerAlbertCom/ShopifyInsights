using MediatR;

namespace ShopInsights.Services.Events
{
    public interface IDataRemovedEvent<T> : IRequest
    {
    }
}