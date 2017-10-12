using Doing.BDDExtensions;
using FluentAssertions;
using NUnit.Framework;
using static Doing.Functional.Option;

namespace Doing.Functional.Specs.Option
{
    public class MatchSpecs : FeatureSpecifications
    {
        public override void When() =>
            _result = _option.Match(
                () => "None",
                value => value.Value.ToString());

        public class When_the_former_option_was_a_some : MatchSpecs
        {
            public override void Given() =>
                _option = ReturnFrom(new Foo {Value = 111});
            
            [Test]
            public void Should_use_the_function_provided_for_the_some_case() =>
                _result.Should().Be("111");
            
            [Test]
            public void Should_be_able_to_get_the_contained_value_in_the_some() =>
                _result.Should().Be("111");
        }

        public class When_the_former_option_was_a_none : MatchSpecs
        {
            public override void Given() =>
                _option = ReturnFrom((Foo) null);
            
            [Test]
            public void Should_use_the_function_provided_for_the_none_case() =>
                _result.Should().Be("None");
        }
        
        
        Option<Foo> _option;
        string _result;
    }
}