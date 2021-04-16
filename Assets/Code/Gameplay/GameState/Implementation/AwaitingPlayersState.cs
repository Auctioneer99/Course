﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class AwaitingPlayersState : AAuthorityGameState
    {
        public AwaitingPlayersState(GameController controller) : base(controller, EGameState.AwaitingPlayers)
        { }

        public void SetReady()
        {
            _isReady = true;
        }

        public override void OnLeaveState(EGameState newStateId)
        {
            base.OnLeaveState(newStateId);
            //
        }

        protected override void OnFinished()
        {
            if (_isReady)
            {
                GameController.Logger.Log("AwaitingPlayersState Players are ready");
                SwitchState(EGameState.Init);
            }
            else
            {
                GameController.Logger.Log("There are no players, destroying game");
            }
        }
    }
}
