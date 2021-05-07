using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class StateTimerDefinition : IDeserializable, ICloneable<StateTimerDefinition>
    {
        public EGameState EGameState { get; private set; }
        public TimerDefinition TimerDefinition { get; private set; }

        private StateTimerDefinition() { }

        public StateTimerDefinition(Packet packet)
        {
            FromPacket(packet);
        }

        public StateTimerDefinition(EGameState state, TimerDefinition definition)
        {
            EGameState = state;
            TimerDefinition = definition;
        }

        public void FromPacket(Packet packet)
        {
            EGameState = packet.ReadEGameState();
            TimerDefinition = new TimerDefinition(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EGameState)
                .Write(TimerDefinition);
        }

        public StateTimerDefinition Clone()
        {
            StateTimerDefinition stateDefinition = new StateTimerDefinition();
            stateDefinition.Copy(this);
            return stateDefinition;
        }

        public void Copy(StateTimerDefinition other)
        {
            EGameState = other.EGameState;
            TimerDefinition = other.TimerDefinition.Clone();
        }
    }
}
