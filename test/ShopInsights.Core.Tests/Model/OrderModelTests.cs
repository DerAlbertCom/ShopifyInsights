using FluentAssertions;
using ShopInsights.Model;
using Xunit;

namespace ShopInsights.Core.Tests.Model
{
    public class OrderModelTests
    {
        [Fact]
        public void Should_show_discounted_price_if_different_from_totalPrice()
        {
            var orderModel = new OrderModel
            {
                DiscountAmount = 1.0m,
                TotalPrice = 2.0m,
            };

            orderModel.SalesPrice.Should().Be(1.0m);
        }
        [Fact]
        public void Should_show_total_price_if_price_is_the_same()
        {
            var orderModel = new OrderModel
            {
                DiscountAmount = 2.0m,
                TotalPrice = 2.0m,
            };

            orderModel.SalesPrice.Should().Be(0.0m);
        }
        [Fact]
        public void Should_show_total_price_if_discount_price_has_no_value()
        {
            var orderModel = new OrderModel
            {
                TotalPrice = 2.0m
            };

            orderModel.SalesPrice.Should().Be(2.0m);
        }
    }
}
