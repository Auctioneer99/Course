using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Editors.NodeGraphSkillEditor
{
    public class ConnectionBuilder
    {
        public event Action<ConnectionView> Created;
        public event Action<ConnectionView> Deleted;

        private NodeBuilder _nodeBuilder;

        private List<ConnectionView> _connections;
        private VariableInputView _selectedInput;
        private VariableOutputView _selectedOutput;

        public ConnectionBuilder(NodeBuilder nodeBuilder)
        {
            _nodeBuilder = nodeBuilder;
        }

        public void Initialize()
        {
            _nodeBuilder.NodeCreated += OnNodeCreated;
            _nodeBuilder.NodeDeleted += OnNodeDeleted;

            _connections = new List<ConnectionView>();
        }

        public void Draw()
        {
            foreach(var connection in _connections)
            {
                connection.Draw();
            }
        }

        public void ProcessEvents(Event e)
        {
            
        }

        private void OnNodeCreated(NodeView node)
        {
            foreach (var input in node.InputsView)
            {
                input.Click += OnVariableInputClick;
            }
            foreach (var output in node.OutputsView)
            {
                output.Click += OnVariableOutputClick;
            }
        }
        private void OnNodeDeleted(NodeView node)
        {
            foreach (var input in node.InputsView)
            {
                input.Click -= OnVariableInputClick;
            }
            foreach (var output in node.OutputsView)
            {
                output.Click -= OnVariableOutputClick;
            }
        }

        private void OnVariableInputClick(VariableInputView input)
        {
            if (TryGetConnectionForInput(input, out var connection))
            {
                RemoveConnection(connection);
            }

            if (_selectedInput == input)
            {
                _selectedInput = null;
                return;
            }
            _selectedInput = input;

            if (_selectedOutput != null)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
        }
        private void OnVariableOutputClick(VariableOutputView output)
        {
            if (_selectedOutput == output)
            {
                _selectedOutput = null;
                return;
            }
            _selectedOutput = output;

            if (_selectedInput != null)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
        }
        private void CreateConnection()
        {
            ConnectionView connection = new ConnectionView(_selectedInput, _selectedOutput);
            connection.Initialize();
            connection.Click += RemoveConnection;
            _connections.Add(connection);
            Created?.Invoke(connection);
        }
        private void ClearConnectionSelection()
        {
            _selectedInput = null;
            _selectedOutput = null;
        }

        private void RemoveConnection(ConnectionView connection)
        {
            _connections.Remove(connection);
            connection.Click -= RemoveConnection;
            Deleted?.Invoke(connection);
            GUI.changed = true;
        }

        private bool TryGetConnectionForInput(VariableInputView input, out ConnectionView connection)
        {
            connection = _connections.Where(c => c.Input == input).FirstOrDefault();
            return connection != null;
        }
    }
}
