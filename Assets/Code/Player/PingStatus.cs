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

        public long Passed { get; private set; }

        public PingStatus(Player player)
        {
            Player = player;
            EventManager eventManager = GameController.EventManager;
            eventManager.
        }

        public void Update()
        {
            GameController controller = GameController;
            if (controller.GameMode != EGameMode.Spectator)
            {
                long deltaTime = controller.TimeManager.DeltaTime;
                Passed += deltaTime;

                if (controller.HasAuthority == false &&
                    controller.PlayerManager.LocalUserId == Player.EPlayer)
                {
                    if (Passed > PING_INTERVAL)
                    {
                        Passed = 0;
                        //send action about ping
                    }
                }
            }
        }
    }
}
