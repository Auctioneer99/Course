using System;
using System.Text;

namespace Gameplay
{
    public class Timer : IRuntimeDeserializable, IRuntimeStateObject<Timer>, ICensored
    {
        public const int TIME_BUFFER_FOR_AUTHORITY_SIDE = 2000;

        public BattleEvent<Timer> Elapsed { get; private set; }
        public BattleEvent<Timer> Started { get; private set; }

        public GameController GameController { get; private set; }
        public TimerDefinition Definition { get; private set; }
        public ETimerState ETimerState { get; private set; }

        public bool IsRunning => ETimerState == ETimerState.Running;
        public bool IsElapsed => ETimerState == ETimerState.Elapsed;

        public int Duration => Definition.Duration;
        public int TimeRemaining => GameController.HasAuthority ? 
            _timeRemaining + Definition.AuthorityBuffer
            : _timeRemaining;

        private int _timeRemaining;

        public Timer(GameController controller, TimerDefinition definition)
        {
            GameController = controller;
            Definition = definition;
            ETimerState = ETimerState.Stopped;
            Started = new BattleEvent<Timer>(GameController);
            Elapsed = new BattleEvent<Timer>(GameController);
        }

        public void Reset()
        {
            Started.RemoveAllListeners(true);
            Elapsed.RemoveAllListeners(true);
        }

        public void Start()
        {
            if (ETimerState == ETimerState.Running)
            {
                throw new Exception("Already running");
            }

            _timeRemaining = Duration;
            ETimerState = ETimerState.Running;

            Started.Invoke(this);
        }

        public void Update(long deltaTime)
        {
            if (IsRunning)
            {
                _timeRemaining -= (int)deltaTime;
                if (TimeRemaining <= 0)
                {
                    GameController.Logger.Log($"Time is out");
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
            _timeRemaining = 0;
            ETimerState = ETimerState.Elapsed;

            Elapsed.Invoke(this);
        }

        public void Copy(Timer other, GameController controller)
        {
            Definition.Copy(other.Definition);
            ETimerState = other.ETimerState;
            _timeRemaining = other._timeRemaining;
        }

        public void Censor(EPlayer player)
        {
            /*
            if (player.Contains(EPlayer.NonAuthority))
            {
                TimeRemaining -= TIME_BUFFER_FOR_AUTHORITY_SIDE;
                Duration -= TIME_BUFFER_FOR_AUTHORITY_SIDE;
            }*/
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            _timeRemaining = packet.ReadInt();
            ETimerState = packet.ReadETimerState();
            Definition = new TimerDefinition(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet//.Write(Duration)
                .Write(_timeRemaining)
                .Write(ETimerState)
                .Write(Definition);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Timer]");
            sb.AppendLine($"State = {ETimerState}");
            sb.AppendLine($"Duration = {Duration}");
            sb.AppendLine($"TimeRemaining = {TimeRemaining}");
            sb.AppendLine(Definition.ToString());
            return sb.ToString();
        }
    }
}
