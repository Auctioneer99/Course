using AbilitySystem.Variables;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors.NodeGraphSkillEditor
{
    public class VariableOutputView
    {
        public event Action<VariableOutputView> Click;

        public Rect Rect
        {
            get
            {
                Rect target = _rect;
                var parent = _parent.Rect;
                target.x = parent.x + _rect.x - _rect.width / 2;
                target.y = parent.y + _rect.y - _rect.height / 2;
                return target;
            }
        }

        public GUIStyle Style => GraphStyle.VariableInputStyle;

        public IIndependentSource Variable { get; private set; }

        private IPositionProvider _parent;
        private Rect _rect;
        private Texture2D _inputTexture;

        public VariableOutputView(IPositionProvider parent, Vector2 position, IIndependentSource variable)
        {
            _inputTexture = EditorGUIUtility.Load("VariableInput.png") as Texture2D;
            _parent = parent;
            _rect = new Rect(position.x, position.y, 24, 24);
            Variable = variable;
        }

        public void Draw()
        {
            if (GUI.Button(Rect, _inputTexture, GUIStyle.none))
            {
                Click?.Invoke(this);
            }
        }
    }
}
