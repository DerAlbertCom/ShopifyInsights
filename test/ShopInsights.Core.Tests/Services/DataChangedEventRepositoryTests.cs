using System;
using FluentAssertions;
using ShopInsights.Services;
using ShopInsights.Services.Events;
using Xunit;
using Xunit.Sdk;

namespace ShopInsights.Core.Tests.Services
{
    public class DataChangedEventRepositoryTests
    {
        DataChangedEventRepository _subject;

        public DataChangedEventRepositoryTests()
        {
            var added = new DataChangedEventDictionary(DataChangedEvent.Added);
            var updated = new DataChangedEventDictionary(DataChangedEvent.Updated);
            var removed = new DataChangedEventDictionary(DataChangedEvent.Removed);
            _subject = new DataChangedEventRepository(added, updated, removed);

            added.Add(typeof(Data1), typeof(Data1Added));
            updated.Add(typeof(Data1), typeof(Data1Updated));
            removed.Add(typeof(Data1), typeof(Data1Removed));
            removed.Add(typeof(Data2), typeof(Data2Removed));
        }

        [Fact]
        public void Create_instance_of_added_event()
        {
            var data = new Data1();
            var instance = _subject.FindAddedEvent(data);


            ((Data1Added) instance).Data.Should().BeSameAs(data);
        }

        [Fact]
        public void Create_instance_of_updated_event()
        {
            var data = new Data1();
            var instance = _subject.FindUpdatedEvent(data);

            ((Data1Updated) instance).Data.Should().BeSameAs(data);
        }

        [Fact]
        public void Create_instance_of_removed_event()
        {
            var data = new Data2();
            var instance = _subject.FindRemovedEvent(data);

            ((Data2Removed) instance).Data.Should().BeSameAs(data);
        }

        [Fact]
        public void Should_not_create_instance_for_missing_data_type_for_added()
        {
            var data = new Data2();
            var result = _subject.FindAddedEvent(data);
            result.Should().BeNull();
        }

        [Fact]
        public void Should_not_create_instance_for_missing_data_type_for_updated()
        {
            var data = new Data2();
            var result =  _subject.FindUpdatedEvent(data);
            result.Should().BeNull();
        }

        [Fact]
        public void Should_not_create_instance_for_missing_data_type_for_removed()
        {
            var data = new DataChangedEventDictionary(DataChangedEvent.Added);
            var result = _subject.FindRemovedEvent(data);
            result.Should().BeNull();
        }
    }
}
