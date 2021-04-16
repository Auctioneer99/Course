using System.Collections.Generic;

namespace Gameplay
{
    public static class TimerFactory
    {
        public const int AWAITING_DURATION = 5000;
        public const int MULLIGAN_DURATION = 20000;
        public const int PLAY_CARD_DURATION = 20000;
        public const int TURN_DURATION = 20000;

        public static List<TimerDefenition> CreateDefaultDefinitions()
        {
            return new List<TimerDefenition>()
            {
                new TimerDefenition(EGameState.AwaitingPlayers, AWAITING_DURATION),
               // new TimerDefenition(EGameState.Mulligan, MULLIGAN_DURATION),
               // new TimerDefenition(EGameState.PlayingCard, PLAY_CARD_DURATION),
               // new TimerDefenition(EGameState.Turn, TURN_DURATION)
            };
        }
    }
}
