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

        public override bool IsValid()
        {
            return true;
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            SwitchGameStateAction other = copyFrom as SwitchGameStateAction;

            From = other.From;
            To = other.To;
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
