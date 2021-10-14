using AbilitySystem.Variables;
using System.Collections.Generic;

namespace Gameplay
{
    public sealed class GameStartedTriggerInfo : ATriggerInfo
    {
        public override ETriggerType TriggerType => ETriggerType.GameStarted;

        public override bool IsInterruption => true;
    }

    public sealed class GameStartedTrigger : ATrigger<GameStartedTriggerInfo>
    {
        public override ETriggerType TriggerType => ETriggerType.GameStarted;

        public override bool Execute()
        {
            return true;
        }

        public override void Setup(GameStartedTriggerInfo info)
        {
            
        }

        protected override bool CanTrigger(TriggerableEntity entity, GameStartedTriggerInfo trigger)
        {
            throw new System.NotImplementedException();
        }

        protected override void PopulateOutputs(List<IIndependentSource> list)
        {
            
        }
    }
}
