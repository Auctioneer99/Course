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
            _states = new Dictionary<EPlayerState, APlayerState>(7)
            {
                { EPlayerState.NotConnected, new PlayerNotConnectedState(this) },
                { EPlayerState.Unprepared, new PlayerUnpreparedState(this) },
                { EPlayerState.UnpreparedLocal, new PlayerUnpreparedLocalState(this) },
                { EPlayerState.Prepared, new PlayerPreparedState(this) },
                { EPlayerState.PreparedLocal, new PlayerPreparedLocalState(this) },
                { EPlayerState.InGame, new PlayerInGameState(this) },
                { EPlayerState.InGameLocal, new PlayerInGameLocalState(this) },
            };


            View.ConnectButton.enabled = false;
            View.ReadyButton.gameObject.SetActive(false);
            View.DisconnectButton.gameObject.SetActive(false);
            View.UnreadyButton.gameObject.SetActive(false);

            TransitionTo(EPlayerState.NotConnected);
        }
           
        public void TransitionTo(EPlayerState state)
        {
            CurrentState?.OnLeaveState();
            CurrentState = _states[state];
            CurrentState.OnEnterState();
        }

        public void GameStarted()
        {
            if (View.Player.IsLocalUser())
            {
                TransitionTo(EPlayerState.InGameLocal);
            }
            else
            {
                TransitionTo(EPlayerState.InGame);
            }
        }
    }
}
