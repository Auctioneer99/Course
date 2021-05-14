using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class PlayerPreparedState : APlayerState
    {
        public PlayerPreparedState(PlayerStateMachine fsm) : base(fsm, EPlayerState.Prepared) { }

        public override void OnEnterState()
        {
        }

        public override void OnLeaveState()
        {
        }

        public override void OnMouseClick()
        {
            Debug.Log("Some other player is ready");
        }
    }
}
