using AbilitySystem.Variables;
using System.Collections.Generic;

namespace Gameplay
{
    public class GetBoolNode : IndependentNode
    {
        private IIndependentSource<bool> _boolVar;

        public GetBoolNode(IIndependentSource<bool> source)
        {
            _boolVar = source;
        }

        protected override void PopulateInputs(List<InputVariable> list)
        {
            
        }

        protected override void PopulateOutputs(List<IIndependentSource> list)
        {
            list.Add(_boolVar);
        }
    }
}
