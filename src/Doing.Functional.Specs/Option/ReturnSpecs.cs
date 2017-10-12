using System;
using System.Collections.Generic;
using System.Linq;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;

namespace Doing.Functional.Specs.Option
{
    public class ReturnSpecs : FeatureSpecifications
    {
        public class ReferenceTypeContext : ReturnSpecs
        {
            public override void When() =>
                _result = Functional.Option.ReturnFrom(_value);
    
            public class When_the_received_value_is_an_actual_value : ReferenceTypeContext
            {
                public override void Given() =>
                    _value = new Foo();
            
                [Test]
                public void Should_return_a_some() =>
                    _result.IsSome().Should().BeTrue();
    
                [Test]
                public void Should_return_a_some_with_the_former_value() =>
                    _result.GetValue().Should().Be(_value);
            }
    
            public class When_the_received_value_is_a_null : ReferenceTypeContext
            {
                public override void Given() =>
                    _value = null;
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }
        
        
            Foo _value;
            Option<Foo> _result;
        }

        public class When_the_received_value_is_from_a_value_type : ReturnSpecs
        {
            public override void Given() =>
                _value = new FooValue();
        
            public override void When() =>
                _result = Functional.Option.ReturnFrom(_value);
    
            [Test]
            public void Should_return_a_some() =>
                _result.IsSome().Should().BeTrue();

            [Test]
            public void Should_return_a_some_with_the_former_value() =>
                _result.GetValue().Should().Be(_value);
        
        
            FooValue _value;
            Option<FooValue> _result;
        }

        public class NullableValueTypeContext : ReturnSpecs
        {
            public override void When() =>
                _result = Functional.Option.ReturnFrom(_value);
    
            public class When_the_received_nullable_struct_has_a_value : NullableValueTypeContext
            {
                public override void Given() =>
                    _value = new FooValue();
            
                [Test]
                public void Should_return_a_some() =>
                    _result.IsSome().Should().BeTrue();
    
                [Test]
                public void Should_return_a_some_with_the_former_value() =>
                    _result.GetValue().Should().Be(_value.Value);
            }
    
            public class When_the_received_nullable_struct_has_no_a_value : NullableValueTypeContext
            {
                public override void Given() =>
                    _value = null;
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }
        
        
            FooValue? _value;
            Option<FooValue> _result;
        }
        
        public class EnumerableContext : ReturnSpecs
        {
            public override void When() =>
                _result = Functional.Option.ReturnFrom(_value);
    
            public class When_the_received_enumerable_has_elements : EnumerableContext
            {
                public override void Given() =>
                    _value = new []{ new Foo() };
            
                [Test]
                public void Should_return_a_some() =>
                    _result.IsSome().Should().BeTrue();
    
                [Test]
                public void Should_return_a_some_with_the_former_value() =>
                    _result.GetValue().Should().BeSameAs(_value);
            }
    
            public class When_the_received_enumerable_is_empty : EnumerableContext
            {
                public override void Given() =>
                    _value = Enumerable.Empty<Foo>();
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }
    
            public class When_the_received_enumerable_is_null : EnumerableContext
            {
                public override void Given() =>
                    _value = null;
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }
        
        
            IEnumerable<Foo> _value;
            Option<IEnumerable<Foo>> _result;
        }
        
        public class StringContext : ReturnSpecs
        {
            public override void When() =>
                _result = Functional.Option.ReturnFrom(_value);
    
            public class When_the_received_string_has_content : StringContext
            {
                public override void Given() =>
                    _value = "some content";
            
                [Test]
                public void Should_return_a_some() =>
                    _result.IsSome().Should().BeTrue();
    
                [Test]
                public void Should_return_a_some_with_the_former_value() =>
                    _result.GetValue().Should().BeSameAs(_value);
            }
    
            public class When_the_received_string_is_empty : StringContext
            {
                public override void Given() =>
                    _value = string.Empty;
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }
    
            public class When_the_received_string_is_null : StringContext
            {
                public override void Given() =>
                    _value = null;
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }
        
        
            string _value;
            Option<string> _result;
        }
    }
}