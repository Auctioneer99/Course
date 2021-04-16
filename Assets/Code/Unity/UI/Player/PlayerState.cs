namespace Gameplay.Unity
{
    public abstract class APlayerState
    {
        public PlayerStateMachine FSM { get; private set; }

        public Player Player => FSM.Player;
        public PlayerIcon Icon => FSM.Icon;

        public APlayerState(PlayerStateMachine fsm)
        {
            FSM = fsm;
        }

        public EPlayerState State { get; private set; }
        public abstract void OnMouseEnter();
        public abstract void OnMouseLeave();
        public abstract void OnMouseClick();
        public abstract void OnEnterState();
        public abstract void OnLeaveState();
    }
}
