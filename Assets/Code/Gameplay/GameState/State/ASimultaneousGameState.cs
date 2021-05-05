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
                foreach(var player in GameController.PlayerManager.Players.Values)
                {
                    if (player != null)
                    {
                        RequestPlayerFinishedReport request = GameController.ActionFactory.Create<RequestPlayerFinishedReport>()
                            .Initialize(player.ConnectionId);
                        GameController.ActionDistributor.Add(request);
                    }
                }
            }
        }

        protected override bool AreFinished()
        {
            return GameController.PlayerManager.AreAllPlayers(EPlayerStatus.Finished);
        }
    }
}
