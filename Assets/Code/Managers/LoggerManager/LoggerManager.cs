using UnityEngine;

namespace Gameplay
{
    public class LoggerManager : AManager
    {
        public LoggerManager(GameController controller) : base(controller)
        { }

        public void Log(string message)
        {
            Debug.Log(message);
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
