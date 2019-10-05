using System;
using System.Linq;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ShopifySharp;
using ShopInsights.Core.Models;
using Xunit;

namespace ShopInsights.Core.Tests.Models
{
    public class OrderStorageTests : WithSubject<OrderStorage>
    {
        public OrderStorageTests()
        {
            Services.AddOptions();
        }

        [Fact]
        public void Should_add_one_order_to_a_date()
        {
            var order = CreateOrder(1, 2019, 3, 2);

            Subject.AddOrders(new[] {order});

            var result = Subject.GetOrdersForDate(new DateTime(2019, 3, 2));

            result.Length.Should().Be(1);
        }

        [Fact]
        public void Should_add_two_orders_to_different_dates()
        {
            var orderA = CreateOrder(1, 2019, 3, 2);
            var orderB = CreateOrder(2, 2019, 3, 3);

            Subject.AddOrders(new[] {orderA, orderB});

            var result = Subject.GetOrdersForDate(new DateTime(2019, 3, 2));

            result[0].OrderNumber.Should().Be(1);
            result.Length.Should().Be(1);

            result = Subject.GetOrdersForDate(new DateTime(2019, 3, 3));

            result[0].OrderNumber.Should().Be(2);
            result.Length.Should().Be(1);

            result[0].UpdatedAt.HasValue.Should().BeFalse();

        }

        [Fact]
        public void Should_set_modified_date_on_update_and_add()
        {
            var orderA = CreateOrder(1, 2019, 3, 2);
            var orderB = CreateOrder(2, 2019, 3, 3);
            var orderB2 = CreateOrder(2, 2019, 3, 3);
            orderB2.UpdatedAt = new DateTimeOffset(2019, 3, 4, 10, 0, 0, _offset);

            Subject.AddOrders(new[] {orderA, orderB});
            Subject.AddOrders(new[] {orderB2});

            var result = Subject.GetOrdersForDate(new DateTime(2019, 3, 2));

            result[0].OrderNumber.Should().Be(1);
            result.Length.Should().Be(1);

            result = Subject.GetOrdersForDate(new DateTime(2019, 3, 3));

            result[0].OrderNumber.Should().Be(2);
            result.Length.Should().Be(1);

            result[0].UpdatedAt.HasValue.Should().BeTrue();

            var dates = Subject.ModifiedDates.ToArray();

            dates.Length.Should().Be(2);

            dates[0].Day.Should().Be(2);
            dates[1].Day.Should().Be(3);
        }

        [Fact]
        public void Should_have_modified_date_when_updateAt_is_newer()
        {
            var orderB = CreateOrder(2, 2019, 3, 3);
            orderB.UpdatedAt = new DateTimeOffset(2019, 3, 4, 10, 0, 0, _offset);

            var orderB2 = CreateOrder(2, 2019, 3, 3);
            orderB2.UpdatedAt = new DateTimeOffset(2019, 3, 5, 10, 0, 0, _offset);

            Subject.AddOrders(new[] {orderB});

            Subject.ResetModifiedDates();

            Subject.AddOrders(new[] {orderB2});

            var result = Subject.GetOrdersForDate(new DateTime(2019, 3, 3));

            result[0].Should().BeSameAs(orderB2);

            var dates = Subject.ModifiedDates.ToArray();

            dates.Length.Should().Be(1);

            dates[0].Day.Should().Be(3);
        }

        [Fact]
        public void Should_have_multiple_modified_dates_when_updated()
        {
            var orderA1 = CreateOrder(2, 2019, 3, 1);
            orderA1.UpdatedAt = new DateTimeOffset(2019, 3, 4, 10, 0, 0, _offset);

            var orderA2 = CreateOrder(2, 2019, 3, 1);
            orderA2.UpdatedAt = new DateTimeOffset(2019, 3, 5, 10, 0, 0, _offset);

            Subject.AddOrders(new[] {orderA1, orderA2});

            var orderB1 = CreateOrder(2, 2019, 3, 3);
            orderB1.UpdatedAt = new DateTimeOffset(2019, 3, 4, 10, 0, 0, _offset);

            var orderB2 = CreateOrder(2, 2019, 3, 3);
            orderB2.UpdatedAt = new DateTimeOffset(2019, 3, 5, 10, 0, 0, _offset);

            Subject.AddOrders(new[] {orderB1, orderB2});

            var dates = Subject.ModifiedDates.ToArray();

            dates.Length.Should().Be(2);
        }

        [Fact] public void Should_not_have_modified_date_when_updateAt_is_older()
        {
            var orderB = CreateOrder(2, 2019, 3, 3);
            orderB.UpdatedAt = new DateTimeOffset(2019, 3, 5, 10, 0, 0, _offset);

            var orderB2 = CreateOrder(2, 2019, 3, 3);
            orderB2.UpdatedAt = new DateTimeOffset(2019, 3, 3, 10, 0, 0, _offset);

            Subject.AddOrders(new[] {orderB});

            Subject.ResetModifiedDates();

            Subject.AddOrders(new[] {orderB2});

            var result = Subject.GetOrdersForDate(new DateTime(2019, 3, 3));

            result[0].Should().BeSameAs(orderB);

            var dates = Subject.ModifiedDates.ToArray();

            dates.Should().BeEmpty();

        }
        [Fact] public void Should_rest_modified_dates()
        {
            var orderB = CreateOrder(2, 2019, 3, 3);
            orderB.UpdatedAt = new DateTimeOffset(2019, 3, 4, 10, 0, 0, _offset);
            var orderB2 = CreateOrder(2, 2019, 3, 3);
            orderB2.UpdatedAt = new DateTimeOffset(2019, 3, 5, 10, 0, 0, _offset);

            Subject.AddOrders(new[] {orderB});
            Subject.AddOrders(new[] {orderB2});



            Subject.ModifiedDates.Should().NotBeEmpty();

            Subject.ResetModifiedDates();

            Subject.ModifiedDates.Should().BeEmpty();
        }

        private Order CreateOrder(int i, int year, int month, int day)
        {
            var order = new Order
            {
                OrderNumber = i,
                CreatedAt = new DateTimeOffset(year, month, day, 12, 12, 12, TimeSpan.FromHours(2))
            };

            return order;
        }

        private readonly TimeSpan _offset = TimeSpan.FromHours(2);
    }
}
