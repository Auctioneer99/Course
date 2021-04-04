using System;

namespace Gameplay
{
    public class PingAction : APlayerAction, IUserAction, IPriorityAction
    {
        public override EAction EAction => EAction.Ping;

        public bool ValidWhenBlocked => true;

        public int LastNetworkPacketNumber { get; private set; }

        public PingAction Initialize(EPlayer player, int lastPacketNumber)
        {
            Initialize(player);

            LastNetworkPacketNumber = lastPacketNumber;
            return this;
        }

        protected override void ApplyImplementation()
        {
            Player player = GameController.PlayerManager.GetPlayer(EPlayer);
            player.PingStatus.Ping(LastNetworkPacketNumber);
        }

        protected override void AttributesFrom(Packet packet)
        {
            base.AttributesFrom(packet);
            LastNetworkPacketNumber = packet.ReadInt();
        }

        protected override void AttributesTo(Packet packet)
        {
            base.AttributesTo(packet);
            packet.Write(LastNetworkPacketNumber);
        }
    }
}
