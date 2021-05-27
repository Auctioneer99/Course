namespace Gameplay
{
    public enum ELocation : short
    {
        Undefined = 0,
        Hand = 1 << 0,
        Deck = 1 << 1,
        Graveyard = 1 << 2,
        Mulligan = 1 << 3,
        Field = 1 << 4,



    }

    public static class ELocationExtension
    {
        public static ELocation ReadELocation(this Packet packet)
        {
            return (ELocation)packet.ReadShort();
        }

        public static Packet Write(this Packet packet, ELocation location)
        {
            packet.Write((short)location);
            return packet;
        }
    }
}
