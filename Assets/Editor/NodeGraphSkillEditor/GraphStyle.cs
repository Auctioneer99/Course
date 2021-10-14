using UnityEditor;
using UnityEngine;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public static class GraphStyle
    {
        public static GUIStyle NodeStyle
        {
            get
            {
                if (_nodeStyle == null)
                {
                    _nodeStyle = new GUIStyle();
                    _nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
                    _nodeStyle.border = new RectOffset(12, 12, 12, 12);
                }
                return _nodeStyle;
            }
        }
        private static GUIStyle _nodeStyle;

        public static GUIStyle VariableInputStyle
        {
            get
            {
                if (_variableInput == null)
                {
                    _variableInput = new GUIStyle();
                    _variableInput.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
                    _variableInput.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
                    _variableInput.border = new RectOffset(4, 4, 12, 12);
                }
                return _variableInput;
            }
        }
        private static GUIStyle _variableInput;
    }
}
