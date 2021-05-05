using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public abstract class ASingleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                return _instance;
            }
        }
        private static T _instance = null;

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                throw new Exception("Only 1 sigleton awaible at any time");
            }
            _instance = this as T;
        }
    }
}
