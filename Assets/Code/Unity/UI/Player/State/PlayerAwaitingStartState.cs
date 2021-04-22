﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class PlayerAwaitingStartState : APlayerState
    {
        public PlayerAwaitingStartState(PlayerStateMachine fsm) : base(fsm)
        {

        }

        public override void OnEnterState()
        {
            FSM.View.ConnectButton.enabled = false;
            //set image
        }

        public override void OnLeaveState()
        {
        }

        public override void OnMouseClick()
        {
            //FSM.TransitionTo(EPlayerState.NotConnected);
            Debug.Log(Player.Info.Name);
        }

        public override void OnMouseEnter()
        {
            
        }

        public override void OnMouseLeave()
        {
            
        }
    }
}
