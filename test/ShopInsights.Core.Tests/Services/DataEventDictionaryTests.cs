using System;
using FluentAssertions;
using ShopInsights.Services;
using ShopInsights.Services.Events;
using Xunit;

namespace ShopInsights.Core.Tests.Services
{


    public class DataEventDictionaryTests
    {
        class MyClass
        {
        }

        class MyClass2
        {
        }

        class MyClass3
        {
        }

        class RemovedEvent2 : IDataRemovedEvent<MyClass2>
        {
            public RemovedEvent2(MyClass2 item)
            {

            }
        }

        class AddedEvent1 : IDataAddedEvent<MyClass>
        {
            public AddedEvent1(MyClass item)
            {

            }
        }

        class AddedEvent2 : IDataAddedEvent<MyClass>
        {
            public AddedEvent2(MyClass data)
            {

            }
        }

        class UpdatedEvent1 : IDataUpdatedEvent<MyClass>
        {
            public UpdatedEvent1(MyClass ding)
            {

            }
        }

        abstract class AbstractUpdatedEvent1 : IDataUpdatedEvent<MyClass>
        {
            public AbstractUpdatedEvent1(MyClass fing)
            {

            }
        }
        interface InterfaceUpdatedEvent1 : IDataUpdatedEvent<MyClass>
        {
        }

        class RemovedEvent1 : IDataRemovedEvent<MyClass>
        {
            public RemovedEvent1(MyClass a)
            {

            }
        }

        class RemovedEventNoCtor : IDataRemovedEvent<MyClass>
        {

        }

        class RemovedEventWrongCtor : IDataRemovedEvent<MyClass>
        {
            public RemovedEventWrongCtor(MyClass2 item)
            {

            }
        }

        class RemovedEventWrongCtor2 : IDataRemovedEvent<MyClass>
        {
            public RemovedEventWrongCtor2(MyClass2 item, MyClass c1)
            {

            }
        }



        [Fact]
        public void Should_add_an_event_with_add()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Added);
            dict.Add(typeof(MyClass), typeof(AddedEvent1));
            var result = dict[typeof(MyClass)];
            result.Should().Be(typeof(AddedEvent1));
        }

        [Fact]
        public void Should_only_add_eventType_for_one_dataType_with_add()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Added);
            dict.Add(typeof(MyClass), typeof(AddedEvent1));
            Action action = () => dict.Add(typeof(MyClass), typeof(AddedEvent2));

            action.Should().ThrowExactly<ArgumentException>().WithMessage("*same key*MyClass*");
        }

        [Fact]
        public void Should_add_an_event_with_indexer()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Added);
            dict[typeof(MyClass)] = typeof(AddedEvent1);
            var result = dict[typeof(MyClass)];
            result.Should().Be(typeof(AddedEvent1));
        }

        [Fact]
        public void Should_only_add_eventType_for_one_dataType_with_indexer()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Added);
            dict[typeof(MyClass)] = typeof(AddedEvent1);
            Action action = () => dict[typeof(MyClass)] = typeof(AddedEvent1);

            action.Should().ThrowExactly<ArgumentException>().WithMessage("*same key*MyClass*");
        }

        [Fact]
        public void Should_not_add_an_updated_event_to_an_added_dictionary()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Added);
            Action action = () => dict.Add(typeof(MyClass), typeof(UpdatedEvent1));
            action.Should().ThrowExactly<ArgumentException>()
                .WithMessage("*UpdatedEvent1*does not*IDataAddedEvent<MyClass>*");
        }

        [Fact]
        public void Should_not_add_an_added_event_to_an_update_dictionary()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Updated);
            Action action = () => dict.Add(typeof(MyClass), typeof(AddedEvent1));
            action.Should().ThrowExactly<ArgumentException>()
                .WithMessage("*AddedEvent1*does not*IDataUpdatedEvent<MyClass>*");
        }

        [Fact]
        public void Should_not_add_an_updated_event_to_an_remvoed_dictionary()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            Action action = () => dict.Add(typeof(MyClass), typeof(AddedEvent1));
            action.Should().ThrowExactly<ArgumentException>()
                .WithMessage("*AddedEvent1*does not*IDataRemovedEvent<MyClass>*");
        }

        [Fact]
        public void Should_containsKey_return_true_when_dataType_is_added()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass), typeof(RemovedEvent1));

            dict.ContainsKey(typeof(MyClass)).Should().BeTrue();
        }

        [Fact]
        public void Should_containsKey_return_false_when_dataType_is_not_added()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass2), typeof(RemovedEvent2));

            dict.ContainsKey(typeof(MyClass)).Should().BeFalse();
        }

        [Fact]
        public void Should_containsKey_return_true_for_all_dataTypes()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass2), typeof(RemovedEvent2));
            dict.Add(typeof(MyClass), typeof(RemovedEvent1));

            dict.ContainsKey(typeof(MyClass)).Should().BeTrue();
            dict.ContainsKey(typeof(MyClass2)).Should().BeTrue();
        }

        [Fact]
        public void Should_TryGetValue_return_true_for_existing_types()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass2), typeof(RemovedEvent2));
            dict.Add(typeof(MyClass), typeof(RemovedEvent1));

            dict.TryGetValue(typeof(MyClass), out var et1).Should().BeTrue();
            dict.TryGetValue(typeof(MyClass2), out var et2).Should().BeTrue();
        }

        [Fact]
        public void Should_TryGetValue_return_false_for_non_existing_types()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass2), typeof(RemovedEvent2));
            dict.Add(typeof(MyClass), typeof(RemovedEvent1));

            dict.TryGetValue(typeof(MyClass3), out var et1).Should().BeFalse();
        }

        [Fact]
        public void Should_TryGetValue_return_the_value_for_existing_types()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass2), typeof(RemovedEvent2));
            dict.Add(typeof(MyClass), typeof(RemovedEvent1));

            dict.TryGetValue(typeof(MyClass), out var et1);

            et1.Should().Be(typeof(RemovedEvent1));
        }

        [Fact]
        public void Should_TryGetValue_return_null_for_no_existing_types()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass2), typeof(RemovedEvent2));
            dict.Add(typeof(MyClass), typeof(RemovedEvent1));

            dict.TryGetValue(typeof(MyClass3), out var et1);

            et1.Should().BeNull();
        }

        [Fact]
        public void Should_Remove_removes_an_existing_value()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass2), typeof(RemovedEvent2));
            dict.Add(typeof(MyClass), typeof(RemovedEvent1));

            dict.Remove(typeof(MyClass)).Should().BeTrue();

            dict.ContainsKey(typeof(MyClass)).Should().BeFalse();
        }

        [Fact]
        public void Should_Remove_not_removes_an_not_existing_value()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);
            dict.Add(typeof(MyClass2), typeof(RemovedEvent2));
            dict.Add(typeof(MyClass), typeof(RemovedEvent1));

            dict.Remove(typeof(MyClass3)).Should().BeFalse();

            dict.ContainsKey(typeof(MyClass)).Should().BeTrue();
            dict.ContainsKey(typeof(MyClass2)).Should().BeTrue();
        }

        [Fact]
        public void Should_not_add_an_abstract_type()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Updated);

            Action action = ()=> dict.Add(typeof(MyClass), typeof(AbstractUpdatedEvent1));

            action.Should().ThrowExactly<ArgumentException>().WithMessage("*AbstractUpdatedEvent1*must be*concrete*");
        }

        [Fact]
        public void Should_not_add_an_interface_type()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Updated);

            Action action = ()=> dict.Add(typeof(MyClass), typeof(InterfaceUpdatedEvent1));

            action.Should().ThrowExactly<ArgumentException>().WithMessage("*InterfaceUpdatedEvent1*must be*concrete*");
        }

        [Fact]
        public void Should_not_add_with_no_ctor()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);

            Action action = ()=> dict.Add(typeof(MyClass), typeof(RemovedEventNoCtor));

            action.Should().ThrowExactly<ArgumentException>().WithMessage("*RemovedEventNoCtor*has no*accepts*MyClass*");
        }
        [Fact]
        public void Should_not_add_with_wrong_type_in_ctor()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);

            Action action = ()=> dict.Add(typeof(MyClass), typeof(RemovedEventWrongCtor));

            action.Should().ThrowExactly<ArgumentException>().WithMessage("*RemovedEventWrongCtor*has no*accepts*MyClass*");
        }

        [Fact]
        public void Should_not_add_with_more_then_one_parameter_in_ctor()
        {
            var dict = new DataChangedEventDictionary(DataChangedEvent.Removed);

            Action action = ()=> dict.Add(typeof(MyClass), typeof(RemovedEventWrongCtor2));

            action.Should().ThrowExactly<ArgumentException>().WithMessage("*RemovedEventWrongCtor2*has no*accepts*MyClass*");
        }
    }
}
