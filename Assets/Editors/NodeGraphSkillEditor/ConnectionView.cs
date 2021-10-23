using AbilitySystem.Variables;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors.NodeGraphSkillEditor
{
    public class ConnectionView
    {
        public event Action<ConnectionView> Click;

        public VariableInputView Input { get; private set; }
        public VariableOutputView Output { get; private set; }
        private Color _color = Color.black;

        public ConnectionView(VariableInputView input, VariableOutputView output)
        {
            Input = input;
            Output = output;
        }

        public void Initialize()
        {
            if (Input.Variable.GetVarType() == Output.Variable.GetVarType())
            {
                Input.Variable.RawValueSource = Output.Variable;
                _color = Color.green;
            }
            else
            {
                _color = Color.red;
            }
        }

        public void Draw()
        {
            Handles.DrawBezier(
                Input.Rect.center,
                Output.Rect.center,
                Input.Rect.center + Vector2.left * 50f,
                Output.Rect.center - Vector2.left * 50f,
                _color,
                null,
                2f
            );
            if (Handles.Button((Input.Rect.center + Output.Rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
            {
                Click?.Invoke(this);
            }
        }
    }
}
