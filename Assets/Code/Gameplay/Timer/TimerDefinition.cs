using System.Text;

namespace Gameplay
{
    public class TimerDefinition : IDeserializable, ICloneable<TimerDefinition>
    {
        public int Duration { get; private set; }

        private TimerDefinition() { }

        public TimerDefinition(Packet packet)
        {
            FromPacket(packet);
        }

        public TimerDefinition(int duration)
        {
            Duration = duration;
        }

        public TimerDefinition Clone()
        {
            TimerDefinition def = new TimerDefinition();
            def.Copy(this);
            return def;
        }

        public void Copy(TimerDefinition other)
        {
            Duration = other.Duration;
        }

        public void FromPacket(Packet packet)
        {
            Duration = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Duration);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[TimerDefinition]");
            sb.AppendLine($"Duration = {Duration}");
            return sb.ToString();
        }
    }
}
