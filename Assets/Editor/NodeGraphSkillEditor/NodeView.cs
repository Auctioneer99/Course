using Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public GUIStyle Style => GraphStyle.NodeStyle;

        private ANode _node;
        private VariableInputView[] _inputsView;
        private VariableOutputView[] _outputsView;
        private Rect _rect;

        public NodeView(ANode node, Vector2 position, Vector2 size)
        {
            _node = node;
            _rect = new Rect(position, size);
        }

        public void Initialize()
        {
            
            _inputsView = new VariableInputView[_node.Inputs.Count()];
            var i = 0;
            foreach(var input in _node.Inputs)
            {
                var position = new Vector2(0, (i + 2) * 10);
                _inputsView[i] = new VariableInputView(this, position, input);
                i++;
            }

            _outputsView = new VariableOutputView[_node.Outputs.Count()];
            i = 0;
            foreach (var output in _node.Outputs)
            {
                var position = new Vector2(Rect.width, (i + 2) * 10);
                _outputsView[i] = new VariableOutputView(this, position, output);
                i++;
            }
        }

        public void Draw()
        {
            DrawVariables();
            GUI.Box(Rect, Title, Style);
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
