using System;

namespace AbilitySystem.Variables
{
    public sealed class Variable<T> : IIndependentSource<T>
    {
        public T Value => _value;
        public object RawValue => _value;

        private T _value;

        public Variable(T value)
        {
            _value = value;
        }

        public Type GetVarType()
        {
            return typeof(T);
        }
    }
}
