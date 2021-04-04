namespace Gameplay
{
    public abstract class APlayerAction : AAction
    {
        public EPlayer EPlayer { get; private set; }

        public override bool IsValid()
        {
            if (this is IUserAction action)
            {
                Player player = GameController.PlayerManager.GetPlayer(EPlayer);
                return action.ValidWhenBlocked || EPlayerStatus.Communicable.Contains(player.EStatus);
            }
            return true;
        }

        protected void Initialize(EPlayer player)
        {
            Initialize();
            EPlayer = player;
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(EPlayer);
        }

        protected override void AttributesFrom(Packet packet)
        {
            EPlayer = packet.ReadEPlayer();
        }
    }
}
