using UnityEngine;

namespace Assets.Editor.NodeGraphSkillEditor
{
    public interface IPositionProvider
    {
        Rect Rect { get; }
    }
}
