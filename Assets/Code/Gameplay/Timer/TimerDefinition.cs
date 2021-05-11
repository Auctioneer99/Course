using System.Text;

namespace Gameplay
{
    public class TimerDefinition : IDeserializable, ICloneable<TimerDefinition>
    {
        public int Duration { get; private set; }

        public int AuthorityBuffer { get; private set; }

        private TimerDefinition() { }

        public TimerDefinition(Packet packet)
        {
            FromPacket(packet);
        }

        public TimerDefinition(int duration, int buffer)
        {
            Duration = duration;
            AuthorityBuffer = buffer;
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
            AuthorityBuffer = other.AuthorityBuffer;
        }

        public void FromPacket(Packet packet)
        {
            Duration = packet.ReadInt();
            AuthorityBuffer = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Duration)
                .Write(AuthorityBuffer);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[TimerDefinition]");
            sb.AppendLine($"Duration = {Duration}");
            sb.AppendLine($"Buffer = {AuthorityBuffer}");
            return sb.ToString();
        }
    }
}
