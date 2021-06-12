using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class ChangeVisibilityAction : AAction
    {
        public override EAction EAction => EAction.ChangeVisibility;

        public List<VisibilityChangeDefinition> Changes { get; private set; }

        public new ChangeVisibilityAction Initialize()
        {
            base.Initialize();

            return this;
        }

        protected override void ApplyImplementation()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
