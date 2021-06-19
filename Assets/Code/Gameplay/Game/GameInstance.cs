using System.Text;
using UnityEngine;

namespace Gameplay
{
    public class GameInstance : ICensored, IDeserializable
    {
        public Logger Logger { get; private set; }

        public GameController Controller { get; private set; }

        public Settings Settings { get; private set; }

        public GameData GameData { get; private set; }

        public LocalConnector Network { get; private set; }

        public EGameMode Mode { get; private set; }
        public bool HasAuthority => EGameMode.Authority.Contains(Mode);
        public bool IsMultiplayer => EGameMode.Multiplayer.Contains(Mode);

        public BattleEvent<GameInstance> SnapshotRestored;

        public GameInstance(Packet packet)
        {
            FromPacket(packet);
        }

        public GameInstance(GameData gameData, EGameMode mode, Settings settings)
        {
            GameData = gameData;
            string color = mode == EGameMode.Server ? "red" : "blue";
            Logger = new Logger(color);

            Mode = mode;
            Settings = settings;
            Network = new LocalConnector(this);
            Controller = new GameController(this, true);
            SnapshotRestored = new BattleEvent<GameInstance>(Controller);

            if (HasAuthority == false)
            {
                Settings.TimerSettings.EnableTimers = false;
            }
            else
            {
                Settings.TimerSettings.EnableTimers = true;
            }
        }

        public void Start()
        {
            Controller.Start();
        }

        public void Update()
        {
            Controller.Update();
        }

        public void Censor(EPlayer player)
        {
            if (player.Contains(EPlayer.Players))
            {
                Mode = EGameMode.Client;
            }
            if (player.Contains(EPlayer.Spectators))
            {
                Mode = EGameMode.Spectator;
            }
            Settings.Censor(player);
            Controller.Censor(player);
        }

        public GameInstance Clone()
        {
            GameInstance gi = new GameInstance(GameData, Mode, Settings);
            gi.Copy(this);
            return gi;
        }

        public void Copy(GameInstance other)
        {
            Settings.Copy(other.Settings);
            Controller.Copy(other.Controller);
            Mode = other.Mode;
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Mode)
                .Write(Settings)
                .Write(Controller);
        }

        public void FromPacket(Packet packet)
        {
            Debug.Log(string.Join(", ", packet.ToArray()));
            Debug.Log(1);
            Mode = packet.ReadEGameMode();
            Debug.Log(2);
            Settings.FromPacket(packet);
            Debug.Log(3);
            //Controller = new GameController(this, true);
            Controller.FromPacket(packet);
            Debug.Log(4);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[GameInstance]");
            sb.AppendLine("GameMode = " + Mode);
            sb.AppendLine(Network.ToString());
            sb.AppendLine(Settings.ToString());
            sb.AppendLine(Controller.ToString());
            return sb.ToString();
        }
    }
}
