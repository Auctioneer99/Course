namespace Gameplay
{
    public class SwitchGameStateAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SwitchGameState;

        public EGameState From { get; private set; }
        public EGameState To { get; private set; }

        public SwitchGameStateAction Initialize(EGameState from, EGameState to)
        {
            Initialize();
            From = from;
            To = to;
            return this;
        }

        protected override void ApplyImplementation()
        {
            GameController.StateMachine.TransitionTo(To);
        }

        protected override void AttributesFrom(Packet packet)
        {
            From = packet.ReadEGameState();
            To = packet.ReadEGameState();
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(From)
                .Write(To);
        }
    }
}
