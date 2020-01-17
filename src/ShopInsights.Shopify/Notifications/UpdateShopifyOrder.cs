using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopifySharp;

namespace ShopInsights.Shopify.Notifications
{
    public class UpdateShopifyOrder : IRequest
    {
        Order Order { get; }

        public UpdateShopifyOrder(Order order)
        {
            Order = order;
        }

        public class UpdateShopifyOrderHandler : AsyncRequestHandler<UpdateShopifyOrder>
        {
            protected override Task Handle(UpdateShopifyOrder request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
