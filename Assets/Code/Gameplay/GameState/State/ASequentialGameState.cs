namespace Gameplay
{
    public abstract class ASequentialGameState : AGameState
    {
        public Player CurrentPlayer => GameController.PlayerManager.CurrentPlayer;
        public EPlayer CurrentEPlayer => GameController.PlayerManager.CurrentPlayerId;

        public ASequentialGameState(GameController controller, EGameState state) : base(controller, state)
        {

        }

        protected override void SendWaitingForFinishedReport()
        {
            if (GameController.HasAuthority)
            {
                RequestPlayerFinishedReport request = GameController.ActionFactory.Create<RequestPlayerFinishedReport>()
                    .Initialize(CurrentEPlayer);
                GameController.ActionDistributor.Add(request);
            }
        }

        protected override bool AreFinished()
        {
            return CurrentPlayer.EStatus == EPlayerStatus.Finished;
        }
    }
}
