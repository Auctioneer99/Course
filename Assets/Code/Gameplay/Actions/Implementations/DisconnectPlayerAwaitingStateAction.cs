using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class DisconnectPlayerAwaitingStateAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.DisconnectPlayerAwaitingState;

        public EPlayer EPlayer { get; private set; }

        public DisconnectPlayerAwaitingStateAction Initialize(EPlayer player)
        {
            Initialize();
            EPlayer = player;
            return this;
        }

        protected override void ApplyImplementation()
        {
            GameController.PlayerManager.DisconnectPlayer(EPlayer);
        }

        protected override void AttributesFrom(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void AttributesTo(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            EPlayer = (copyFrom as DisconnectPlayerAwaitingStateAction).EPlayer;
        }
    }
}
