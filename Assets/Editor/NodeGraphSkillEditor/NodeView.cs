using Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public class NodeView : IPositionProvider
    {
        public event Action<NodeView> Click;

        public Rect Rect => _rect;
        public string Title { get; private set; }

        public IEnumerable<VariableInputView> InputsView => _inputsView;
        public IEnumerable<VariableOutputView> OutputsView => _outputsView;

        public GUIStyle Style { get; private set; }

        private ANode _node;
        private VariableInputView[] _inputsView;
        private VariableOutputView[] _outputsView;
        private Rect _rect;
        private Color _nodeColor = new Color(227 / 255f, 38 / 255f, 54 / 255f);

        public NodeView(ANode node, GUIStyle style, Vector2 position, Vector2 size)
        {
            //Texture2D scaled = new Texture2D((int)position.x, (int)position.y, TextureFormat.RGB24, true);
            //Graphics.ConvertTexture(image, scaled);
            //image = scaled;
            Style = style;
            _node = node;
            _rect = new Rect(position, new Vector2(300, 0));
        }

        public void Initialize()
        {
            
            _inputsView = new VariableInputView[_node.Inputs.Count()];
            var i = 0;
            foreach(var input in _node.Inputs)
            {
                var position = new Vector2(0, i * 30 + 30);
                _inputsView[i] = new VariableInputView(this, position, input);
                i++;
            }

            _outputsView = new VariableOutputView[_node.Outputs.Count()];
            i = 0;
            foreach (var output in _node.Outputs)
            {
                var position = new Vector2(Rect.width, i * 30 + 30);
                _outputsView[i] = new VariableOutputView(this, position, output);
                i++;
            }
            _rect.height = 50 + Math.Max(_outputsView.Length, _inputsView.Length) * 30;
        }

        public void Draw()
        {
            EditorGUI.DrawRect(Rect, _nodeColor);
            DrawVariables();

            //GUI.DrawTexture(Rect, image, ScaleMode.StretchToFill);
            //GUI.Box(Rect, new GUIContent(image), Style);
        }

        private void DrawVariables()
        {
            foreach (var input in _inputsView)
            {
                input.Draw();
            }
            foreach (var output in _outputsView)
            {
                output.Draw();
            }
        }

        public void ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (Rect.Contains(e.mousePosition))
                        {
                            Click?.Invoke(this);
                        }
                    }
                    break;
            }
        }

        public void Move(Vector2 delta)
        {
            _rect.position += delta;
        }
    }
}
