using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PlayerPrepareAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.PreparePlayer;

        public EPlayer EPlayer { get; private set; }
        public bool Preparation { get; private set; }

        public PlayerPrepareAction Initialize(EPlayer player, bool toPrepare)
        {
            Initialize();
            EPlayer = player;
            Preparation = toPrepare;
            return this;
        }

        protected override void ApplyImplementation()
        {
            if (Preparation)
            {
                GameController.PlayerManager.GetPlayer(EPlayer).Prepare();
            }
            else
            {
                GameController.PlayerManager.GetPlayer(EPlayer).Unprepare();
            }
        }

        protected override void AttributesFrom(Packet packet)
        {
            EPlayer = packet.ReadEPlayer();
            Preparation = packet.ReadBool();
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(EPlayer)
                .Write(Preparation);
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            PlayerPrepareAction other = copyFrom as PlayerPrepareAction;
            EPlayer = other.EPlayer;
            Preparation = other.Preparation;
        }
    }
}
