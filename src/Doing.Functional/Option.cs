using System;
using System.Collections.Generic;
using System.Linq;

namespace Doing.Functional
{
    public struct Option<T>
    {
        private readonly T _value;
        private readonly bool _isSome;

        internal Option(T value)
        {
            _value = value;
            _isSome = true;
        }

        public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> func) =>
            _isSome ? func(_value) : new Option<TResult>();

        public TResult Match<TResult>(Func<TResult> onNoneGet, Func<T, TResult> onSomeGet) =>
            _isSome ? onSomeGet(_value) : onNoneGet();
    }

    public static class Option
    {
        public static Option<T> ReturnFrom<T>(T rawValue) =>
            rawValue != null ? new Option<T>(rawValue) : new Option<T>();

        public static Option<T> ReturnFrom<T>(T? rawValue) where T : struct =>
            rawValue.HasValue ? new Option<T>(rawValue.Value) : new Option<T>();
        
        public static Option<IEnumerable<T>> ReturnFrom<T>(IEnumerable<T> enumerable) =>
            enumerable != null && enumerable.GetEnumerator().MoveNext() ? new Option<IEnumerable<T>>(enumerable) : new Option<IEnumerable<T>>();
        
        public static Option<string> ReturnFrom(string str) =>
            string.IsNullOrEmpty(str) ? new Option<string>() : new Option<string>(str);
    }
}