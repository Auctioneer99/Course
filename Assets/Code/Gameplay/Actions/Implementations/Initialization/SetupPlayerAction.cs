namespace Gameplay
{
    public class SetupPlayerAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SetupPlayer;

        public EPlayer Player { get; private set; }
        public int Connection { get; private set; }
        public PlayerInfo Info { get; private set; }

        public SetupPlayerAction Initialize(EPlayer player, int connection, PlayerInfo info)
        {
            Initialize();
            Player = player;
            Connection = connection;
            Info = info;
            return this;
        }

        public override bool IsValid()
        {
            return GameController.StateMachine.ECurrentState == EGameState.AwaitingPlayers;
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            SetupPlayerAction other = copyFrom as SetupPlayerAction;

            Player = other.Player;
            Connection = other.Connection;
            Info = other.Info.Clone();
        }

        protected override void ApplyImplementation()
        {
            Player player = GameController.PlayerManager.SetupPlayer(Player, Info, Connection);

            GameController.EventManager.OnPlayerSetup.Invoke(player);
        }

        protected override void AttributesFrom(Packet packet)
        {
            Player = packet.ReadEPlayer();
            Connection = packet.ReadInt();
            Info.FromPacket(packet);
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Player)
                .Write(Connection)
                .Write(Info);
        }
    }
}
