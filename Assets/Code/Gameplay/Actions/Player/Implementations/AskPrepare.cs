using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class AskPrepare : APlayerAction, IUserAction
    {
        public override EAction EAction => EAction.AskPrepare;

        public bool ValidWhenBlocked => true;

        public bool Preparation { get; private set; }

        public override bool IsValid()
        {
            if (base.IsValid() == false)
            {
                return false;
            }
            if (GameController.HasAuthority)
            {
                return GameController.PlayerManager.GetPlayer(EPlayer).IsPrepared != Preparation &&
                    GameController.StateMachine.ECurrentState == EGameState.AwaitingPlayers;
            }
            else
            {
                return GameController.PlayerManager.GetPlayer(GameController.PlayerManager.LocalUserId)?.IsPrepared != Preparation &&
                    GameController.StateMachine.ECurrentState == EGameState.AwaitingPlayers;
            }
        }

        public AskPrepare Initialize(bool toPrepare)
        {
            Initialize();
            Preparation = toPrepare;
            return this;
        }

        protected override void ApplyImplementation()
        {
            if (GameController.HasAuthority)
            {
                PlayerPrepareAction action = GameController.ActionFactory.Create<PlayerPrepareAction>()
                    .Initialize(EPlayer, Preparation);
                GameController.ActionDistributor.Add(action);
            }
        }

        protected override void PlayerAttributesFrom(Packet packet)
        {
            Preparation = packet.ReadBool();
        }

        protected override void PlayerAttributesTo(Packet packet)
        {
            packet.Write(Preparation);
        }

        protected override void PlayerCopyImplementation(APlayerAction copyFrom, GameController controller)
        {
            Preparation = (copyFrom as AskPrepare).Preparation;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($"Player = {EPlayer}");
            sb.AppendLine($"Preparation = {Preparation}");
            return sb.ToString();
        }
    }
}
