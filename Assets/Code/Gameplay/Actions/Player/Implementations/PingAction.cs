using System;

namespace Gameplay
{
    public class PingAction : APlayerAction, IUserAction, IPriorityAction
    {
        public override EAction EAction => EAction.Ping;

        public bool ValidWhenBlocked => true;

        public int LastNetworkPacketNumber { get; private set; }

        public PingAction Initialize(int lastPacketNumber)
        {
            Initialize();

            LastNetworkPacketNumber = lastPacketNumber;
            return this;
        }

        protected override void ApplyImplementation()
        {
            Player player = GameController.PlayerManager.GetPlayer(EPlayer);
            //player.PingStatus.Ping(LastNetworkPacketNumber);
        }

        protected override void PlayerAttributesFrom(Packet packet)
        {
            LastNetworkPacketNumber = packet.ReadInt();
        }

        protected override void PlayerAttributesTo(Packet packet)
        {
            packet.Write(LastNetworkPacketNumber);
        }

        protected override void PlayerCopyImplementation(APlayerAction copyFrom, GameController controller)
        {
            throw new NotImplementedException();
        }
    }
}
