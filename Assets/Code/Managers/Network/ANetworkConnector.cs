namespace Gameplay
{
    public abstract class ANetworkConnector
    {
        public GameController GameController { get; private set; }

        public Player LocalUser { get { return null; } }
        public EPlayer LocalUserId { get { return GameController.PlayerManager.LocalUserId; } }

        public ANetworkConnector(GameController controller)
        {

        }

        public abstract void Send(Packet packet, NetworkTarget target, EPlayer targetPlayer);

        public void HandlePacket(Packet packet, EPlayer sender)
        {
            AAction action = packet.ToAction(GameController);
            HandleAction(action, sender);
        }

        public void HandleAction(AAction action, EPlayer sender)
        {
            bool valid = ValidateAction(action, sender);
            if (valid == false)
            {
                return;
            }


            GameController.ActionManager.AddAction(action);
        }

        public bool ValidateAction(AAction action, EPlayer sender)
        {
            return true;
        }
    }
}
