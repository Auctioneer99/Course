using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public sealed class AbilityInstance
    {
        public int Id { get; private set; }

        public ATrigger StartTrigger { get; private set; }

        public ATriggerInfo StartTriggerInfo { get; private set; } 
    }
}
