using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class TimerDefenition
    {
        public EGameState EGameState;
        public List<TimerLevelDefenition> Levels = new List<TimerLevelDefenition>();

        public TimerDefenition() { }

        public TimerDefenition(EGameState eGameState, List<TimerLevelDefenition> levels)
        {
            EGameState = eGameState;
            Levels.AddRange(levels);
        }

        public TimerLevelDefenition AtLevel(int level)
        {
            level = Math.Min(level, Levels.Count - 1);
            return Levels[level];
        }
    }
}
