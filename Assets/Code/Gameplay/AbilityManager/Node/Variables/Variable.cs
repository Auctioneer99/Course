using System;

namespace AbilitySystem.Variables
{
    public abstract class Variable : IIndependentSource
    {
        public string Name { get; set; }

        public Variable(string name)
        {
            Name = name;
        }

        public abstract object RawValue { get; }

        public abstract Type GetVarType();
    }


    public sealed class Variable<T> : Variable, IIndependentSource<T>
    {
        public T Value { get; set; }
        public override object RawValue => Value;

        public Variable(string name, T value) : base(name)
        {
            Value = value;
        }

        public override Type GetVarType()
        {
            return typeof(T);
        }
    }
}
