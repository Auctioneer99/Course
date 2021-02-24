using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIProvider
{
    private static List<UI> _uis = new List<UI>();

    public static void Register(UI ui)
    {
        _uis.Add(ui);
    }

    public static UI Get(int id)
    {
        return _uis[id];
    }
}
