using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct VisibilityChangeDefinition
    {
        public ushort Card;
        public ECardVisibility TargetVisibility;
        public EPlayer Target;
        public bool ShouldBeHiddenForClient;

        public VisibilityChangeDefinition(ushort card, ECardVisibility targetVisibility, EPlayer target, bool shouldBeHidden)
        {
            Card = card;
            TargetVisibility = targetVisibility;
            Target = target;
            ShouldBeHiddenForClient = shouldBeHidden;
        }
    }
}
