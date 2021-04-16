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

        protected override void ApplyImplementation()
        {
            StateTimer timer = GameController.StateMachine.GetState(EGameState).Timer;
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
