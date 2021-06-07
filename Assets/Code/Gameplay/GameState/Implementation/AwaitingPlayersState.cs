using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class AwaitingPlayersState : AGameState
    {
        public AwaitingPlayersState(GameController controller) : base(controller, EGameState.AwaitingPlayers)
        { }

        public override void OnLeaveState(EGameState newStateId)
        {
            base.OnLeaveState(newStateId);
            //

        }

        protected override void SendWaitingForFinishedReport()
        {

        }

        protected override bool AreFinished()
        {
            return GameController.PlayerManager.AreAllPrepared();
        }

        protected override void OnFinished()
        {
            if (GameController.PlayerManager.AreAllPrepared())
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
