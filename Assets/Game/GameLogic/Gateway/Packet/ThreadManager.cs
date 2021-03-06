﻿using System;
using System.Collections.Generic;

public class ThreadManager
{
    private readonly List<Action> _executeOnMainThread = new List<Action>();
    private readonly List<Action> _executeCopiedOnMainThread = new List<Action>();
    private bool _actionToExecuteOnMainThread = false;

    public ThreadManager()
    {
    }

    public void ExecuteOnMainThread(Action _action)
    {
        if (_action == null)
        {
            return;
        }

        lock (_executeOnMainThread)
        {
            _executeOnMainThread.Add(_action);
            _actionToExecuteOnMainThread = true;
        }
    }

    public void Update()
    {
        if (_actionToExecuteOnMainThread)
        {
            _executeCopiedOnMainThread.Clear();
            lock (_executeOnMainThread)
            {
                _executeCopiedOnMainThread.AddRange(_executeOnMainThread);
                _executeOnMainThread.Clear();
                _actionToExecuteOnMainThread = false;
            }

            for (int i = 0; i < _executeCopiedOnMainThread.Count; i++)
            {
                _executeCopiedOnMainThread[i]();
            }
        }
    }
}