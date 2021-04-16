namespace Gameplay
{
    public abstract class AConnector
    {
        public int ConnectionId { get; private set; }

        public EPlayer Role { get; protected set; } = EPlayer.Spectators;

        public NetworkManager Manager { get; set; }

        public virtual void Initialize(int id)
        {
            ConnectionId = id;
        }

        public void Send(AAction action, int connectionId)
        {
            Manager.SendToTarget(this, action, connectionId);
        }

        public void Send(AAction action)
        {
            Packet packet = new Packet();
            action.ToPacket(packet);

            if (action is ICensoredAction)
            {
                foreach (var p in GameController.PlayerManager.Players.Values)
                {
                    CensorSend(action, p.EPlayer);
                }
                if (GameController.PlayerManager.HasSpectators)
                {
                    Send(packet, NetworkTarget.Spectators, EPlayer.Spectator);
                }
            }
            else
            {
                GetPacketReceivers(action, out NetworkTarget target, out EPlayer player);
                int[] toConnections = ConvertToConnectionIds(target, player);
                Manager.SendToTargets(this, action, toConnections);
            }
        }

        private void GetPacketReceivers(AAction action, out NetworkTarget target, out EPlayer Role)
        {

        }

        private int[] ConvertToConnectionIds(NetworkTarget target, EPlayer Role)
        {
            return new int[0];
        }

        public abstract void GetDecks();

        public abstract void HandleMessage(AConnector sender, AAction action);
    }
}
