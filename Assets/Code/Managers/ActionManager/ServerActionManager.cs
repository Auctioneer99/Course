namespace Gameplay
{
    public sealed class ServerActionManager : ActionManager
    {
        public ServerActionManager(GameController controller, IGateway gateway) : base(controller, gateway) { }

        protected override bool CanSend(AAction action)
        {
            bool isClientAction = action is IUserAction;
            bool isServerSide = action is IAuthoritySideAction;
            bool canSend = (isClientAction || isServerSide) == false;

            if (canSend)
            {
                return (action is ITargetedAction targetedAction
                    && targetedAction.Target.Contains(NetworkTarget.TargetPlayer)
                    && targetedAction.TargetPlayer == EPlayer.Undefined) == false;
            }

            throw new System.Exception("cant send this action " + action.EAction);
            //return false;
        }

        protected override void GetDestination(AAction action, out NetworkTarget target, out EPlayer targetPlayer)
        {
            if (action is ITargetedAction targetedAction)
            {
                target = targetedAction.Target;
                targetPlayer = targetedAction.TargetPlayer;
            }
            else
            {
                target = NetworkTarget.AllPlayers;
                targetPlayer = EPlayer.Undefined;
            }
        }
    }
}
