using System;

namespace AbilitySystem.Variables
{
    public sealed class DynamicVariable<T> : IIndependentSource<T>
    {
        public object RawValue => _valueFunc();

        public T Value => _valueFunc();

        private Func<T> _valueFunc;

        public DynamicVariable(Func<T> valueFunc)
        {
            _valueFunc = valueFunc;
        }

        public Type GetVarType()
        {
            return typeof(T);
        }
    }
}
