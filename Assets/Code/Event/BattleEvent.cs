using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public static class BattleEventExtension
    {
        public const int DEFAULT_PRIORITY = 0;
        public const int VISUAL_PRIORITY = 10000;
    }

    public class BattleEvent
    {
        public Event CoreEvent;
        public Event VisualEvent;

        public BattleEvent()
        {
            CoreEvent = new Event();
            VisualEvent = new Event();

            CoreEvent.AddListener(VisualEvent.Invoke, priority: BattleEventExtension.VISUAL_PRIORITY);
        }

        public void Invoke()
        {
            CoreEvent.Invoke();
        }
    }

    public class BattleEvent<T>
    {
        public Event<T> CoreEvent;
        public Event<T> VisualEvent;

        public BattleEvent()
        {
            CoreEvent = new Event<T>();
            VisualEvent = new Event<T>();

            CoreEvent.AddListener(VisualEvent.Invoke, priority: BattleEventExtension.VISUAL_PRIORITY);
        }

        public void Invoke(T arg)
        {
            CoreEvent.Invoke(arg);
        }
    }

    public class BattleEvent<T1, T2>
    {
        public Event<T1, T2> CoreEvent;
        public Event<T1, T2> VisualEvent;

        public BattleEvent()
        {
            CoreEvent = new Event<T1, T2>();
            VisualEvent = new Event<T1, T2>();

            CoreEvent.AddListener(VisualEvent.Invoke, priority: BattleEventExtension.VISUAL_PRIORITY);
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            CoreEvent.Invoke(arg1, arg2);
        }
    }

    public class BattleEvent<T1, T2, T3>
    {
        public Event<T1, T2, T3> CoreEvent;
        public Event<T1, T2, T3> VisualEvent;

        public BattleEvent()
        {
            CoreEvent = new Event<T1, T2, T3>();
            VisualEvent = new Event<T1, T2, T3>();

            CoreEvent.AddListener(VisualEvent.Invoke, priority: BattleEventExtension.VISUAL_PRIORITY);
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            CoreEvent.Invoke(arg1, arg2, arg3);
        }
    }
}
