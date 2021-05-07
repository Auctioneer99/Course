using System;
using System.Text;

namespace Gameplay
{
    public class Timer : IRuntimeDeserializable, IStateObjectCloneable<Timer>, ICensored
    {
        public const int TIME_BUFFER_FOR_AUTHORITY_SIDE = 2000;

        public BattleEvent<Timer> Elapsed { get; private set; }
        public BattleEvent<Timer> Started { get; private set; }

        public GameController GameController { get; private set; }
        public TimerDefinition Definition { get; private set; }
        public ETimerState ETimerState { get; private set; }

        public bool IsRunning => ETimerState == ETimerState.Running;
        public bool IsElapsed => ETimerState == ETimerState.Elapsed;

        public int Duration { get; private set; }
        public int TimeRemaining { get; private set; }

        private Timer() { }

        public Timer(GameController controller, Packet packet)
        {
            FromPacket(controller, packet);
            Initialize();
        }

        public Timer(GameController controller, TimerDefinition definition)
        {
            GameController = controller;
            Definition = definition;
            ETimerState = ETimerState.Stopped;
            Initialize();
        }

        private void Initialize()
        {
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
            Duration = Definition.Duration + 
                (GameController.HasAuthority ? TIME_BUFFER_FOR_AUTHORITY_SIDE : 0);
            TimeRemaining = Duration;
            ETimerState = ETimerState.Running;

            Started.Invoke(this);
        }

        public void Update(long deltaTime)
        {
            if (IsRunning)
            {
                TimeRemaining -= (int)deltaTime;
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
            TimeRemaining = 0;
            ETimerState = ETimerState.Elapsed;

            Elapsed.Invoke(this);
        }

        public Timer Clone(GameController controller)
        {
            Timer timer = new Timer();
            timer.Copy(this, controller);
            return timer;
        }

        public void Copy(Timer other, GameController controller)
        {
            GameController = controller;
            Definition = other.Definition.Clone();
            Duration = other.Duration;
            ETimerState = other.ETimerState;
            TimeRemaining = other.TimeRemaining;

            Initialize();
        }

        public void Censor(EPlayer player)
        {
            if (player.Contains(EPlayer.NonAuthority))
            {
                TimeRemaining -= TIME_BUFFER_FOR_AUTHORITY_SIDE;
                Duration -= TIME_BUFFER_FOR_AUTHORITY_SIDE;
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            GameController = controller;
            Duration = packet.ReadInt();
            TimeRemaining = packet.ReadInt();
            ETimerState = packet.ReadETimerState();
            Definition = new TimerDefinition(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Duration)
                .Write(TimeRemaining)
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
