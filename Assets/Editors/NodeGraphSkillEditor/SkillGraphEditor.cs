using AbilitySystem.Variables;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors.NodeGraphSkillEditor
{
    public class SkillGraphEditor : EditorWindow
    {
        private NodeBuilder _nodeBuilder;
        private ConnectionBuilder _connectionBuilder;
        private VariableMenu _variableMenu;

        private Rect _varsPosition;

        public static SkillGraphEditor OpenWindow(AbilityContaineer ability)
        {
            SkillGraphEditor window = GetWindow<SkillGraphEditor>("Skill Graph Editor");
            window.Initialize(ability);
            return window;
        }

        private void Initialize(AbilityContaineer ability)
        {
            _varsPosition = new Rect(0, 0, 300, 500);
            _variableMenu = new VariableMenu();
            _variableMenu.Initialize(ability);
            _nodeBuilder = new NodeBuilder(_variableMenu);
            _nodeBuilder.Initialize(ability);
            _connectionBuilder = new ConnectionBuilder(_nodeBuilder);
            _connectionBuilder.Initialize(ability);
        }

        private void OnGUI()
        {
            _nodeBuilder.Draw();
            _connectionBuilder.Draw();

            BeginWindows();
            _varsPosition = GUI.Window(1, _varsPosition, (id) => {
                _variableMenu.Draw();
                GUI.DragWindow();
                _variableMenu.SetPosition(_varsPosition);
            }, "Variable Menu");
            EndWindows();

            Event e = Event.current;
            _variableMenu.ProcessEvents(e);
            _nodeBuilder.ProcessEvents(e);
            _connectionBuilder.ProcessEvents(e);

            if (GUI.changed)
            {
                Repaint();
            }
        }
    }
}
