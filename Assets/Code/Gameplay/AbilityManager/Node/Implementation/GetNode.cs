using AbilitySystem.Variables;
using System.Collections.Generic;

namespace Gameplay
{
    public sealed class GetNode<T> : IndependentNode
    {
        private IIndependentSource<T> _source;

        public GetNode(IIndependentSource<T> source)
        {
            _source = source;
        }

        protected override void PopulateInputs(List<InputVariable> list)
        {
            
        }

        protected override void PopulateOutputs(List<IIndependentSource> list)
        {
            list.Add(_source);
        }
    }
}
