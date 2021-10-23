using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Assets.Editors.NodeGraphSkillEditor
{
    public static class GraphStyle
    {
        public static GUIStyle GetterNodeStyle
        {
            get
            {
                if (_getterNodeStyle == null)
                {
                    _getterNodeStyle = new GUIStyle();
                    _getterNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
                    _getterNodeStyle.border = new RectOffset(12, 12, 12, 12);
                }
                return _getterNodeStyle;
            }
        }
        private static GUIStyle _getterNodeStyle;

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
                    _variableInput.border = new RectOffset(5, 5, 10, 10);
                }
                return _variableInput;
            }
        }
        private static GUIStyle _variableInput;

        public static GUIStyle VariableInputConnectedStyle
        {
            get
            {
                if (_variableConnectedInput == null)
                {
                    _variableConnectedInput = new GUIStyle();
                    _variableConnectedInput.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
                    _variableConnectedInput.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
                    _variableConnectedInput.border = new RectOffset(4, 4, 12, 12);
                }
                return _variableConnectedInput;
            }
        }
        private static GUIStyle _variableConnectedInput;
    }
}
