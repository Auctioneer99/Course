namespace AbilitySystem.Variables
{
    public interface IIndependentSource : IVarSource
    {
    }

    public interface IIndependentSource<T> : IIndependentSource, IVarSource<T>
    {
    }
}
