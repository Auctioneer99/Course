namespace Gameplay
{
    public abstract class ANetworkConnector
    {
        public GameController GameController { get; private set; }

        public Player LocalUser { get { return null; } }
        public EPlayer LocalUserId { get { return GameController.PlayerManager.LocalUserId; } }

        public ANetworkConnector(GameController controller)
        {
            GameController = controller;
        }

        public abstract void Send(Packet packet, NetworkTarget target, EPlayer targetPlayer);

        public virtual void StartHandlingMessages() { }

        public void HandlePacket(Packet packet, EPlayer sender)
        {
            APlayerAction action = packet.ToAction(GameController);
            HandleAction(action, sender);
        }

        private void HandleAction(APlayerAction action, EPlayer sender)
        {
            bool valid = ValidateAction(action, sender);
            if (valid == false)
            {
                return;
            }


            GameController.ActionDistributor.Add(action);
        }

        public bool ValidateAction(APlayerAction action, EPlayer sender)
        {
            if (action is IUserAction userAction)
            {
                if (userAction.Sender != sender)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
