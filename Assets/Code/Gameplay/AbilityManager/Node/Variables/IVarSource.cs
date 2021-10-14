using System;

namespace AbilitySystem.Variables
{
    public interface IVarSource
    {
        string Name { get; }

        object RawValue { get; }

        Type GetVarType();
    }

    public interface IVarSource<T> : IVarSource
    {
        T Value { get; }
    }
}
