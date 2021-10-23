using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public abstract class AAbilityManager
    {
        private Dictionary<ETriggerType, List<TriggerableEntity>> _listeners;

        public abstract void Initialize();

        public void ExecuteTrigger<TriggerInfo>(TriggerInfo info) where TriggerInfo : ATriggerInfo
        {
            var triggers = new List<ATrigger>();
            foreach (var listener in _listeners)
            {
                triggers.Clear();
                //if (listener.TryGetTriggers(info.TriggerType, triggers))
                {
                //    foreach (var trigger in triggers)
                    {

                    }
                }
            }
        }


        private void AddAbilityInstance(TriggerableEntity entity, ATrigger trigger, ATriggerInfo info)
        {

        }
    }
}
