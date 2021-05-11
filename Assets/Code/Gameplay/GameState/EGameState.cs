namespace Gameplay
{
    public enum EGameState : byte
    {
        AwaitingPlayers,
        Init,
        Mulligan,
        ChoosePlayer,
        RoundStart,
        RoundEnd,
        TurnStart,
        TurnEnd,
        Turn,
        EndGame,
        PlayingCardStart,
        PlayingCardEnd,
        PlayingCard,

        NonSyncTurnStart,

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
