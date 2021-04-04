using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PingStatus
    {
        public const long PING_INTERVAL = 5000;

        public Player Player { get; private set; }
        public GameController GameController => Player.PlayerManager.GameController;

        public int LastReportedNetworkPacketId { get; private set; }
        public int LastKnownNetworkPacketId { get; private set; }

        public long TimePassedSinceLastPing { get; private set; }

        public PingStatus(Player player)
        {
            Player = player;
            EventManager eventManager = GameController.EventManager;
            //eventManager.
        }

        public void Update()
        {
            GameController controller = GameController;
            if (controller.EGameMode != EGameMode.Spectator)
            {
                long deltaTime = controller.TimeManager.DeltaTime;
                TimePassedSinceLastPing += deltaTime;

                if (controller.HasAuthority == false &&
                    controller.PlayerManager.LocalUserId == Player.EPlayer)
                {
                    if (TimePassedSinceLastPing > PING_INTERVAL)
                    {
                        TimePassedSinceLastPing = 0;
                        PingAction action = GameController.ActionFactory.Create<PingAction>()
                            .Initialize(Player.EPlayer, LastKnownNetworkPacketId);
                        GameController.ActionDistributor.Add(action);
                    }
                }
            }
        }

        public void Ping(int lastNetworkPacketNumber)
        {
            if (LastReportedNetworkPacketId < lastNetworkPacketNumber &&
                lastNetworkPacketNumber <= LastKnownNetworkPacketId)
            {
                if (GameController.HasAuthority)
                {
                    TimePassedSinceLastPing = 0;
                }


                LastReportedNetworkPacketId = lastNetworkPacketNumber;
            }
        }
    }
}
