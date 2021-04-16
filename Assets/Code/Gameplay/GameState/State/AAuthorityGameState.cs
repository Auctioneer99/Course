namespace Gameplay
{
    public abstract class AAuthorityGameState : AGameState
    {
        protected bool _isReady = false;

        public AAuthorityGameState(GameController controller, EGameState state) : base(controller, state) { }

        protected override void SendWaitingForFinishedReport()
        {
           
        }

        protected override bool AreFinished()
        {
            return _isReady;
        }
    }
}
