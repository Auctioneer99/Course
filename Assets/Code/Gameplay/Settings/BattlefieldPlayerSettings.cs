namespace Gameplay
{
    public class BattlefieldPlayerSettings
    {
        public EPlayer EPlayer;
        public TileDefinition[] Influence;
        public int LeaderStartPosition;

        public bool ShouldSpawnLeader => LeaderStartPosition != -1;

        public BattlefieldPlayerSettings(EPlayer player, TileDefinition[] influence, int leaderStart)
        {
            EPlayer = player;
            Influence = influence;
            LeaderStartPosition = leaderStart;
        }
    }
}
