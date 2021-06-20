namespace Gameplay
{
    public interface IStateObjectCloneable<T> : IStateObject<T>
    {
        T Clone();
    }
}
