using Gameplay.Events;
using System.Collections.Generic;

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

        public readonly BattleEvent<Card, Position> CardMoved;
        public readonly BattleEvent<Card, Position> AfterCardMoved;
        public readonly BattleEvent<Card, Position, List<TileDefinition>> AfterCardBattlefieldMoved;

        public readonly BattleEvent<EPauseType, bool> PauseToggled;

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

            CardMoved = new BattleEvent<Card, Position>(controller);
            AfterCardMoved = new BattleEvent<Card, Position>(controller);
            AfterCardBattlefieldMoved = new BattleEvent<Card, Position, List<TileDefinition>>(controller);

            PauseToggled = new BattleEvent<EPauseType, bool>(controller);
        }
    }
}
