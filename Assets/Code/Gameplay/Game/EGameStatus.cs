namespace Gameplay
{
    public enum EGameStatus : byte
    {
        Uninit,
        Running,
        Ended
    }

    public static class EGameStatusExtension
    {
        public static Packet Write(this Packet packet, EGameStatus status)
        {
            return packet.Write((byte)status);
        }

        public static EGameStatus ReadEGameStatus(this Packet packet)
        {
            return (EGameStatus)packet.ReadByte();
        }
    }
}
