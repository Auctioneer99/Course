using AbilitySystem.Variables;
using System.Collections.Generic;

namespace Gameplay
{
    public abstract class ANode
    {
        public IEnumerable<InputVariable> Inputs => _inputs;
        public IEnumerable<IIndependentSource> Outputs => _outputs;

        private List<InputVariable> _inputs;
        private List<IIndependentSource> _outputs;

        public void Initialize()
        {
            _inputs = new List<InputVariable>();
            PopulateInputs(_inputs);
            _outputs = new List<IIndependentSource>();
            PopulateOutputs(_outputs);
        }

        protected abstract void PopulateInputs(List<InputVariable> list);

        protected abstract void PopulateOutputs(List<IIndependentSource> list);
    }
}
