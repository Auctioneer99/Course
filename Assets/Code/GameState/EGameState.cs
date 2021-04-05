namespace Gameplay
{
    public enum EGameState : byte
    {
        Init = 0,
        Mulligan = 1,
        ChoosePlayer = 2,
        RoundStart = 3,
        RoundEnd = 4,
        TurnStart = 5,
        TurnEnd = 6,
        Turn = 7,
        End = 8,
        PlayingCardStart = 9,
        PlayingCardEnd = 10,
        PlayingCard = 11,

        NonSyncTurnStart = 12,

        Invalid
    }

    public static class EGameStateExtension
    {
        public static Packet Write(this Packet packet, EGameState state)
        {
            packet.Write((byte)state);
            return packet;
        }

        public static EGameState ReadEGameState(this Packet packet)
        {
            return (EGameState)packet.ReadByte();
        }
    }
}
