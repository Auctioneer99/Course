using System.Text;

namespace Gameplay
{
    public class Player : IDeserializable
    {
        public PlayerManager PlayerManager { get; private set; }
        public GameController GameController => PlayerManager.GameController;

        public int ConnectionId { get; private set; }

        public EPlayer EPlayer => GameController.Network.Manager.GetPlayerTypeFromConnectionId(ConnectionId);
        public EPlayerStatus EStatus
        {
            get
            {
                return _eStatus;
            }
            set
            {
                if (_eStatus != value)
                {
                    _eStatus = value;
                    GameController.EventManager.OnPlayerStatusChanged.Invoke(this);
                }
            }
        }
        private EPlayerStatus _eStatus;

        public PingStatus PingStatus { get; private set; }
        public PlayerInfo Info { get; set; }

        public Player(PlayerManager manager, int connectionId)
        {
            ConnectionId = connectionId;
            PlayerManager = manager;
            EStatus = EPlayerStatus.Loading;
            Initialize();
        }

        public Player(PlayerManager manager, Packet packet)
        {
            PlayerManager = manager;
            Initialize();
            FromPacket(packet);
        }

        private void Initialize()
        {

        }

        public void Reset()
        {

        }

        public bool IsLocalUser()
        {
            return PlayerManager.LocalUserId == EPlayer;
        }

        public void FromPacket(Packet packet)
        {
            ConnectionId = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(ConnectionId);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Player]");
            sb.Append($"\n\t{EPlayer}");
            sb.Append($"\n\t{EStatus}");
            sb.Append($"\n\t{Info}");
            return sb.ToString();
        }
    }
}
