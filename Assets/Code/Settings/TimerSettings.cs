using System.Collections.Generic;

namespace Gameplay
{
    public class TimerSettings
    {
        public bool EnableTimers { get; private set; }
        public List<TimerDefenition> Timers { get; private set; }

        public TimerSettings(bool enableTimers)
        {
            EnableTimers = enableTimers;
            if (enableTimers)
            {
                Timers = TimerFactory.CreateDefaultDefinitions();
            }
        }

        public TimerSettings(List<TimerDefenition> definitions)
        {
            EnableTimers = true;
            Timers.AddRange(definitions);
        }
    }
}
