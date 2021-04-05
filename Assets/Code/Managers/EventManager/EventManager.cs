using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class EventManager : AManager
    {
        public readonly BattleEvent<GameController> OnGameInitialized;
        public readonly BattleEvent<AGameState, AGameState> OnGameStateChanged;


        public readonly BattleEvent<Player> OnPlayerStatusChanged;
        public readonly BattleEvent<StateTimer> OnStateTimerElapsed;


        public EventManager(GameController controller) : base(controller)
        {
            OnGameInitialized = new BattleEvent<GameController>(controller);
            OnGameStateChanged = new BattleEvent<AGameState, AGameState>(controller);

            OnPlayerStatusChanged = new BattleEvent<Player>(controller);
            OnStateTimerElapsed = new BattleEvent<StateTimer>(controller);
        }
    }
}
