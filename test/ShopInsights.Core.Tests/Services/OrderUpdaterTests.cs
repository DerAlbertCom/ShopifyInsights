using System;
using System.Collections.Generic;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using ShopifySharp;
using ShopInsights.Core.Services;
using Xunit;

namespace ShopInsights.Core.Tests.Services
{
    public class OrderUpdaterTests : WithSubject<OrderUpdater>
    {
        [Fact]
        public void Should_add_the_Order_if_it_not_exists()
        {
            var orders = new Dictionary<int, Order>();
            var order1 = new Order()
            {
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now
            };

            Subject.Update(orders, order1);

            orders.Count.Should().Be(1);

            orders.Keys.Should().Contain(2);
            orders.Values.Should().Contain(order1);
        }

        [Fact]
        public void Should_add_a_second_order_if_it_not_exists()
        {
            var orders = new Dictionary<int, Order>();
            var order1 = new Order()
            {
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now
            };
            var order2 = new Order()
            {
                OrderNumber = 10,
                UpdatedAt = DateTimeOffset.Now
            };

            Subject.Update(orders, order1);
            Subject.Update(orders, order2);

            orders.Count.Should().Be(2);

            orders.Keys.Should().Contain(2);
            orders.Keys.Should().Contain(10);

            orders.Values.Should().Contain(order1);
            orders.Values.Should().Contain(order2);

        }

        [Fact]
        public void Should_not_update_existing_order_if_it_is_older()
        {
            var orders = new Dictionary<int, Order>();
            var order1 = new Order()
            {
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now
            };
            var order2 = new Order()
            {
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(1))
            };

            Subject.Update(orders, order1);
            Subject.Update(orders, order2);

            orders.Count.Should().Be(1);

            orders.Keys.Should().Contain(2);

            orders.Values.Should().Contain(order1);

        }

        [Fact]
        public void Should_update_existing_order_if_it_is_newer()
        {
            var orders = new Dictionary<int, Order>();
            var order1 = new Order()
            {
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now
            };
            var order2 = new Order()
            {
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now.Add(TimeSpan.FromDays(1))
            };

            Subject.Update(orders, order1);
            Subject.Update(orders, order2);

            orders.Count.Should().Be(1);

            orders.Keys.Should().Contain(2);

            orders.Values.Should().Contain(order2);

        }

    }
}
