using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Gameplay;

namespace Gameplay.Unity
{
    public class PlayerNotConnectedState : APlayerState
    {
        public PlayerNotConnectedState(PlayerStateMachine fsm) : base(fsm, EPlayerState.NotConnected)
        {
        }

        public override void OnEnterState()
        {
            FSM.View.ConnectButton.enabled = true;
        }

        public override void OnLeaveState()
        {
            FSM.View.ConnectButton.enabled = false;
        }

        public override void OnMouseClick()
        {
            if (GameController.Network.Role == EPlayer.Spectators)
            {
                AskJoinAction join = GameController.ActionFactory.Create<AskJoinAction>()
                    .Initialize(FSM.View.Place);
                GameController.ActionDistributor.Add(join);
            }
        }
    }
}
