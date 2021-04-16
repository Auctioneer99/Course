namespace Gameplay
{
    public class LocalConnector : AConnector
    {
        public GameController Controller { get; private set; }

        public LocalConnector(GameController controller)
        {
            Controller = controller;
        }

        public override void GetDecks()
        {

        }

        public override void HandleMessage(AConnector sender, AAction action)
        {
            if (CanHandleMessage(sender, action) == false)
            {
                return;
            }

            if (action is IPriorityAction)
            {
                Controller.ActionDistributor.HandleAction(action);
            }
            else
            {
                Controller.ActionDistributor.Add(action);
            }
        }

        private bool CanHandleMessage(AConnector connector, AAction action)
        {
            bool isPlayerAction = action is APlayerAction;
            bool isAuthorityAction = action is IAuthorityAction;
            //EPlayer senderRole = sender.Role;

            if (Controller.HasAuthority)
            {
                if (isPlayerAction)
                {
                    return true;
                }
            }
            else
            {
                if (isAuthorityAction)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
