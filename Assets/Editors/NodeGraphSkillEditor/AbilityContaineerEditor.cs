using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors.NodeGraphSkillEditor
{
    [CustomEditor(typeof(AbilityContaineer))]
    public class AbilityContaineerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Edit"))
            {
                var editor = SkillGraphEditor.OpenWindow();
                var ability = target as AbilityContaineer;
            }
        }
    }
}
