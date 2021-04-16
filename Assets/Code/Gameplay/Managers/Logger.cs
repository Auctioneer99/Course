using UnityEngine;

namespace Gameplay
{
    public class Logger
    {
        public string Color;

        public Logger(string color)
        {
            Color = color;
        }

        public void Log(string message)
        {
            Debug.Log($"<color={Color}>{message}</color>");
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);

        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }

        public void Log(string message, string color)
        {
            Debug.Log($"<color={color}>{message}</color>");
        }
    }
}
