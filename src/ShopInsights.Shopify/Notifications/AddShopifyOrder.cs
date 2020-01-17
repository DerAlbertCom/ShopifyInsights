using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopifySharp;

namespace ShopInsights.Shopify.Notifications
{
    public class AddShopifyOrder : IRequest
    {
        public Order Order { get; }

        public AddShopifyOrder(Order order)
        {
            Order = order;
        }

        public class AddShopifyOrderHandler : AsyncRequestHandler<AddShopifyOrder>
        {
            protected override Task Handle(AddShopifyOrder request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
