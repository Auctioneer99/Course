using System;

namespace AbilitySystem.Variables
{
    public sealed class DynamicVariable<T> : IIndependentSource<T>
    {
        public string Name { get; private set; }

        public object RawValue => _valueFunc();

        public T Value => _valueFunc();

        private Func<T> _valueFunc;

        public DynamicVariable(string name, Func<T> valueFunc)
        {
            Name = name;
            _valueFunc = valueFunc;
        }

        public Type GetVarType()
        {
            return typeof(T);
        }
    }
}
