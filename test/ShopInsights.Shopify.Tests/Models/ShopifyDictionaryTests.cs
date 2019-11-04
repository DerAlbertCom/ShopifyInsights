using System;
using System.Linq;
using FluentAssertions;
using ShopifySharp;
using ShopInsights.Shopify.Models;
using Xunit;

namespace ShopInsights.Shopify.Tests.Models
{
    public class ShopifyDictionaryTests
    {
        ShopifyDictionary<long,Order> Subject = new ShopifyDictionary<long, Order>(o=> o.Id);

        [Fact]
        public void Should_add_an_order()
        {
            var order = new Order
            {
                Id = 1,
                CreatedAt = DateTimeOffset.Now
            };

            Subject.Add(order);

            Subject.Values.Count().Should().Be(1);

            Subject.Values.First().Should().BeSameAs(order);
        }

        [Fact]
        public void Should_throw_on_adding_with_missing_key()
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
        public void Should_throw_on_update_with_missing_key()
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
        public void Should_throw_on_update_with_non_existing_key()
        {
            var order = new Order
            {
                Id = 102,
                CreatedAt = DateTimeOffset.Now
            };

            Action action = () => Subject.Update(order);

            action.Should().ThrowExactly<InvalidOperationException>().WithMessage("*102*");

            Subject.Values.Should().BeEmpty();

        }
    }
}
