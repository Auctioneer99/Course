using System.Text;

namespace Gameplay
{
    public class Player : ICensored, IStateObjectCloneable<Player>, IRuntimeDeserializable
    {
        public PlayerManager PlayerManager { get; private set; }
        public GameController GameController => PlayerManager.GameController;

        public int ConnectionId { get; private set; }

        public EPlayer EPlayer { get; private set; }
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

        //public PingStatus PingStatus { get; private set; }
        public PlayerInfo Info { get; set; }

        public Player(PlayerManager manager)
        {
            PlayerManager = manager;
        }

        public Player(PlayerManager manager, EPlayer player, int connectionId)
        {
            EPlayer = player;
            ConnectionId = connectionId;
            PlayerManager = manager;
            EStatus = EPlayerStatus.Ready;
            Initialize();
        }

        public Player(PlayerManager manager, Packet packet)
        {
            PlayerManager = manager;
            Initialize();
            FromPacket(manager.GameController, packet);
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

        public void FromPacket(GameController controller, Packet packet)
        {
            ConnectionId = packet.ReadInt();
            EStatus = packet.ReadEPlayerStatus();
            Info = new PlayerInfo(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(ConnectionId)
                .Write(_eStatus)
                .Write(Info);
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

        public void Censor(EPlayer player)
        {
            
        }

        public Player Clone(GameController controller)
        {
            Player p = new Player(PlayerManager);
            p.Copy(this, controller);
            return p;
        }

        public void Copy(Player other, GameController controller)
        {
            ConnectionId = other.ConnectionId;
            EPlayer = other.EPlayer;
            Info = other.Info.Clone();
        }
    }
}
