using AbilitySystem.Variables;
using Gameplay;
using UnityEditor;

namespace Assets.Editors.NodeGraphSkillEditor
{
    public abstract class VariableView
    {
        public abstract Variable Variable { get; }

        public virtual void DrawEditing()
        {
            EditorGUILayout.LabelField("Changing Variable");
            EditorGUILayout.LabelField("Name:");
            Variable.Name = EditorGUILayout.TextField(Variable.Name);
        }

        public abstract ANode CreateGetterNode();
    }
}
