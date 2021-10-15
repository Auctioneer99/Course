using AbilitySystem.Variables;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public class SkillGraphEditor : EditorWindow
    {
        private EditorGUISplitView _horizontalSplit;


        private NodeBuilder _nodeBuilder;
        private ConnectionBuilder _connectionBuilder;
        private VariableMenu _variableMenu;

        private Rect _varsPosition;

        [MenuItem("Custom/Skill Graph Editor")]
        private static void OpenWindow()
        {
            SkillGraphEditor window = GetWindow<SkillGraphEditor>("Skill Graph Editor");
            window.Initialize();
        }

        private void Initialize()
        {
            _horizontalSplit = new EditorGUISplitView(EditorGUISplitView.Direction.Horizontal);
            _varsPosition = new Rect(0, 0, 300, 500);
            _variableMenu = new VariableMenu();
            _variableMenu.Initialize();
            _nodeBuilder = new NodeBuilder(_variableMenu);
            _nodeBuilder.Initialize();
            _connectionBuilder = new ConnectionBuilder(_nodeBuilder);
            _connectionBuilder.Initialize();
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
