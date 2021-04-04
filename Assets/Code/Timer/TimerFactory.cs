using System.Collections.Generic;

namespace Gameplay
{
    public static class TimerFactory
    {
        public const int MULLIGAN_DURATION = 20000;
        public const int PLAY_CARD_DURATION = 20000;
        public const int TURN_DURATION = 20000;

        public static List<TimerDefenition> CreateDefaultDefinitions()
        {
            return new List<TimerDefenition>()
            {
                new TimerDefenition(EGameState.Mulligan, new List<TimerLevelDefenition>(){
                    new TimerLevelDefenition(0, MULLIGAN_DURATION)
                }),
                new TimerDefenition(EGameState.PlayingCard, new List<TimerLevelDefenition>(){
                    new TimerLevelDefenition(0, PLAY_CARD_DURATION)
                }),
                new TimerDefenition(EGameState.Turn, new List<TimerLevelDefenition>(){
                    new TimerLevelDefenition(0, TURN_DURATION)
                })
            };
        }
    }
}
