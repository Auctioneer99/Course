using System;

namespace Gameplay
{
    public class RequestPlayerFinishedReport : ARequest, ITargetedAction
    {
        public override EAction EAction => EAction.RequestPlayerFinished;
        public override ERequest ERequest => ERequest.PlayerFinished;

        public NetworkTarget Target => NetworkTarget.TargetPlayer;
        public EPlayer TargetPlayer => PlayerId;

        //hash?

        public RequestPlayerFinishedReport Initialize(EPlayer targetPlayer)
        {
            Initialize(targetPlayer, false);


            return this;
        }

        protected override void ApplyImplementation() { }

        public override bool IsFulfilled()
        {
            if(GameController.HasAuthority)
            {
                Player player = GameController.PlayerManager.GetPlayer(PlayerId);
                return player.EStatus == EPlayerStatus.Finished;
            }
            else
            {
                return GameController.IsFinished(true);
            }
        }

        public override void HandleFulfilled()
        {
            //Player player = GameController.PlayerManager.GetPlayer(PlayerId);
            //bool isLocalPlayer = player.IsLocalUser();

            if (GameController.HasAuthority)
            {
                ValidateState();
            }
            else
            {
                ReportPlayerStatusAction action = GameController.ActionFactory.Create<ReportPlayerStatusAction>()
                    .Initialize(PlayerId, NetworkActionNumber, EPlayerStatus.Finished);
                GameController.ActionDistributor.HandleAction(action);
            }
        }

        private void ValidateState()
        {

        }

        public override void HandleAborted() { }

        public override void HandleCancelled() { }

        public override void HandleExpired() { }
    }
}
