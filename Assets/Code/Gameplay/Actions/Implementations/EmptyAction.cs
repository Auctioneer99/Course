using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class EmptyAction : AAction, IAuthoritySideAction
    {
        public override EAction EAction => EAction.Empty;

        public override bool IsValid()
        {
            throw new NotImplementedException();
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
