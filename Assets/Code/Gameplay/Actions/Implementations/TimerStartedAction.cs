namespace Gameplay
{
    public class TimerStartedAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.TimerStarted;

        public EGameState EGameState { get; private set; }

        public TimerStartedAction Initialize(EGameState state)
        {
            Initialize();
            EGameState = state;
            return this;
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            EGameState = (copyFrom as TimerStartedAction).EGameState;
        }

        protected override void ApplyImplementation()
        {
            Timer timer = GameController.StateMachine.GetState(EGameState).Timer;
            timer.Start();
        }

        protected override void AttributesFrom(Packet packet)
        {
            EGameState = packet.ReadEGameState();
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(EGameState);
        }
    }
}
