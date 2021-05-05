namespace Gameplay
{
    public enum ETimerState : byte
    {
        Stopped,
        Running,
        Elapsed
    }

    public static class ETimerStateExtension
    {
        public static Packet Write(this Packet packet, ETimerState state)
        {
            packet.Write((byte)state);
            return packet;
        }

        public static ETimerState ReadETimerState(this Packet packet)
        {
            return (ETimerState)packet.ReadByte();
        }
    }
}
