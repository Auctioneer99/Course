using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gameplay
{
    public class TimeManager : AManager
    {
        public readonly long CreatedAt;

        private readonly Stopwatch _stopwatch;

        public long GameTime { get; private set; }
        public long DeltaTime { get; private set; }

        private long _previousTime;

        private List<StateTimer> _registredTimers;


        public TimeManager(GameController controller) : base(controller)
        {
            CreatedAt = DateTime.Now.Millisecond;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _previousTime = 0;
        }

        public void Update()
        {
            long time = _stopwatch.ElapsedMilliseconds;
            DeltaTime = time - _previousTime;
            _previousTime = time;


            bool paused = false;
            if (paused)
            {
                DeltaTime = 0;
            }
            else
            {
                GameTime += DeltaTime;
            }

            if (paused == false)
            {
                // update timers
            }
        }

        public void SetupTimers()
        {
            _registredTimers.Clear();
            TimerSettings settings = GameController.Settings.TimerSettings;
            if (settings.EnableTimers)
            {
                foreach(var definition in settings.Timers)
                {
                    StateTimer timer = new StateTimer(this, definition);
                    _registredTimers.Add(timer);
                    AGameState gameState = GameController.StateMachine.CurrentState;
                    gameState.
                }
            }
        }
    }
}
