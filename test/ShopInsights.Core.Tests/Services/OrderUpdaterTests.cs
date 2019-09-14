using System;
using System.Linq;
using FluentAssertions;
using ShopifySharp;
using ShopInsights.Core.Services;
using Xunit;

namespace ShopInsights.Core.Tests.Services
{
    public class OrderUpdaterTests
    {
        public OrderUpdaterTests()
        {
            Subject = new OrderUpdater();
        }

        OrderUpdater Subject { get; }

        [Fact]
        public void Should_add_the_Order_if_it_not_exists()
        {
            var orders = new OrderDictionary();
            var order1 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 2,
                UpdatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0))
            };

            Subject.AddOrUpdate(orders, order1);

            orders.Count.Should().Be(1);

            orders.Keys.Should().Contain(2);
            orders.Values.Should().Contain(order1);


        }




        [Fact]
        public void Should_add_a_second_order_if_it_not_exists()
        {
            var orders = new OrderDictionary();
            var order1 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now
            };
            var order2 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 10,
                UpdatedAt = DateTimeOffset.Now
            };

            Subject.AddOrUpdate(orders, order1);
            Subject.AddOrUpdate(orders, order2);

            orders.Count.Should().Be(2);

            orders.Keys.Should().Contain(2);
            orders.Keys.Should().Contain(10);

            orders.Values.Should().Contain(order1);
            orders.Values.Should().Contain(order2);
        }

        [Fact]
        public void Should_not_update_existing_order_if_it_is_older()
        {
            var orders = new OrderDictionary();
            var order1 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now
            };
            var order2 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(1))
            };

            Subject.AddOrUpdate(orders, order1);
            Subject.AddOrUpdate(orders, order2);

            orders.Count.Should().Be(1);

            orders.Keys.Should().Contain(2);

            orders.Values.Should().Contain(order1);
        }

        [Fact]
        public void Should_update_existing_order_if_it_is_newer()
        {
            var orders = new OrderDictionary();
            var order1 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now
            };
            var order2 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now.Add(TimeSpan.FromDays(1))
            };

            Subject.AddOrUpdate(orders, order1);
            Subject.AddOrUpdate(orders, order2);

            orders.Count.Should().Be(1);

            orders.Keys.Should().Contain(2);

            orders.Values.Should().Contain(order2);
        }

        [Fact]
        public void Should_add_the_Order_created_date_to_modified_date()
        {
            var orders = new OrderDictionary();
            var order1 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 2,
                UpdatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0))
            };

            Subject.AddOrUpdate(orders, order1);

            Subject.ModifiedDates.Count.Should().Be(1);
            var data = Subject.ModifiedDates.First();

            data.Year.Should().Be(2019);
            data.Month.Should().Be(9);
            data.Day.Should().Be(14);
            data.Hour.Should().Be(0);
        }

        [Fact]
        public void Should_only_have_on_modified_date_if_more_on_the_same_day_are_added()
        {
            var orders = new OrderDictionary();
            var order1 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 2,
                UpdatedAt = DateTimeOffset.Now
            };
            var order2 = new Order()
            {
                CreatedAt = new DateTimeOffset(2019, 09, 14, 12, 12, 12, TimeSpan.FromHours(0)),
                OrderNumber = 10,
                UpdatedAt = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(1))
            };


            Subject.AddOrUpdate(orders, order1);
            Subject.AddOrUpdate(orders, order2);
            Subject.ModifiedDates.Count.Should().Be(1);

            var data = Subject.ModifiedDates.First();

            data.Year.Should().Be(2019);
            data.Month.Should().Be(9);
            data.Day.Should().Be(14);
            data.Hour.Should().Be(0);
        }
    }
}
