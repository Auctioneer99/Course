namespace Gameplay
{
    public class SetupPlayerAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SetupPlayer;

        public EPlayer Player { get; private set; }
        public PlayerInfo Info { get; private set; }

        public SetupPlayerAction Initialize(EPlayer player, PlayerInfo info)
        {
            Initialize();
            Player = player;
            Info = info;
            return this;
        }

        protected override void ApplyImplementation()
        {
            GameController.PlayerManager.GetPlayer(Player).Info = Info;
            if (GameController.HasAuthority == false)
            {
                GameController.GameInstance.Settings.GetPlayerSettings(Player).PlayerInfo = Info;
            }
        }

        protected override void AttributesFrom(Packet packet)
        {
            Player = packet.ReadEPlayer();
            Info.FromPacket(packet);
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Player)
                .Write(Info);
        }
    }
}
