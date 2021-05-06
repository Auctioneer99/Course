using System;
using System.Text;
using UnityEngine;

namespace Gameplay
{
    public class StateTimer : IRuntimeDeserializable, IStateObjectCloneable<StateTimer>, ICensored
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
            OnElapsed.CoreEvent.RemoveAllListeners();
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

        public StateTimer Clone(GameController controller)
        {
            StateTimer timer = new StateTimer(TimeManager, Definition);
            timer.Copy(this, controller);
            return timer;
        }

        public void Copy(StateTimer other, GameController controller)
        {
            Definition = other.Definition;
            Duration = other.Duration;
            ETimerState = other.ETimerState;
            TimeRemaining = other.TimeRemaining;
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            Duration = packet.ReadInt();
            TimeRemaining = packet.ReadInt();
            ETimerState = packet.ReadETimerState();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Duration)
                .Write(TimeRemaining)
                .Write(ETimerState);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[StateTimer]");
            sb.AppendLine($"State = {ETimerState}");
            sb.AppendLine($"Duration = {Duration}");
            sb.AppendLine($"TimeRemaining = {TimeRemaining}");
            sb.AppendLine(Definition.ToString());
            return sb.ToString();
        }

        public void Censor(EPlayer player)
        {
            //Debug.Log("Censoring state timer " + player.Contains(EPlayer.NonAuthority));
            if (player.Contains(EPlayer.NonAuthority))
            {
                TimeRemaining -= TIME_BUFFER_FOR_AUTHORITY_SIDE;
                Duration -= TIME_BUFFER_FOR_AUTHORITY_SIDE;
            }
        }
    }
}
