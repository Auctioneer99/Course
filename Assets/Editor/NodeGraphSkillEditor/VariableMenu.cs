using AbilitySystem.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public class VariableMenu : IPositionProvider
    {
        public IEnumerable<VariableView> Variables => _variables;

        public Rect Rect { get; private set; }
        private Rect _rect;

        private List<VariableView> _variables;
        private VariableView _editingVariable;
        private Vector2 _scrollPosition;

        public void Initialize()
        {
            _variables = new List<VariableView>();
        }

        public void Draw()
        {
            if (GUILayout.Button("Add variable"))
            {
                GenericMenu menu = new GenericMenu();
                PopulateAddMenu(menu);
                menu.ShowAsContext();
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.MaxHeight(300));
            foreach(var variable in _variables)
            {
                if (GUILayout.Button(variable.Variable.Name))
                {
                    _editingVariable = variable;
                }
            }
            EditorGUILayout.EndScrollView();
            if (_editingVariable != null)
            {
                _editingVariable.DrawEditing();
            }
        }

        private void PopulateAddMenu(GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Bool"), false, () => AddVariable(new BooleanView()));
            menu.AddItem(new GUIContent("Int"), false, () => AddVariable(new IntView()));
        }

        private void AddVariable(VariableView variable)
        {
            _variables.Add(variable);
        }

        public void ProcessEvents(Event e)
        {
            switch(e.type)
            {
                case EventType.MouseDown:
                    if (_rect.Contains(e.mousePosition))
                    {
                        e.Use();
                    }
                    break;
            }
        }

        public void SetPosition(Rect position)
        {
            if (_rect != position)
                Debug.Log(position);
            _rect = position;
        }
    }
}
