using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class PlayerNotConnectedState : APlayerState
    {
        public PlayerNotConnectedState(PlayerStateMachine fsm) : base(fsm)
        {
        }

        public override void OnEnterState()
        {
            Icon.ConnectButton.enabled = true;
        }

        public override void OnLeaveState()
        {
            Debug.Log("disabling");
            Icon.ConnectButton.enabled = false;
        }

        public override void OnMouseClick()
        {
            Debug.Log("Trying to connect");
            FSM.TransitionTo(EPlayerState.AwaitingStart);
            //try to connect
        }

        public override void OnMouseEnter()
        {
        }

        public override void OnMouseLeave()
        {
        }
    }
}
