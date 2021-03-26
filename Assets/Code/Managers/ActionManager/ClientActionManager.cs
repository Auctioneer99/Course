namespace Gameplay
{
    public sealed class ClientActionManager : ActionManager
    {
        public ClientActionManager(GameController controller, IGateway gateway) : base(controller, gateway) { }

        protected override bool CanSend(AAction action)
        {
            bool isAuthoritativeAction = action is IAuthorityAction;
            bool isClientAction = action is IUserAction;

            if ((isClientAction || isAuthoritativeAction) == false)
            {
                throw new System.Exception("Unauthrized action send");
            }
            if (!isAuthoritativeAction && isClientAction)
            {
                return true;
            }
            else
            {
                throw new System.Exception("cant send this action " + action.EAction);
            }
            //return !isAuthoritativeAction && isClientAction;
        }

        protected override void GetDestination(AAction action, out NetworkTarget target, out EPlayer targetPlayer)
        {
            target = NetworkTarget.Server;
            targetPlayer = EPlayer.Undefined;
        }
    }
}
