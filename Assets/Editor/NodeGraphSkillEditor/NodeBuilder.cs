using AbilitySystem.Variables;
using Gameplay;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public class NodeBuilder
    {
        public event Action<NodeView> NodeCreated;
        public event Action<NodeView> NodeDeleted;

        private List<NodeView> _nodes;

        private NodeView _draggable;

        private VariableMenu _varMenu;

        public NodeBuilder(VariableMenu variableMenu)
        {
            _varMenu = variableMenu;
        }

        public void Initialize()
        {
            _nodes = new List<NodeView>();
        }

        public void Draw()
        {
            foreach (var n in _nodes)
            {
                n.Draw();
            }
        }

        public void ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                    }
                    break;
                case EventType.MouseUp:
                    if (e.button == 0)
                    {
                        _draggable = null;
                    }
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0)
                    {
                        if (_draggable == null)
                        {
                            OnDrag(e.delta);
                        }
                        else
                        {
                            OnDrag(_draggable, e.delta);
                        }
                        e.Use();
                    }
                    break;
            }
            ProcessNodeEvents(e);
        }

        private void ProcessNodeEvents(Event e)
        {
            foreach (var node in _nodes)
            {
                node.ProcessEvents(e);
            }
        }

        private void OnDrag(Vector2 delta)
        {
            foreach (var node in _nodes)
            {
                node.Move(delta);
            }
            GUI.changed = true;
        }

        private void OnDrag(NodeView node, Vector2 delta)
        {
            node.Move(delta);
            GUI.changed = true;
        }

        private void ProcessContextMenu(Vector2 position)
        {
            GenericMenu menu = new GenericMenu();
            PopulateMenuWithGetters(menu, position);
            menu.AddItem(new GUIContent("Add AndGate"),
                false,
                () => OnClickAndNode(position));
            menu.ShowAsContext();
        }

        private void PopulateMenuWithGetters(GenericMenu menu, Vector2 targetPosition)
        {
            string folder = "Getters/";
            foreach(var variable in _varMenu.Variables)
            {
                menu.AddItem(new GUIContent(folder + variable.Variable.Name),
                    false,
                    () => AddGetterNode(variable, targetPosition));
            }
        }

        private void AddGetterNode(VariableView variable, Vector2 position)
        {
            var node = variable.CreateGetterNode();
            var nodeView = new NodeView(node, GraphStyle.GetterNodeStyle, position, new Vector2(200, 70));
            InitializeNodeView(nodeView);
        }

        private void OnClickAndNode(Vector2 position)
        {
            var node = new AndLogicNode();
            node.Initialize();
            var nodeView = new NodeView(node, GraphStyle.NodeStyle, position, new Vector2(200, 50));
            InitializeNodeView(nodeView);
        }

        private void InitializeNodeView(NodeView node)
        {
            node.Initialize();
            _nodes.Add(node);
            NodeCreated?.Invoke(node);
            node.Click += OnNodeClick;
        }

        private void OnNodeClick(NodeView node)
        {
            _draggable = node;
        }

        private void DeleteNode(NodeView node)
        {
            node.Click -= OnNodeClick;
            _nodes.Remove(node);
            NodeDeleted?.Invoke(node);
        }
    }
}
