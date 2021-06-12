namespace Gameplay
{
    public class BattlefieldPlayerSettings
    {
        public EPlayer EPlayer;
        public int LeaderStartPosition;

        public bool ShouldSpawnLeader => LeaderStartPosition != -1;

        public BattlefieldPlayerSettings(EPlayer player, int leaderStart)
        {
            EPlayer = player;
            LeaderStartPosition = leaderStart;
        }
    }
}
