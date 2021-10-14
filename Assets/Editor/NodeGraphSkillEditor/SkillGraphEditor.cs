using AbilitySystem.Variables;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public class SkillGraphEditor : EditorWindow
    {
        private NodeBuilder _nodeBuilder;
        private ConnectionBuilder _connectionBuilder;

        [MenuItem("Window/Skill Graph Editor")]
        private static void OpenWindow()
        {
            SkillGraphEditor window = GetWindow<SkillGraphEditor>();
            window.Initialize();
            window.titleContent = new GUIContent("Skill Graph Editor");
        }

        private void Initialize()
        {
            _nodeBuilder = new NodeBuilder();
            _nodeBuilder.Initialize();
            _connectionBuilder = new ConnectionBuilder(_nodeBuilder);
            _connectionBuilder.Initialize();
        }

        private void OnGUI()
        {
            _nodeBuilder.Draw();
            _connectionBuilder.Draw();

            _nodeBuilder.ProcessEvents(Event.current);
            _connectionBuilder.ProcessEvents(Event.current);

            if (GUI.changed)
            {
                Repaint();
            }
        }
    }
}
