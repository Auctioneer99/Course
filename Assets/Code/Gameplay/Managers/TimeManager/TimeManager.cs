using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Gameplay
{
    public class TimeManager : AManager, IStateObjectCloneable<TimeManager>, IRuntimeDeserializable, ICensored
    {
        public long CreatedAt { get; private set; }

        private Stopwatch _stopwatch;

        private List<StateTimer> _timers = new List<StateTimer>();

        public long GameTime { get; private set; }
        public long DeltaTime { get; private set; }
        private long _previousTime;

        public TimeManager(GameController controller) : base(controller)
        {
            CreatedAt = DateTime.Now.Millisecond;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _previousTime = 0;
        }

        public void Reset()
        {
            foreach(var t in _timers)
            {
                t.Reset();
            }
            _timers.Clear();
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
                if (GameController.StateMachine.CurrentState != null && GameController.StateMachine.CurrentState.HasTimer)
                {
                    GameController.StateMachine.CurrentState.Timer.Update(DeltaTime);
                }
            }
        }

        public void SetupTimers()
        {
            _timers.Clear();
            TimerSettings settings = GameController.GameInstance.Settings.TimerSettings;
            if (settings.EnableTimers)
            {
                foreach(var definition in settings.Timers)
                {
                    StateTimer timer = new StateTimer(this, definition);
                    _timers.Add(timer);
                    AGameState gameState = GameController.StateMachine.GetState(timer.EGameState);
                    gameState.SetupTimer(timer);
                }
                //GameController.Logger.Log("TimeManager Timers Setuped");
            }
        }

        public StateTimer GetCurrentStateTimer()
        {
            EGameState state = GameController.StateMachine.ECurrentState;
            return GetTimer(state);
        }

        public StateTimer GetTimer(EGameState state)
        {
            foreach(var t in _timers)
            {
                if (t.EGameState == state)
                {
                    return t;
                }
            }
            return null;
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            _previousTime = packet.ReadLong();
            GameTime = packet.ReadLong();
            DeltaTime = packet.ReadLong();

            SetupTimers();
            foreach(var timer in _timers)
            {
                timer.FromPacket(controller, packet);
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(_previousTime)
                .Write(GameTime)
                .Write(DeltaTime);

            foreach(var t in _timers)
            {
                packet.Write(t);
            }
        }

        public TimeManager Clone(GameController controller)
        {
            TimeManager manager = new TimeManager(controller);
            manager.Copy(this, controller);
            return manager;
        }

        public void Copy(TimeManager other, GameController controller)
        {
            _previousTime = other._previousTime;
            GameTime = other.GameTime;
            DeltaTime = other.DeltaTime;

            SetupTimers();
            int count = _timers.Count;
            for (int i = 0; i < count; i++)
            {
                _timers[i].Copy(other._timers[i], controller);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[TimeManager]");
            sb.AppendLine($"GameTime = {GameTime}");
            sb.AppendLine($"DeltaTime = {DeltaTime}");
            foreach(var t in _timers)
            {
                sb.AppendLine(t.ToString());
            }
            return sb.ToString();
        }

        public void Censor(EPlayer player)
        {
            foreach(var t in _timers)
            {
                t.Censor(player);
            }
        }
    }
}
