using AbilitySystem.Variables;
using Gameplay;
using UnityEditor;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public class IntView : VariableView
    {
        public override Variable Variable => _variable;

        private Variable<int> _variable;

        public IntView()
        {
            _variable = new Variable<int>("New", 0);
        }

        public override ANode CreateGetterNode()
        {
            var node = new GetNode<int>(_variable);
            node.Initialize();
            return node;
        }

        public override void DrawEditing()
        {
            base.DrawEditing();
            _variable.Value = EditorGUILayout.IntField(_variable.Value);
        }
    }
}
