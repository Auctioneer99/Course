namespace Gameplay
{
    public abstract class APlayerAction : AAction
    {
        public int ConnectionId { get; set; }
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

        protected sealed override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            APlayerAction other = copyFrom as APlayerAction;

            ConnectionId = other.ConnectionId;
            PlayerCopyImplementation(other, controller);
        }

        protected sealed override void AttributesTo(Packet packet)
        {
            packet.Write(ConnectionId);
            PlayerAttributesTo(packet);
        }

        protected sealed override void AttributesFrom(Packet packet)
        {
            ConnectionId = packet.ReadInt();
            PlayerAttributesFrom(packet);
        }

        protected abstract void PlayerCopyImplementation(APlayerAction copyFrom, GameController controller);
        protected abstract void PlayerAttributesTo(Packet packet);
        protected abstract void PlayerAttributesFrom(Packet packet);
    }
}
