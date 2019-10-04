using System;
using System.Linq;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using ShopifySharp;
using ShopInsights.Core.Models;
using Xunit;

namespace ShopInsights.Core.Tests.Models
{
    public class OrderDictionaryTests : WithSubject<OrderDictionary>
    {
        [Fact]
        public void Should_add_an_order()
        {
            var order = new Order
            {
                OrderNumber = 1,
                CreatedAt = DateTimeOffset.Now
            };

            Subject.Add(order);

            Subject.Values.Count().Should().Be(1);

            Subject.Values.First().Should().BeSameAs(order);
        }

        [Fact]
        public void Should_throw_on_adding_with_missing_orderNumber()
        {
            var order = new Order
            {
                CreatedAt = DateTimeOffset.Now
            };

            Action action = () => Subject.Add(order);

            action.Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("*missing*");

            Subject.Values.Should().BeEmpty();

        }



        [Fact]
        public void Should_throw_on_adding_with_missing_createdAt()
        {
            var order = new Order
            {
                OrderNumber = 101,
            };

            Action action = () => Subject.Add(order);

            action.Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("*missing*101*");

            Subject.Values.Should().BeEmpty();
        }

        [Fact]
        public void Should_throw_on_update_with_missing_orderNumber()
        {
            var order = new Order
            {
                CreatedAt = DateTimeOffset.Now
            };

            Action action = () => Subject.Update(order);

            action.Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("*missing*");

            Subject.Values.Should().BeEmpty();

        }

        [Fact]
        public void Should_throw_on_update_with_non_existing_order_number()
        {
            var order = new Order
            {
                OrderNumber = 102,
                CreatedAt = DateTimeOffset.Now
            };

            Action action = () => Subject.Update(order);

            action.Should().ThrowExactly<InvalidOperationException>().WithMessage("*102*");

            Subject.Values.Should().BeEmpty();

        }
    }
}
