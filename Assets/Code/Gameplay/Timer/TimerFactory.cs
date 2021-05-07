using System.Collections.Generic;

namespace Gameplay
{
    public static class TimerFactory
    {
        public const int AWAITING_DURATION = 5000;
        public const int MULLIGAN_DURATION = 20000;
        public const int PLAY_CARD_DURATION = 20000;
        public const int TURN_DURATION = 20000;

        public static List<StateTimerDefinition> CreateDefaultDefinitions()
        {
            return new List<StateTimerDefinition>()
            {
                new StateTimerDefinition(EGameState.AwaitingPlayers, new TimerDefinition(AWAITING_DURATION)),
               // new TimerDefenition(EGameState.Mulligan, MULLIGAN_DURATION),
               // new TimerDefenition(EGameState.PlayingCard, PLAY_CARD_DURATION),
               // new TimerDefenition(EGameState.Turn, TURN_DURATION)
            };
        }
    }
}
