using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public abstract class ATriggerInfo
    {
        public abstract ETriggerType TriggerType { get; }

        public abstract bool IsInterruption { get; }

        protected ATriggerInfo()
        {

        }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
