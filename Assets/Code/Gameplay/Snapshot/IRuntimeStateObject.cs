namespace Gameplay
{
    public interface IRuntimeStateObject<T>
    {
        void Copy(T other, GameController controller);
    }
}
