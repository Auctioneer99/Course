﻿using System;
using System.Collections.Generic;

namespace Gameplay.Events
{
    public abstract class AEvent<T0>
    {
        protected static readonly HandlerContainer.Comparer _comparer = new HandlerContainer.Comparer();

        protected struct HandlerContainer
        {
            public readonly Delegate Listener;
            public readonly T0 Handler;
            public readonly int Priority;

            public bool IsAlive => Listener != null;

            public HandlerContainer(Delegate listener, T0 handler, int priority)
            {
                Listener = listener;
                Handler = handler;
                Priority = priority;
            }

            public HandlerContainer(T0 handler)
            {
                Listener = null;
                Handler = handler;
                Priority = 0;
            }

            public int CompareTo(object obj)
            {
                if (obj is HandlerContainer other)
                {
                    return Priority.CompareTo(other.Priority);
                }
                return -1;
            }

            public class Comparer : IComparer<HandlerContainer>
            {
                public int Compare(HandlerContainer x, HandlerContainer y)
                {
                    if (x.IsAlive == false && y.IsAlive == false)
                    {
                        return 0;
                    }
                    if (x.IsAlive == false)
                    {
                        return 1;
                    }
                    if (y.IsAlive == false)
                    {
                        return -1;
                    }
                    return x.Priority.CompareTo(y.Priority);
                }
            }
        }


        protected bool isHandling = false;
        protected HandlerContainer[] _listeners = new HandlerContainer[1];
        protected int _size = 0;
        protected int _removedSize = 0;
        protected bool _isSorted = true;

        protected int IndexOf(Delegate listener)
        {
            if (listener == null)
            {
                return -1;
            }
            for (int i = 0; i < _size; i++)
            {
                if (_listeners[i].Listener == listener)
                {
                    return i;
                }
            }
            return -1;
        }

        public abstract void AddListener(Action listener, bool singleCall = false, int priority = 1000);

        protected void AddListenerImpl(Delegate listener, T0 handler, int priority, bool unique)
        {
            if (listener == null)
            {
                throw new Exception("null listener");
            }
            if (unique && IndexOf(listener) != -1)
            {
                return;
            }
            if (_listeners.Length == _size)
            {
                Array.Resize(ref _listeners, _listeners.Length * 2);
            }
            _listeners[_size++] = new HandlerContainer(listener, handler, priority);
            _isSorted = false;
        }

        public void RemoveListener(Delegate listener)
        {
            int index = IndexOf(listener);
            if (index == -1)
            {
                return;
            }
            if (isHandling)
            {
                _listeners[index] = new HandlerContainer(_listeners[index].Handler);
                ++_removedSize;
            }
            else
            {
                InstantRemoveListener(index);
            }
        }

        public void RemoveAllListeners()
        {
            if (isHandling)
            {
                for(int i = 0; i < _size; i++)
                {
                    _listeners[i] = new HandlerContainer(_listeners[i].Handler);
                }
                _removedSize = _size;
            }
            else
            {
                InstantRemoveAllListeners();
            }
        }

        public void RemoveListenerOfTarget(object target)
        {
            for (int i = 0; i < _size; i++)
            {
                Delegate listener = _listeners[i].Listener;
                if (!ReferenceEquals(listener, null) && ReferenceEquals(target, listener.Target))
                {
                    if (isHandling)
                    {
                        _listeners[i] = new HandlerContainer(_listeners[i].Handler);
                        ++_removedSize;
                    }
                    else
                    {
                        InstantRemoveListener(i);
                    }
                    return;
                }
            }
        }

        private void InstantRemoveAllListeners()
        {
            Array.Clear(_listeners, 0, _listeners.Length);
        }

        private void InstantRemoveListener(int index)
        {
            _isSorted = false;
            if (index != --_size)
            {
                Array.Copy(_listeners, index + 1, _listeners, index, _size - index);
            }
            _listeners[_size] = default(HandlerContainer);
        }

        protected void DeferredRemoveListeners()
        {
            if (_removedSize == _size)
            {
                InstantRemoveAllListeners();
            }
            else
            {
                for (int i = _size - 1; _removedSize > 0; i--)
                {
                    if (_listeners[i].IsAlive == false)
                    {
                        InstantRemoveListener(i);
                        --_removedSize;
                    }
                }
            }
        }

        protected void DeferredSortListeners()
        {
            if (_isSorted == false)
            {
                Array.Sort(_listeners, _comparer);
                _isSorted = true;
            }
        }
    }
}
