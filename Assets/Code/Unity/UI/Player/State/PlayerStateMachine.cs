using System.Collections.Generic;

namespace Gameplay.Unity
{
    public class PlayerStateMachine
    {
        public PlayerView View { get; private set; }
        public APlayerState CurrentState { get; private set; }

        public Player Player => View.Player;

        private Dictionary<EPlayerState, APlayerState> _states;

        public PlayerStateMachine(PlayerView view)
        {
            View = view;
            _states = new Dictionary<EPlayerState, APlayerState>(4)
            {
                { EPlayerState.NotConnected, new PlayerNotConnectedState(this) },
                { EPlayerState.AwaitingStart, new PlayerAwaitingStartState(this) },
            };

            
            if(View.PlayersUI.Controller.StateMachine.ECurrentState == EGameState.AwaitingPlayers)
            {
                if (Player == null)
                {
                    TransitionTo(EPlayerState.NotConnected);
                }
                else
                {
                    TransitionTo(EPlayerState.AwaitingStart);
                }
            }
        }

        public void TransitionTo(EPlayerState state)
        {
            CurrentState?.OnLeaveState();
            CurrentState = _states[state];
            CurrentState.OnEnterState();
        }
    }
}
