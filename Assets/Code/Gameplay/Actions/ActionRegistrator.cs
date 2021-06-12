using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Gameplay
{
    public static class ActionRegistrator
    {
        private static Dictionary<int, Type> _enumToType = new Dictionary<int, Type>();

        static ActionRegistrator()
        {
            RegisterActions();
#if UNITY_EDITOR
            VerifyMappings();
#endif
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
                    Debug.LogError(string.Format("Missing Type for Action: {0}, {1}", action.ToString(), action));
                }
            }
        }
#endif

        private static void Register<T>(EAction action) where T: AAction
        {
            _enumToType[(int)action] = typeof(T);
        }

        private static void RegisterActions()
        {
            Register<EmptyAction>(EAction.Empty);
            Register<SwitchGameStateAction>(EAction.SwitchGameState);

            Register<ConnectInitializationAction>(EAction.ConnectInitialization);
            Register<PingAction>(EAction.Ping);
            Register<SetupPlayerAction>(EAction.SetupPlayer);
            Register<DisconnectPlayerAwaitingStateAction>(EAction.DisconnectPlayerAwaitingState);
            Register<SetupPlayerDeckAction>(EAction.SetupPlayerDeck);
            Register<SyncTimersSettingsAction>(EAction.SyncTimersSettings);
            Register<TimerStartedAction>(EAction.TimerStarted);
            Register<TimerElapsedAction>(EAction.TimerElapsed);

            Register<RequestPlayerFinishedReport>(EAction.RequestPlayerFinished);
            Register<ReportPlayerStatusAction>(EAction.ReportPlayerStatus);
            Register<SetPlayerStatusAction>(EAction.SetPlayerStatus);
            Register<PlayerPrepareAction>(EAction.PreparePlayer);

            Register<AskJoinAction>(EAction.AskJoinPlayer);
            Register<AskPrepare>(EAction.AskPrepare);
            Register<AskDisconnectPlayerAwaitingStateAction>(EAction.AskDisconnectPlayerAwaitingState);

            Register<SetupBattlefieldAction>(EAction.SetupBattlefield);
            Register<SpawnCardsAction>(EAction.SpawnCards);

            Register<AddPauseAction>(EAction.AddPause);
            Register<RemovePauseAction>(EAction.RemovePause);

            Register<MoveAction>(EAction.Move);
            Register<SyncCardsAction>(EAction.SyncCards);
            Register<ChangeVisibilityAction>(EAction.ChangeVisibility);
        }
    }
}
