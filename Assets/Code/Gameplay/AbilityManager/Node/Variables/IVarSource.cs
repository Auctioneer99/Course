using System;

namespace AbilitySystem.Variables
{
    public interface IVarSource
    {
        object RawValue { get; }

        Type GetVarType();
    }

    public interface IVarSource<T> : IVarSource
    {
        T Value { get; }
    }
}
