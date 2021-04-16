namespace Gameplay
{
    public abstract class ASimultaneousGameState : AGameState
    {
        public ASimultaneousGameState(GameController controller, EGameState state) : base(controller, state)
        {

        }

        protected override void SendWaitingForFinishedReport()
        {
            if (GameController.HasAuthority)
            {
                RequestPlayerFinishedReport request = GameController.ActionFactory.Create<RequestPlayerFinishedReport>()
                    .Initialize(EPlayer.Players);
                GameController.ActionDistributor.Add(request);
            }
        }

        protected override bool AreFinished()
        {
            return GameController.PlayerManager.AreAllPlayers(EPlayerStatus.Finished);
        }
    }
}
