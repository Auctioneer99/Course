using AbilitySystem.Variables;
using System.Collections.Generic;

namespace Gameplay
{
    public abstract class ATrigger : ANode, IFlowNode
    {
        public abstract ETriggerType TriggerType { get; }
        public InputVariable<ETriggerPriority> Priority { get; private set; }
        public InputVariable<ELocation> OwnerLocation { get; private set; }
        public OutputFlowConnector OutputFlow { get; private set; } = new OutputFlowConnector();

        public ATrigger()
        {
            Priority = new InputVariable<ETriggerPriority>("Priority", NullVarSource<ETriggerPriority>.Instance);
            OwnerLocation = new InputVariable<ELocation>("Owner Location", NullVarSource<ELocation>.Instance);
        }

        public abstract bool Execute();

        public abstract bool CanTrigger(TriggerableEntity entity, ATriggerInfo trigger);
    }

    public abstract class ATrigger<TriggerInfo> : ATrigger where TriggerInfo: ATriggerInfo
    {
        protected override void PopulateInputs(List<InputVariable> list)
        {
            list.Add(Priority);
        }

        public abstract void Setup(TriggerInfo info);

        public override bool CanTrigger(TriggerableEntity entity, ATriggerInfo trigger)
        {
            bool result = false;
            if (OwnerLocation.Value.Contains(entity.Location))
            {
                result = CanTrigger(entity, (TriggerInfo)trigger);
            }
            return result;
        }

        protected abstract bool CanTrigger(TriggerableEntity entity, TriggerInfo trigger);
    }
}
