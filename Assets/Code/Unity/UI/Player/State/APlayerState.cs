using UnityEngine;

namespace Gameplay.Unity
{
    public abstract class APlayerState
    {
        public PlayerStateMachine FSM { get; private set; }

        public GameController GameController => FSM.View.PlayersUI.Controller;
        public Player Player => FSM.Player;

        public APlayerState(PlayerStateMachine fsm)
        {
            FSM = fsm;
        }

        public EPlayerState State { get; private set; }

        public void OnMouseEnter()
        {
            Debug.Log(Player.ToString());
        }

        public void OnMouseLeave()
        {

        }

        public abstract void OnMouseClick();
        public abstract void OnEnterState();
        public abstract void OnLeaveState();
    }
}
