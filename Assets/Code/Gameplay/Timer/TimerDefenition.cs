using UnityEngine;

namespace Gameplay
{
    public class TimerDefenition : IDeserializable, ICloneable<TimerDefenition>
    {
        public EGameState EGameState { get; private set; }
        public int Duration { get; private set; }

        public TimerDefenition() { }

        public TimerDefenition(Packet packet)
        {
            FromPacket(packet);
        }

        public TimerDefenition(EGameState eGameState, int duration)
        {
            EGameState = eGameState;
            Duration = duration;
        }

        public TimerDefenition Clone()
        {
            TimerDefenition def = new TimerDefenition();
            def.Copy(this);
            return def;
        }

        public void Copy(TimerDefenition other)
        {
            EGameState = other.EGameState;
            Duration = other.Duration;
        }

        public void FromPacket(Packet packet)
        {
            EGameState = packet.ReadEGameState();
            Duration = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EGameState)
                .Write(Duration);
        }
    }
}
