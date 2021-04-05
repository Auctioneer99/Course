namespace Gameplay
{
    public class SetPlayerStatusAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SetPlayerStatus;

        public EPlayer Player { get; private set; }
        public EPlayerStatus Status { get; private set; }

        public SetPlayerStatusAction Initialize(EPlayer player, EPlayerStatus status)
        {
            Initialize();

            Player = player;
            Status = status;
            return this;
        }

        protected override void ApplyImplementation()
        {
            foreach (var player in GameController.PlayerManager.Players)
            {
                if (Player.Contains(player.EPlayer))
                {
                    player.EStatus = Status;
                }
            }
        }

        protected override void AttributesFrom(Packet packet)
        {
            Player = packet.ReadEPlayer();
            Status = packet.ReadEPlayerStatus();
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Player)
                .Write(Status);
        }
    }
}
