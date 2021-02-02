using UnityEngine;

public class UnityLogger : ILogger
{
    private string _owner;
    private string _decoration;
    public UnityLogger(string owner, string color)
    {
        _owner = owner;
        _decoration = $"<color={color}>{{0}}</color>";
    }

    public void Log(string message)
    {
        Debug.LogFormat(_decoration, $"{_owner} : {message}");
    }
}
