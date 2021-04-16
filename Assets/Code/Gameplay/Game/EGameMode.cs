namespace Gameplay
{
    public enum EGameMode : byte
    {
        Undefined = 0,
        SinglePlayer = 1 << 0,
        Client = 1 << 1,
        Server = 1 << 2,
        Spectator = 1 << 3,
        Replay = 1 << 4,

        Authority = SinglePlayer | Server,
        Multiplayer = Client | Server | Spectator,
    }

    public static class EGameModeExtension
    {
        public static bool Contains(this EGameMode mode, EGameMode other)
        {
            return (mode & other) > 0;
        }
    }
}
