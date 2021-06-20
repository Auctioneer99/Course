using System.Text;

namespace Gameplay
{
    public struct StateTimerDefinition : ISerializable, IStateObjectCloneable<StateTimerDefinition>
    {
        public EGameState EGameState { get; private set; }
        public TimerDefinition TimerDefinition { get; private set; }

        public StateTimerDefinition(Packet packet)
        {
            EGameState = packet.ReadEGameState();
            TimerDefinition = new TimerDefinition(packet);
        }

        public StateTimerDefinition(EGameState state, TimerDefinition definition)
        {
            EGameState = state;
            TimerDefinition = definition;
        }

        public StateTimerDefinition Clone()
        {
            StateTimerDefinition definition = new StateTimerDefinition();
            definition.Copy(this);
            return this;
        }

        public void Copy(StateTimerDefinition other)
        {
            EGameState = other.EGameState;
            TimerDefinition.Copy(other.TimerDefinition);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EGameState)
                .Write(TimerDefinition);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[StateTimerDefinition]");
            sb.AppendLine($"GameState = {EGameState}");
            sb.Append(TimerDefinition.ToString());
            return sb.ToString();
        }
    }
}
