using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class MulliganGameState : ASimultaneousGameState
    {
        public MulliganGameState(GameController controller) : base(controller, EGameState.Mulligan) { }

        public override void OnEnterState(EGameState prevState)
        {
            base.OnEnterState(prevState);

            if (GameController.HasAuthority)
            {
                foreach (var p in GameController.PlayerManager.Players.Keys)
                {
                    HandlePlayerMulligan(p);
                }
            }
        }

        private void HandlePlayerMulligan(EPlayer player)
        {
            //Debug.Log("RECEIVING MULLIGANS ACTIONS from " + player.ToString());
        }

        protected override void OnFinished()
        {
            SwitchState(EGameState.ChoosePlayer);
        }
    }
}
