namespace Gameplay
{
    public sealed class SetPlayerStatusAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SetPlayerStatus;

        public EPlayer Player { get; private set; }
        public EPlayerStatus Status { get; private set; }

        public override bool IsValid()
        {
            return true;
        }

        public SetPlayerStatusAction Initialize(EPlayer player, EPlayerStatus status)
        {
            Initialize();

            Player = player;
            Status = status;
            return this;
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            SetPlayerStatusAction other = copyFrom as SetPlayerStatusAction;

            Player = other.Player;
            Status = other.Status;
        }

        protected override void ApplyImplementation()
        {
            foreach (var player in GameController.PlayerManager.Players.Values)
            {
                if (player != null)
                {
                    if (Player.Contains(player.EPlayer))
                    {
                        player.EStatus = Status;
                    }
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
