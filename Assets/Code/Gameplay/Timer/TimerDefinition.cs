using System.Text;

namespace Gameplay
{
    public struct TimerDefinition : ISerializable, IStateObject<TimerDefinition>
    {
        public int Duration { get; private set; }

        public int AuthorityBuffer { get; private set; }

        public TimerDefinition(Packet packet)
        {
            Duration = packet.ReadInt();
            AuthorityBuffer = packet.ReadInt();
        }

        public TimerDefinition(int duration, int buffer)
        {
            Duration = duration;
            AuthorityBuffer = buffer;
        }

        public void Copy(TimerDefinition other)
        {
            Duration = other.Duration;
            AuthorityBuffer = other.AuthorityBuffer;
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
