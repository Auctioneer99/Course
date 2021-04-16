namespace Gameplay
{
    public class TimerDefenition : IDeserializable
    {
        public EGameState EGameState;
        public int Duration;

        public TimerDefenition() { }

        public TimerDefenition(EGameState eGameState, int duration)
        {
            EGameState = eGameState;
            Duration = duration;
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
