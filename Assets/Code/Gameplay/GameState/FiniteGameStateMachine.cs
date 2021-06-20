using System;
using System.Collections.Generic;
using System.Text;

namespace Gameplay
{
    public class FiniteGameStateMachine : IRuntimeStateObject<FiniteGameStateMachine>, IRuntimeDeserializable, ICensored
    {
        public GameController GameController { get; private set; }
        public TimeManager TimeManager { get; private set;}

        public AGameState CurrentState { get; private set; }
        private Dictionary<EGameState, AGameState> _states;

        public EGameState ECurrentState => CurrentState == null ? EGameState.Invalid : CurrentState.EGameState;

        public FiniteGameStateMachine(GameController controller)
        {
            GameController = controller;
            TimeManager = new TimeManager(this);
            Initialize();
        }

        private void Initialize()
        {
            int statesCount = Enum.GetValues(typeof(EAction)).Length;
            _states = new Dictionary<EGameState, AGameState>(statesCount)
            {
                { EGameState.AwaitingPlayers, new AwaitingPlayersState(GameController) },
                { EGameState.Init, new InitializeGameState(GameController) },
                { EGameState.Mulligan, new MulliganGameState(GameController) },
            };
        }

        public void Reset()
        {
            foreach(var s in _states)
            {
                s.Value.Reset();
            }
            _states.Clear();
        }

        public void Start()
        {
            if (GameController.HasAuthority)
            {
                TimeManager.SetupTimers();
            }

            TransitionTo(EGameState.AwaitingPlayers);
        }

        public void Update()
        {
            TimeManager.Update();
            CurrentState?.Update();
        }

        public void AddState(AGameState state)
        {
            if (_states.ContainsKey(state.EGameState))
            {
                throw new Exception("State already registred");
            }
            else
            {
                _states.Add(state.EGameState, state);
            }
        }

        public AGameState GetState(EGameState state)
        {
            if (_states.TryGetValue(state, out AGameState result))
            {
                return result;
            }
            return null;
        }

        public void TransitionTo(EGameState state)
        {
            if (ECurrentState == state)
            {
                return;
            }

            _states.TryGetValue(state, out AGameState nextState);

            AGameState previousState = CurrentState;
            CurrentState = nextState;

            previousState?.OnLeaveState(state);
            CurrentState?.OnEnterState(previousState == null ? EGameState.Invalid : previousState.EGameState);

            GameController.EventManager.OnGameStateChanged.Invoke(previousState, CurrentState);
            GameController.Logger.Log($"FGSM After Transition {previousState?.EGameState} -> {state}");
        }

        public void Copy(FiniteGameStateMachine other, GameController controller)
        {
            CurrentState = other.CurrentState == null ? null : _states[other.ECurrentState];
            TimeManager.Copy(other.TimeManager, controller);
        }

        public void Censor(EPlayer player)
        {
            TimeManager.Censor(player);
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            EGameState currentState = packet.ReadEGameState();
            CurrentState = GetState(currentState);

            int count = _states.Count;
            for (int i = 0; i < count; i++)
            {
                EGameState state = packet.ReadEGameState();
                AGameState gameState = GetState(state);
                gameState.FromPacket(controller, packet);
            }
            //TimeManager = new TimeManager(this, packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(ECurrentState);
            foreach (var s in _states)
            {
                packet.Write(s.Key)
                    .Write(s.Value);
            }
            packet.Write(TimeManager);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("FSM");
            sb.AppendLine($"CurrentState: {ECurrentState}");
            sb.AppendLine(TimeManager.ToString());
            return sb.ToString();
        }
    }
}
