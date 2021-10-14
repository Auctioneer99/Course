using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public interface TriggerableEntity
    {
        bool TryGetTriggers(ETriggerType type, List<ATrigger> triggers);

        ELocation Location { get; }
    }
}
