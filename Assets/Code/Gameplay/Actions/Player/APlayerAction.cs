namespace Gameplay
{
    public abstract class APlayerAction : AAction
    {
        public int ConnectionId { get; private set; }
        public EPlayer EPlayer => GameController.Network.Manager.GetPlayerTypeFromConnectionId(ConnectionId);

        public override bool IsValid()
        {
            if (this is IUserAction action)
            {
                Player player = GameController.PlayerManager.GetPlayer(EPlayer);
                return action.ValidWhenBlocked || EPlayerStatus.Communicable.Contains(player.EStatus);
            }
            return true;
        }

        protected void Initialize(int connectionId)
        {
            Initialize();
            ConnectionId = connectionId;
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(ConnectionId);
        }

        protected override void AttributesFrom(Packet packet)
        {
            ConnectionId = packet.ReadInt();
        }
    }
}
