using AbilitySystem.Variables;
using Gameplay;
using UnityEditor;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public class BooleanView : VariableView
    {
        public override Variable Variable => _variable;

        private Variable<bool> _variable;

        public BooleanView()
        {
            _variable = new Variable<bool>("New", false);
        }

        public override ANode CreateGetterNode()
        {
            var node = new GetNode<bool>(_variable);
            node.Initialize();
            return node;
        }

        public override void DrawEditing()
        {
            base.DrawEditing();
            _variable.Value = EditorGUILayout.Toggle(_variable.Value);
        }
    }
}
