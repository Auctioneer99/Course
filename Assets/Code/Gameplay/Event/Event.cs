using System;
using UnityEngine;

namespace Gameplay
{
    public class Event : AEvent<Action>
    {
        public void Invoke()
        {
            DeferredRemoveListeners();
            DeferredSortListeners();
            isHandling = true;
            for (int i = 0, size = _size; i < size && i < _size; i++)
            {
                _listeners[i].Handler();
            }
            isHandling = false;
            DeferredRemoveListeners();
        }

        public override void AddListener(Action listener, bool singleCall = false, int priority = 1000)
        {
            if (singleCall)
            {
                AddListenerImpl(listener, () => { RemoveListener(listener); listener(); }, priority, false);
            }
            else
            {
                AddListenerImpl(listener, listener, priority, true);
            }
        }
    }

    public class Event<T> : AEvent<Action<T>>
    {
        public void Invoke(T arg)
        {
            DeferredRemoveListeners();
            DeferredSortListeners();
            isHandling = true;
            for (int i = 0, size = _size; i < size; i++)
            {
                _listeners[i].Handler(arg);
            }
            isHandling = false;
            DeferredRemoveListeners();
        }

        public void AddListener(Action<T> listener, bool singleCall = false, int priority = 1000)
        {
            if (singleCall)
            {
                AddListenerImpl(listener, (arg) => { RemoveListener(listener); listener(arg); }, priority, false);
            }
            else
            {
                AddListenerImpl(listener, listener, priority, true);
            }
        }

        public override void AddListener(Action listener, bool singleCall = false, int priority = 1000)
        {
            if (singleCall)
            {
                AddListenerImpl(listener, (arg) => { RemoveListener(listener); listener(); }, priority, false);
            }
            else
            {
                AddListenerImpl(listener, (arg) => listener(), priority, true);
            }
        }

        public void RemoveListener(Action<T> listener)
        {
            //throw new Exception("lol");
            RemoveListener(listener as Delegate);
        }
    }

    public class Event<T1, T2> : AEvent<Action<T1, T2>>
    {
        public void Invoke(T1 arg1, T2 arg2)
        {
            DeferredRemoveListeners();
            DeferredSortListeners();
            isHandling = true;
            for (int i = 0, size = _size; i < size && i < _size; i++)
            {
                _listeners[i].Handler(arg1, arg2);
            }
            isHandling = false;
            DeferredRemoveListeners();
        }

        public void AddListener(Action<T1, T2> listener, bool singleCall = false, int priority = 1000)
        {
            if (singleCall)
            {
                AddListenerImpl(listener, (arg1, arg2) => { RemoveListener(listener); listener(arg1, arg2); }, priority, false);
            }
            else
            {
                AddListenerImpl(listener, listener, priority, true);
            }
        }

        public override void AddListener(Action listener, bool singleCall = false, int priority = 1000)
        {
            if (singleCall)
            {
                AddListenerImpl(listener, (arg1, arg2) => { RemoveListener(listener); listener(); }, priority, false);
            }
            else
            {
                AddListenerImpl(listener, (arg1, arg2) => listener(), priority, true);
            }
        }

        public void RemoveListener(Action<T1, T2> listener)
        {
            RemoveListener(listener as Delegate);
        }
    }

    public class Event<T1, T2, T3> : AEvent<Action<T1, T2, T3>>
    {
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            DeferredRemoveListeners();
            DeferredSortListeners();
            isHandling = true;
            for (int i = 0, size = _size; i < size && i < _size; i++)
            {
                _listeners[i].Handler(arg1, arg2, arg3);
            }
            isHandling = false;
            DeferredRemoveListeners();
        }

        public void AddListener(Action<T1, T2, T3> listener, bool singleCall = false, int priority = 1000)
        {
            if (singleCall)
            {
                AddListenerImpl(listener, (arg1, arg2, arg3) => { RemoveListener(listener); listener(arg1, arg2, arg3); }, priority, false);
            }
            else
            {
                AddListenerImpl(listener, listener, priority, true);
            }
        }

        public override void AddListener(Action listener, bool singleCall = false, int priority = 1000)
        {
            if (singleCall)
            {
                AddListenerImpl(listener, (arg1, arg2, arg3) => { RemoveListener(listener); listener(); }, priority, false);
            }
            else
            {
                AddListenerImpl(listener, (arg1, arg2, arg3) => listener(), priority, true);
            }
        }

        public void RemoveListener(Action<T1, T2, T3> listener)
        {
            RemoveListener(listener as Delegate);
        }
    }
}
