using System.Text;

namespace Gameplay
{
    public struct PauseRequest : ISerializable
    {
        public int Id;
        public EPauseType Type;
        public long TimeRemaining;

        public PauseRequest(int id, EPauseType type, long duration)
        {
            Id = id;
            Type = type;
            TimeRemaining = duration;
        }

        public PauseRequest(Packet packet)
        {
            Id = packet.ReadInt();
            TimeRemaining = packet.ReadLong();
            Type = packet.ReadEPauseType();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Id)
                .Write(TimeRemaining)
                .Write(Type);

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[PauseRequest]");
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"TimeRemaining = {TimeRemaining}");
            sb.AppendLine($"Type = {Type}");
            return sb.ToString();
        }
    }
}
