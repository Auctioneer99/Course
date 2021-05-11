using System.Collections.Generic;

namespace Gameplay
{
    public static class TimerFactory
    {
        public const int AWAITING_DURATION = 60000;
        public const int MULLIGAN_DURATION = 20000;
        public const int PLAY_CARD_DURATION = 20000;
        public const int TURN_DURATION = 20000;

        public const int AUTHORITY_BUFFER = 2000;

        public static List<StateTimerDefinition> CreateDefaultDefinitions()
        {
            return new List<StateTimerDefinition>()
            {
                new StateTimerDefinition(EGameState.AwaitingPlayers, CreateDefinition(AWAITING_DURATION)),
                new StateTimerDefinition(EGameState.Mulligan, CreateDefinition(MULLIGAN_DURATION)),
               // new TimerDefenition(EGameState.PlayingCard, PLAY_CARD_DURATION),
               // new TimerDefenition(EGameState.Turn, TURN_DURATION)
            };
        }

        private static TimerDefinition CreateDefinition(int duration)
        {
            return new TimerDefinition(duration, AUTHORITY_BUFFER);
        }
    }
}
