using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public static class Extensions
    {
        public static void Copy<T>(this List<T> list, List<T> other)
        {
            int otherCount = other.Count;
            list.Clear();
            for (int x = 0; x < otherCount; x++)
            {
                list.Add(other[x]);
            }
        }

        public static List<T> Clone<T>(this List<T> list, GameController controller)
            where T : IStateObjectCloneable<T>
        {
            List<T> result = new List<T>();
            foreach(var item in list)
            {
                result.Add(item.Clone(controller));
            }
            return result;
        }

        public static List<T> Clone<T>(this List<T> list)
            where T : ICloneable<T>
        {
            List<T> result = new List<T>();
            foreach (var item in list)
            {
                result.Add(item.Clone());
            }
            return result;
        }

        public static Dictionary<Key, T> Clone<Key, T>(this Dictionary<Key, T> list, GameController controller)
            where T : IStateObjectCloneable<T>
        {
            Dictionary<Key, T> result = new Dictionary<Key, T>();
            foreach (var item in list)
            {
                T value = default(T);
                if (item.Value != null)
                {
                    value = item.Value.Clone(controller);
                }
                result.Add(item.Key, value);
            }
            return result;
        }

        public static Dictionary<Key, T> Clone<Key, T>(this Dictionary<Key, T> list)
            where T : ICloneable<T>
        {
            Dictionary<Key, T> result = new Dictionary<Key, T>();
            foreach (var item in list)
            {
                T value = default(T);
                if (item.Value != null)
                {
                    value = item.Value.Clone();
                }
                result.Add(item.Key, value);
            }
            return result;
        }
    }
}
