using System.Collections.Generic;

namespace Gameplay.Unity
{
    public class PlayerStateMachine
    {
        public Player Player { get; private set; }
        public APlayerState CurrentState { get; private set; }
        public PlayerIcon Icon { get; private set; }


        private Dictionary<EPlayerState, APlayerState> _states;

        public PlayerStateMachine(PlayerIcon icon, Player player)
        {
            Icon = icon;
            Player = player;
            _states = new Dictionary<EPlayerState, APlayerState>(4)
            {
                { EPlayerState.NotConnected, new PlayerNotConnectedState(this) },
                { EPlayerState.AwaitingStart, new PlayerAwaitingStartState(this) },
            };
        }

        public void TransitionTo(EPlayerState state)
        {
            CurrentState?.OnLeaveState();
            CurrentState = _states[state];
            CurrentState.OnEnterState();
        }
    }
}
