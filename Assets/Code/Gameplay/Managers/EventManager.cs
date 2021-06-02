using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class EventManager : AManager
    {
        public readonly BattleEvent OnSnapshotRestored;
        public readonly BattleEvent<Player> OnPlayerSetup;
        public readonly BattleEvent<Player> PlayerDisconnected;
        public readonly BattleEvent<GameController> OnGameInitialized;
        public readonly BattleEvent<AGameState, AGameState> OnGameStateChanged;


        public readonly BattleEvent<Player> OnPlayerStatusChanged;

        public readonly BattleEvent<StateTimer> OnStateTimerStarted;
        public readonly BattleEvent<StateTimer> OnStateTimerElapsed;

        public readonly BattleEvent OnGameStatusChanged;

        public readonly BattleEvent<Battlefield> OnBattleFieldSetuped;
        public readonly BattleEvent<List<Card>> CardsSpawned;

        public readonly BattleEvent<Card, Location> CardMoved;

        public EventManager(GameController controller) : base(controller)
        {
            OnGameInitialized = new BattleEvent<GameController>(controller);
            OnGameStateChanged = new BattleEvent<AGameState, AGameState>(controller);

            OnBattleFieldSetuped = new BattleEvent<Battlefield>(controller);

            CardsSpawned = new BattleEvent<List<Card>>(controller);

            OnPlayerStatusChanged = new BattleEvent<Player>(controller);

            OnStateTimerStarted = new BattleEvent<StateTimer>(controller);
            OnStateTimerElapsed = new BattleEvent<StateTimer>(controller);

            OnGameStatusChanged = new BattleEvent(controller);

            OnSnapshotRestored = new BattleEvent(controller);
            OnPlayerSetup = new BattleEvent<Player>(controller);
            PlayerDisconnected = new BattleEvent<Player>(controller);

            CardMoved = new BattleEvent<Card, Location>(controller);
        }
    }
}
