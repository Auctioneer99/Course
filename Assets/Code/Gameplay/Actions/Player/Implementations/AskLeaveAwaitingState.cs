using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    class AskLeaveAwaitingStateAction : APlayerAction, IUserAction
    {
        public override EAction EAction => EAction.AskLeaveAwaitingState;

        public bool ValidWhenBlocked => true;

        

        public override bool IsValid()
        {
            if (base.IsValid() == false)
            {
                return false;
            }

            return GameController.StateMachine.ECurrentState == EGameState.AwaitingPlayers;
        }

        protected override void ApplyImplementation()
        {
            if (GameController.HasAuthority)
            {
                Player player = GameController.PlayerManager.GetPlayer(EPlayer);
                if (player != null)
                {

                }
            }
        }

        protected override void PlayerAttributesFrom(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void PlayerAttributesTo(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void PlayerCopyImplementation(APlayerAction copyFrom, GameController controller)
        {
            throw new NotImplementedException();
        }
    }
}
