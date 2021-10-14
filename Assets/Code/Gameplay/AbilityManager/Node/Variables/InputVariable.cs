using System;

namespace AbilitySystem.Variables
{
    public abstract class InputVariable: IVarSource
    {
        public string Name { get; private set; }

        public abstract IIndependentSource RawValueSource { get; set; }

        public bool HasSource => RawValueSource != null;

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

        public InputVariable(string name) : base(name)
        {

        }

        public InputVariable(string name, IIndependentSource<T> variableSource) : base(name)
        {
            ValueSource = variableSource;
        }

        public override Type GetVarType()
        {
            return ValueSource.GetVarType();
        }
    }
}
