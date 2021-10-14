using System;

namespace AbilitySystem.Variables
{
    public abstract class InputVariable: IVarSource
    {
        public string Name { get; private set; }

        public abstract IIndependentSource RawValueSource { get; set; }

        public object RawValue => RawValueSource.RawValue;

        public InputVariable(string name)
        {
            Name = name;
        }

        public abstract Type GetVarType();
    }

    public sealed class InputVariable<T> : InputVariable, IVarSource<T>
    {
        public override IIndependentSource RawValueSource
        {
            get
            {
                return ValueSource;
            }
            set
            {
                ValueSource = value as IIndependentSource<T>;
            }
        }

        public IIndependentSource<T> ValueSource { get; private set; }

        public T Value => ValueSource.Value;

        public InputVariable(string name, IIndependentSource<T> variableSource) : base(name)
        {
            ValueSource = variableSource;
        }

        public bool CanConnectTo<Y>(IVarSource<Y> other)
        {
            return GetVarType().IsAssignableFrom(other.GetVarType());
        }

        public void Disconnect(IVarSource<T> source)
        {
            if (source == ValueSource)
            {
                ValueSource = null;
            }
        }

        public override Type GetVarType()
        {
            return ValueSource.GetVarType();
        }
    }
}
