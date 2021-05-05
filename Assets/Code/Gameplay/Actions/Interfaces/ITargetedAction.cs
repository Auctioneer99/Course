namespace Gameplay
{
    public interface ITargetedAction : IAuthorityAction
    {
        int Connection { get; }
        NetworkTarget Target { get; }
        //EPlayer TargetPlayer { get; }
    }
}
