using System;

namespace Doing.Functional.Specs
{
    public static class Helpers
    {
        public static bool IsSome<T>(this Option<T> option) => 
            option.Match(() => false, _ => true);

        public static T GetValue<T>(this Option<T> option) => 
            option.Match(() => throw new Exception("There is no value for the None case"), value => value);
    }

    internal class Foo
    {
        public int Value { get; set; }
    }

    internal struct FooValue
    {
        
    }
}