using System;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using ShopifySharp;
using ShopInsights.Core.Configuration;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services;
using Xunit;

namespace ShopInsights.Core.Tests.Services
{
    public class OrdersSelectorTests : WithFakes
    {
        private readonly OrdersSelector _subject;

        public OrdersSelectorTests()
        {
            var shopOptions = new ShopInstanceOptions();
            The<IOptions<ShopInstanceOptions>>().Value.Returns(shopOptions);
            The<IOptionsSnapshot<ShopInstanceOptions>>().Value.Returns(shopOptions);

            _subject = CreateSubjectUnderTest<OrdersSelector>();
        }

        [Fact]
        public void Should_give_me_order_of_2019_03_02()
        {
            var orders = _subject.SelectForDate(CreateTestDictionary(),
                new DateTime(2019, 3, 2, 5, 2, 3, DateTimeKind.Local));

            orders[0].OrderNumber.Should().Be(2);
            orders[1].OrderNumber.Should().Be(3);
        }

        [Fact]
        public void Should_give_the_order_of_2019_02_02_local()
        {
            var orders = _subject.SelectForDate(CreateTestDictionary(),
                new DateTime(2019, 2, 2, 0, 0, 0, DateTimeKind.Local));

            orders[0].OrderNumber.Should().Be(1);
        }

        [Fact]
        public void Should_give_the_order_of_2019_02_02_local_last_value()
        {
            var orders = _subject.SelectForDate(CreateTestDictionary(),
                new DateTime(2019, 2, 2, 23, 59, 59, DateTimeKind.Local));

            orders[0].OrderNumber.Should().Be(1);
        }

        [Fact]
        public void Should_give_the_order_of_2019_02_02_utc()
        {
            var orders = _subject.SelectForDate(CreateTestDictionary(),
                new DateTime(2019, 2, 2, 0, 0, 0, DateTimeKind.Utc));

            orders[0].OrderNumber.Should().Be(1);
        }

        [Fact]
        public void Should_not_give_the_order_of_2019_02_02_utc_last_value()
        {
            var orders = _subject.SelectForDate(CreateTestDictionary(),
                new DateTime(2019, 2, 2, 23, 59, 59, DateTimeKind.Utc));

            orders.Should().BeEmpty();
        }

        [Fact]
        public void Should_give_the_order_of_2019_02_01_utc_last_value()
        {
            var orders = _subject.SelectForDate(CreateTestDictionary(),
                new DateTime(2019, 2, 1, 23, 59, 59, DateTimeKind.Utc));

            orders[0].OrderNumber.Should().Be(1);
        }

        private OrderDictionary CreateTestDictionary()
        {
            var dictionary = new OrderDictionary();

            var offset = TimeZoneInfo.Local.GetUtcOffset(new DateTime(2019, 2, 2));

            var orders = new[]
            {
                new Order
                {
                    OrderNumber = 1,
                    CreatedAt = new DateTimeOffset(2019, 02, 02, 0, 0, 0, offset)
                },
                new Order
                {
                    OrderNumber = 3,
                    CreatedAt = new DateTimeOffset(2019, 03, 02, 3, 2, 3, offset)
                },
                new Order
                {
                    OrderNumber = 2,
                    CreatedAt = new DateTimeOffset(2019, 03, 02, 3, 2, 3, offset)
                },
                new Order
                {
                    OrderNumber = 4,
                    CreatedAt = new DateTimeOffset(2019, 03, 04, 1, 2, 3, offset)
                },
                new Order
                {
                    OrderNumber = 5,
                    CreatedAt = new DateTimeOffset(2019, 03, 04, 1, 2, 3, offset)
                },
                new Order
                {
                    OrderNumber = 6,
                    CreatedAt = new DateTimeOffset(2019, 03, 05, 1, 2, 3, offset)
                },
                new Order
                {
                    OrderNumber = 7,
                    CreatedAt = new DateTimeOffset(2019, 03, 06, 1, 2, 3, offset)
                },
                new Order
                {
                    OrderNumber = 8,
                    CreatedAt = new DateTimeOffset(2019, 04, 02, 1, 2, 3, offset)
                },
                new Order
                {
                    OrderNumber = 9,
                    CreatedAt = new DateTimeOffset(2019, 04, 02, 1, 2, 3, offset)
                },
            };

            var orderUpdater = new OrderUpdater(The<IOptionsSnapshot<ShopInstanceOptions>>());
            foreach (var order in orders)
            {
                orderUpdater.AddOrUpdate(dictionary, order);
            }

            return dictionary;
        }
    }
}
