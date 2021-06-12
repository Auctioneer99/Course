using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class PlayerInGameLocalState : APlayerState
    {
        public PlayerInGameLocalState(PlayerStateMachine fsm) : base(fsm, EPlayerState.InGameLocal)
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
            Debug.Log(Player.ToString());
            Debug.Log(GameController.BoardManager.GetBoardSide(Player.EPlayer).ToString());
        }
    }
}
