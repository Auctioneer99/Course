using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    class AskDisconnectPlayerAwaitingStateAction : APlayerAction, IUserAction
    {
        public override EAction EAction => EAction.AskDisconnectPlayerAwaitingState;

        public bool ValidWhenBlocked => true;

        public override bool IsValid()
        {
            if (base.IsValid() == false)
            {
                return false;
            }

            return GameController.StateMachine.ECurrentState == EGameState.AwaitingPlayers;
        }

        public new AskDisconnectPlayerAwaitingStateAction Initialize()
        {
            base.Initialize();
            return this;
        }

        protected override void ApplyImplementation()
        {
            if (GameController.HasAuthority)
            {
                Player player = GameController.PlayerManager.GetPlayer(EPlayer);
                if (player != null)
                {
                    var action = GameController.ActionFactory.Create<DisconnectPlayerAwaitingStateAction>()
                        .Initialize(EPlayer);
                    GameController.ActionDistributor.Add(action);
                }
            }
        }

        protected override void PlayerAttributesFrom(Packet packet)
        {
        }

        protected override void PlayerAttributesTo(Packet packet)
        {
        }

        protected override void PlayerCopyImplementation(APlayerAction copyFrom, GameController controller)
        {
            
        }
    }
}
