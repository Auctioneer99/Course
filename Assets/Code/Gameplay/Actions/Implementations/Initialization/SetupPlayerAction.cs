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
            Info = other.Info.Clone();
        }

        protected override void ApplyImplementation()
        {
            Player player = GameController.PlayerManager.SetupPlayer(Player, Connection);
            player.Info = Info;


            /*
            player.Info = Info;

            if (GameController.HasAuthority)
            {
                GameController.GameInstance.Settings.PlayersSettings[Player] = new PlayerSettings(Player, Info);
                SetPlayerStatusAction action = GameController.ActionFactory.Create<SetPlayerStatusAction>()
                    .Initialize(Player, EPlayerStatus.Finished);
                //RequestPlayerFinishedReport request = GameController.ActionFactory.Create<RequestPlayerFinishedReport>()
                //        .Initialize(Connection);
                GameController.ActionDistributor.Add(action, true);
                //GameController.GameInstance.Settings.GetPlayerSettings(Player).PlayerInfo = Info;
            }*/

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
