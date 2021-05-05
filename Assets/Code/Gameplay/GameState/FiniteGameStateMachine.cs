using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class FiniteGameStateMachine : IStateObjectCloneable<FiniteGameStateMachine>, IRuntimeDeserializable
    {
        public const int STATES_COUNT = 13;

        public AGameState CurrentState => _currentState;
        public EGameState ECurrentState => _currentState == null ? EGameState.Invalid : _currentState.EGameState;
        private AGameState _currentState;

        private Dictionary<EGameState, AGameState> _states;

        public GameController GameController { get; private set; }

        public FiniteGameStateMachine(GameController controller)
        {
            GameController = controller;
            Initialize();
        }

        private void Initialize()
        {
            _states = new Dictionary<EGameState, AGameState>(STATES_COUNT)
            {
                { EGameState.AwaitingPlayers, new AwaitingPlayersState(GameController) },
                { EGameState.Init, new InitializeGameState(GameController) },
            };
        }

        public void AddState(AGameState state)
        {
            if (_states.ContainsKey(state.EGameState))
            {
                throw new System.Exception("State already registred");
            }
            else
            {
                _states.Add(state.EGameState, state);
            }
        }

        public void Update()
        {
            _currentState?.Update();
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
            if (_currentState?.EGameState == state)
            {
                return;
            }

            _states.TryGetValue(state, out AGameState nextState);

            AGameState previousState = _currentState;
            _currentState = nextState;

            previousState?.OnLeaveState(state);
            _currentState?.OnEnterState(previousState == null ? EGameState.Invalid : previousState.EGameState);

            GameController.EventManager.OnGameStateChanged.Invoke(previousState, _currentState);
            GameController.Logger.Log($"FGSM After Transition {previousState?.EGameState} -> {state}");
        }

        public override string ToString()
        {
            return $"[FGSM] \n CurrentState: {ECurrentState}";
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            EGameState currentState = packet.ReadEGameState();
            _currentState = FindState(currentState);

            int count = _states.Count;
            for (int i = 0; i < count; i++)
            {
                EGameState state = packet.ReadEGameState();
                AGameState gameState = FindState(state);
                gameState.FromPacket(controller, packet);
            }
        }

        public void ToPacket(Packet packet)
        {
            Debug.Log("Writing ECurrentState " + ECurrentState);
            packet.Write(ECurrentState);
            foreach(var s in _states)
            {
                packet.Write(s.Key)
                    .Write(s.Value);
            }
        }

        public AGameState FindState(EGameState state)
        {
            if (_states.TryGetValue(state, out AGameState result))
            {
                return result;
            }
            return null;
        }

        public FiniteGameStateMachine Clone(GameController controller)
        {
            FiniteGameStateMachine fsm = new FiniteGameStateMachine(controller);
            fsm.Copy(this, controller);
            return fsm;
        }

        public void Copy(FiniteGameStateMachine other, GameController controller)
        {
            _currentState = other.CurrentState == null ? null : _states[other.ECurrentState];
            
        }
    }
}
