using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class SetupBattlefieldAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SetupBattlefield;

        public BattlefieldSettings Settings { get; private set; }

        public SetupBattlefieldAction Initialize(BattlefieldSettings settings)
        {
            Initialize();

            Settings = settings;
            return this;
        }

        protected override void ApplyImplementation()
        {
            GameController.BoardManager.Initialize(Settings);
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
            Settings = (copyFrom as SetupBattlefieldAction).Settings;
        }
    }
}
