using System;
using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using static Doing.Functional.Option;

namespace Doing.Functional.Specs.Option
{
    public class BindSpecs : FeatureSpecifications
    {
        public override void When() =>
            _result = _option.Bind(_func);

        public class FromSomeContext : BindSpecs
        {
            public override void Given() =>
                _option = ReturnFrom(new Foo {Value = 111});
            
            public class When_coming_from_a_some_the_func_also_will_produce_a_some : FromSomeContext
            {
                public override void Given() =>
                    _func = value => ReturnFrom(value.Value.ToString());
            
                [Test]
                public void Should_return_a_some() =>
                    _result.IsSome().Should().BeTrue();
            
                [Test]
                public void Should_use_the_function() =>
                    _result.GetValue().Should().Be("111");
            
                [Test]
                public void Should_be_able_to_get_the_contained_value() =>
                    _result.GetValue().Should().Be("111");
            }

            public class When_coming_from_a_some_the_func_will_produce_a_none : FromSomeContext
            {
                public override void Given() =>
                    _option = ReturnFrom((Foo) null);
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }
        }

        public class FromNoneContext : BindSpecs
        {
            public override void Given() =>
                _option = ReturnFrom((Foo) null);
            
            public class When_coming_from_a_none_the_func_could_produce_a_some : FromNoneContext
            {
                public override void Given() =>
                    _func = value => ReturnFrom(value.Value.ToString());
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }

            public class When_coming_from_a_none_the_func_also_could_produce_a_none : FromNoneContext
            {
                public override void Given() =>
                    _option = ReturnFrom((Foo) null);
            
                [Test]
                public void Should_return_a_none() =>
                    _result.IsSome().Should().BeFalse();
            }
        }
        
        
        Option<Foo> _option;
        Option<string> _result;
        Func<Foo, Option<string>> _func;
    }
}