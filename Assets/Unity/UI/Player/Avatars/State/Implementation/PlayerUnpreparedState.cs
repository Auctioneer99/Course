using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class PlayerUnpreparedState : APlayerState
    {
        public PlayerUnpreparedState(PlayerStateMachine fsm) : base(fsm, EPlayerState.Unprepared) { }

        public override void OnEnterState()
        {
            
        }

        public override void OnLeaveState()
        {
            
        }

        public override void OnMouseClick()
        {
            Debug.Log("Some other player");
        }
    }
}
