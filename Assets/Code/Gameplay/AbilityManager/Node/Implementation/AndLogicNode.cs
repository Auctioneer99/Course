using AbilitySystem.Variables;
using System.Collections.Generic;

namespace Gameplay
{
    public class AndLogicNode : IndependentNode
    {
        private InputVariable<bool> _first;
        private InputVariable<bool> _second;

        private DynamicVariable<bool> _output;

        public AndLogicNode()
        {
            _first = new InputVariable<bool>("1", NullVarSource<bool>.Instance);
            _second = new InputVariable<bool>("2", NullVarSource<bool>.Instance);

            _output = new DynamicVariable<bool>("AND", Calculate);
        }

        public AndLogicNode(Packet packet)
        {
            _first = new InputVariable<bool>("1", NullVarSource<bool>.Instance);
            _second = new InputVariable<bool>("2", NullVarSource<bool>.Instance);

            _output = new DynamicVariable<bool>("AND", Calculate);
        }

        private bool Calculate()
        {
            return _first.Value && _second.Value;
        }

        protected override void PopulateInputs(List<InputVariable> list)
        {
            list.Add(_first);
            list.Add(_second);
        }

        protected override void PopulateOutputs(List<IIndependentSource> list)
        {
            list.Add(_output);
        }
    }
}
