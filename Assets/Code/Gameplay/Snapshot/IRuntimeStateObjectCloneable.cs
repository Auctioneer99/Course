namespace Gameplay
{
    public interface IRuntimeStateObjectCloneable<T> : IRuntimeStateObject<T>
    {
        T Clone(GameController controller);
    }
}
