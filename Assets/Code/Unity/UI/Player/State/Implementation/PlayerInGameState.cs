using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class PlayerInGameState : APlayerState
    {
        public PlayerInGameState(PlayerStateMachine fsm) : base(fsm, EPlayerState.InGame)
        {

        }

        public override void OnEnterState()
        {

        }

        public override void OnLeaveState()
        {

        }

        public override void OnMouseClick()
        {
            Debug.Log("Ingame local");
        }
    }
}
