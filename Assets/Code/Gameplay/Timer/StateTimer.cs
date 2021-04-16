using System;

namespace Gameplay
{
    public class StateTimer
    {
        public const int TIME_BUFFER_FOR_AUTHORITY_SIDE = 2000;

        public readonly BattleEvent<StateTimer> OnElapsed;

        public GameController GameController => TimeManager.GameController;
        public TimeManager TimeManager { get; private set; }
        public TimerDefenition Definition { get; private set; }
        public EGameState EGameState => Definition.EGameState;
        public ETimerState ETimerState { get; private set; }

        public bool IsRunning => ETimerState == ETimerState.Running;
        public bool IsElapsed => ETimerState == ETimerState.Elapsed;

        public int Duration { get; private set; }
        public int TimeRemaining { get; private set; }

        public StateTimer(TimeManager manager, TimerDefenition definition)
        {
            TimeManager = manager;
            Definition = definition;

            ETimerState = ETimerState.Stopped;

            OnElapsed = new BattleEvent<StateTimer>(TimeManager.GameController);
        }

        public void Reset()
        {
            Definition = null;
            ETimerState = ETimerState.Stopped;
        }

        public void Start()
        {
            if (ETimerState == ETimerState.Running)
            {
                throw new Exception("Already running");
            }
            Duration = Definition.Duration + 
                (TimeManager.GameController.HasAuthority ? TIME_BUFFER_FOR_AUTHORITY_SIDE : 0);
            TimeRemaining = Duration;
            ETimerState = ETimerState.Running;

            GameController.EventManager.OnStateTimerStarted.Invoke(this);
        }

        public void Update(long deltaTime)
        {
            if (IsRunning)
            {
                TimeRemaining -= (int)deltaTime;
                if (TimeRemaining <= 0)
                {
                    GameController.Logger.Log($"{EGameState} Time is out");
                    Finish();
                }
            }
        }

        public void Finish()
        {
            if (ETimerState == ETimerState.Elapsed)
            {
                throw new Exception("Timer already elapsed");
            }
            TimeRemaining = 0;
            ETimerState = ETimerState.Elapsed;
            OnElapsed.Invoke(this);
        }
    }
}
