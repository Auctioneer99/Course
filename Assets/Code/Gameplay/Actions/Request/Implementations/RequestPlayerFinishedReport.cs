using System;

namespace Gameplay
{
    public class RequestPlayerFinishedReport : ARequest, ITargetedAction
    {
        public override EAction EAction => EAction.RequestPlayerFinished;
        public override ERequest ERequest => ERequest.PlayerFinished;

        public NetworkTarget Target => NetworkTarget.TargetPlayer;

        //hash?

        public RequestPlayerFinishedReport Initialize(int connection)
        {
            Initialize(connection, false);

            return this;
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override void CopyRequestImplementation(ARequest req, GameController controller)
        {

        }

        protected override void ApplyImplementation() { }

        public override bool IsFulfilled()
        {
            if(GameController.HasAuthority)
            {
                Player player = GameController.PlayerManager.GetPlayer(Connection);
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
                    .Initialize(NetworkActionNumber, EPlayerStatus.Finished);
                GameController.ActionDistributor.HandleAction(action);
            }
        }

        private void ValidateState()
        {

        }

        public override void HandleAborted() { }

        public override void HandleCancelled() { }

        public override void HandleExpired() { }

        protected override void RequestAttributesFrom(Packet packet)
        {
            
        }

        protected override void RequestAttributesTo(Packet packet)
        {
            
        }
    }
}
