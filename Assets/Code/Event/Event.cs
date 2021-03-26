using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Event : AEvent<Action>
    {
        public void Invoke()
        {
            DeferredRemoveListeners();
            if (_isSorted == false)
            {
                Array.Sort(_listeners);
            }
            isHandling = true;
            for (int i = 0, size = _pointer; i < size && i < _pointer; i++)
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

        public void Invoke(T arg)
        {
            DeferredRemoveListeners();
            if (_isSorted == false)
            {
                Array.Sort(_listeners);
            }
            isHandling = true;
            for (int i = 0, size = _pointer; i < size && i < _pointer; i++)
            {
                _listeners[i].Handler(arg);
            }
            isHandling = false;
            DeferredRemoveListeners();
        }

        public void RemoveListener(Action<T> listener)
        {
            RemoveListener(listener);
        }
    }
}
