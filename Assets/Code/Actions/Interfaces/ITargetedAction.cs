namespace Gameplay
{
    public interface ITargetedAction
    {
        NetworkTarget Target { get; }
        EPlayer TargetPlayer { get; }
    }
}
