using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Gameplay
{
    public static class EActionExt
    {
        private static Dictionary<int, Type> _enumToType = new Dictionary<int, Type>();

        static EActionExt()
        {
            RegisterActions();
#if UNITY_EDITOR
            VerifyMappings();
#endif
        }

        public static void Write(this Packet packet, EAction action)
        {

        }

        public static EAction ReadEAction(this Packet packet)
        {

        }

        public static Type GetActionType(this EAction action)
        {
            _enumToType.TryGetValue((int)action, out Type type);
            return type;
        }

#if UNITY_EDITOR
        public static void VerifyMappings()
        {
            EAction[] actions = (EAction[])Enum.GetValues(typeof(EAction));

            for (int i = 0; i < actions.Length; i++)
            {
                EAction action = actions[i];
                if (!_enumToType.ContainsKey((int)action))
                {
                    Debug.LogError(string.Format("Missing Type for ActionID: {0}, {1}", action.ToString(), action));
                }
            }
        }
#endif

        private static void Register<T>(EAction action) where T: APlayerAction
        {
            _enumToType[(int)action] = typeof(T);
        }

        private static void RegisterActions()
        {
            Register<ChangeGameStateAction>(EAction.ChangeGameState);
        }
    }
}
