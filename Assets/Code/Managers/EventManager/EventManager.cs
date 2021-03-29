using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class EventManager : AManager
    {
        public readonly Lazy<GameController> OnGameInitialized;
        public readonly Lazy<BattleEvent<AGameState, AGameState>> OnGameStateChanged;


        public EventManager(GameController controller) : base(controller)
        {
            OnGameInitialized = new Lazy<GameController>();
            OnGameStateChanged = new Lazy<BattleEvent<AGameState, AGameState>>();
        }
    }
}
