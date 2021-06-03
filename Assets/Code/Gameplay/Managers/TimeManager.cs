using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Gameplay
{
    public class TimeManager : IRuntimeDeserializable, ICensored
    {
        public FiniteGameStateMachine FiniteGameStateMachine { get; private set; }

        public long CreatedAt { get; private set; }
        public long GameTime { get; private set; }
        public long DeltaTime { get; private set; }

        private Stopwatch _stopwatch = new Stopwatch();
        private List<StateTimer> _stateTimers = new List<StateTimer>();
        private long _previousTime;

        public GameController GameController => FiniteGameStateMachine.GameController;

        public TimeManager()
        {
            _stopwatch.Start();
        }

        public TimeManager(FiniteGameStateMachine fsm, Packet packet)
        {
            FromPacket(fsm.GameController, packet);
            _stopwatch.Start();
        }

        public TimeManager(FiniteGameStateMachine fsm)
        {
            CreatedAt = DateTime.Now.Millisecond;

            _stopwatch.Start();
            _previousTime = 0;
            FiniteGameStateMachine = fsm;
        }

        public void Reset()
        {
            foreach(var t in _stateTimers)
            {
                t.Reset();
            }
            _stateTimers.Clear();
            _stopwatch.Stop();
        }

        public void Update()
        {
            long time = _stopwatch.ElapsedMilliseconds;
            DeltaTime = time - _previousTime;
            _previousTime = time;


            bool paused = GameController.PauseManager.HasPause(EPauseType.Timers);
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
                if (FiniteGameStateMachine.CurrentState != null && FiniteGameStateMachine.CurrentState.HasTimer)
                {
                    FiniteGameStateMachine.CurrentState.Timer.Update(DeltaTime);
                }
            }
        }

        public void SetupTimers()
        {
            _stateTimers.Clear();
            TimerSettings settings = GameController.GameInstance.Settings.TimerSettings;
            if (settings.EnableTimers)
            {
                foreach(var definition in settings.StateTimers)
                {
                    StateTimer timer = new StateTimer(this, definition);
                    _stateTimers.Add(timer);
                    AGameState gameState = FiniteGameStateMachine.GetState(timer.EGameState);
                    gameState.SetupTimer(timer);
                }
            }
        }

        public StateTimer GetCurrentStateTimer()
        {
            EGameState state = FiniteGameStateMachine.ECurrentState;
            return GetTimer(state);
        }

        public StateTimer GetTimer(EGameState state)
        {
            foreach(var t in _stateTimers)
            {
                if (t.EGameState == state)
                {
                    return t;
                }
            }
            return null;
        }

        public TimeManager Clone(FiniteGameStateMachine fsm)
        {
            TimeManager manager = new TimeManager();
            manager.Copy(this, fsm);
            return manager;
        }

        public void Copy(TimeManager other, FiniteGameStateMachine fsm)
        {
            FiniteGameStateMachine = fsm;

            _previousTime = other._previousTime;
            GameTime = other.GameTime;
            DeltaTime = other.DeltaTime;

            foreach(var t in other._stateTimers)
            {
                StateTimer timer = t.Clone(fsm.GameController);
                _stateTimers.Add(timer);
                AGameState gameState = FiniteGameStateMachine.GetState(timer.EGameState);
                gameState.SetupTimer(timer);
            }
            /*
            SetupTimers();
            int count = _stateTimers.Count;
            for (int i = 0; i < count; i++)
            {
                _stateTimers[i].Copy(other._stateTimers[i], controller);
            }*/
        }

        public void Censor(EPlayer player)
        {
            foreach (var t in _stateTimers)
            {
                t.Censor(player);
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            FiniteGameStateMachine = controller.StateMachine;

            _previousTime = packet.ReadLong();
            GameTime = packet.ReadLong();
            DeltaTime = packet.ReadLong();

            int count = packet.ReadInt();
            for (int i = 0; i < count; i++)
            {
                StateTimer timer = new StateTimer(this, packet);
                _stateTimers.Add(timer);
                AGameState gameState = FiniteGameStateMachine.GetState(timer.EGameState);
                gameState.SetupTimer(timer);
            }

            //SetupTimers();
            /*
            foreach(var timer in _stateTimers)
            {
                timer.FromPacket(controller, packet);
            }*/
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(_previousTime)
                .Write(GameTime)
                .Write(DeltaTime)
                .Write(_stateTimers);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[TimeManager]");
            sb.AppendLine($"GameTime = {GameTime}");
            sb.AppendLine($"DeltaTime = {DeltaTime}");
            foreach(var t in _stateTimers)
            {
                sb.AppendLine(t.ToString());
            }
            return sb.ToString();
        }
    }
}
