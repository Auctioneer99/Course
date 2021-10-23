using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public interface TriggerableEntity
    {
        AbilityData AbilityData { get; }

        ELocation Location { get; }
    }

    public sealed class AbilityData
    {
        private HashSet<APassiveTrigger> _passiveTriggers;

        public AbilityData(IEnumerable<APassiveTrigger> triggers)
        {
            _passiveTriggers = new HashSet<APassiveTrigger>();
            foreach(var t in triggers)
            {
                _passiveTriggers.Add(t);
            }
        }

        public IEnumerable<APassiveTrigger> GetTriggers(ETriggerType type)
        {
            return _passiveTriggers.Where(t => t.TriggerType == type);
        }
    }
}
