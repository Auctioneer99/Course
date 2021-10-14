using AbilitySystem.Variables;
using System;
using UnityEngine;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public class VariableInputView
    {
        public event Action<VariableInputView> Click;

        public Rect Rect
        {
            get
            {
                Rect target = _rect;
                var parent = _parent.Rect;
                target.x = parent.x + _rect.x - _rect.width;
                target.y = parent.y + _rect.y - _rect.height;
                return target;
            }
        }

        public GUIStyle Style => GraphStyle.VariableInputStyle;

        public InputVariable Variable { get; private set; }

        private IPositionProvider _parent;
        private Rect _rect;

        public VariableInputView(IPositionProvider parent, Vector2 position, InputVariable variable)
        {
            _parent = parent;
            _rect = new Rect(position.x, position.y, 10, 10);
            Variable = variable;
        }

        public void Draw()
        {
            if (GUI.Button(Rect, "", Style))
            {
                Click?.Invoke(this);
            }
        }
    }
}
