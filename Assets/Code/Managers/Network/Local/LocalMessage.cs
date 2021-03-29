namespace Gameplay
{
    public class LocalMessage
    {
        public readonly GameController GameController;
        public readonly Packet Packet;
        public readonly NetworkTarget Target;
        public readonly EPlayer TargetPlayer;

        public LocalMessage(GameController controller, Packet packet, NetworkTarget target, EPlayer targetPlayer)
        {
            GameController = controller;
            Packet = packet;
            Target = target;
            TargetPlayer = targetPlayer;
        }
    }
}
