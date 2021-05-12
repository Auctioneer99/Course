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

            return GameController.PlayerManager.GetPlayer(EPlayer).IsPrepared == false;
        }

        public AskPrepare Initialize(bool toPrepare)
        {
            Preparation = toPrepare;
            return this;
        }

        protected override void ApplyImplementation()
        {
            PlayerPrepareAction action = GameController.ActionFactory.Create<PlayerPrepareAction>()
                .Initialize(EPlayer, Preparation);
            GameController.ActionDistributor.Add(action);
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
    }
}
