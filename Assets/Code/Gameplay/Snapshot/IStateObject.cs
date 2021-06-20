namespace Gameplay
{
    public interface IStateObject<T>
    {
        void Copy(T other);
    }
}
