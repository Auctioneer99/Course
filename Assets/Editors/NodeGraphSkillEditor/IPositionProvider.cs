using UnityEngine;

namespace Assets.Editors.NodeGraphSkillEditor
{
    public interface IPositionProvider
    {
        Rect Rect { get; }
    }
}
