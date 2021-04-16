﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay.Unity
{
    public class GameListenerManager
    {
        public GameController Game { get; private set; }

        public bool IsProcessing { get; private set; }

        private List<GameListenerEntry> _listeners = new List<GameListenerEntry>();
        private List<GameListenerEntry> _toAdd = new List<GameListenerEntry>();
        private List<IGameListener> _toRemove = new List<IGameListener>();

        public void SetGame(GameController game)
        {
            if (game == Game)
            { 
                return; 
            }

            Game = game;

            IsProcessing = true;
            DetachListeners();

            if (Game == null)
            {
                return;
            }

            if(Game.IsInitialized)
            {
                AfterGameInitialized(game, false);
            }
            else
            {
                Game.EventManager.OnGameInitialized.VisualEvent.AddListener(HandleGameInitialized, true, 0);
            }
        }

        public void FlushChanges(bool wasJustInitialized)
        {

        }

        public void Add(IGameListener listener, int priority = 0)
        {
            GameListenerEntry entry = new GameListenerEntry(listener, priority);
            Add(entry, false);
        }

        private void Add(GameListenerEntry entry, bool wasJustInitialized)
        {
            if (IsProcessing)
            {
                _toAdd.Add(entry);
                return;
            }

            if (_listeners.Contains(entry))
            {
                throw new Exception("Already registred");
            }

            int length = _listeners.Count;
            int insertAt = -1;
            for (int i = 0; i < length; i++)
            {
                GameListenerEntry item = _listeners[i];
                if (item.Priority < entry.Priority)
                {
                    insertAt = i;
                    break;
                }
            }

            if (insertAt >= 0)
            {
                _listeners.Insert(insertAt, entry);
            }
            else
            {
                _listeners.Add(entry);
            }

            if(Game != null && Game.IsInitialized)
            {
                entry.Attach(Game, wasJustInitialized);
            }
        }

        public void Remove(IGameListener listener)
        {
            int length = _listeners.Count;
            for(int i = length - 1; i >= 0; i++)
            {
                GameListenerEntry entry = _listeners[i];
                if (entry.Listener == listener)
                {
                    if (IsProcessing)
                    {
                        entry.MarkedForRemove = true;
                        _toRemove.Add(listener);
                    }
                    else
                    {
                        entry.Detach();
                        _listeners.RemoveAt(i);
                    }
                    return;
                }
            }

            length = _toAdd.Count;
            for (int i = 0; i < length; i++)
            {
                if (_toAdd[i].Listener == listener)
                {
                    _toAdd.RemoveAt(i);
                    return;
                }
            }
        }


        private void DetachListeners()
        {
            int length = _listeners.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                GameListenerEntry entry = _listeners[i];
                if (entry.MarkedForRemove == false)
                {
                    entry.Detach();
                }
            }
        }

        private void AttachListeners(bool wasJustInitialized)
        {
            if (Game == null)
            { 
                return; 
            }
            foreach(var entry in _listeners)
            {
                if (entry.MarkedForRemove == false)
                {
                    entry.Attach(Game, wasJustInitialized);
                }
            }
        }

        private void HandleGameInitialized(GameController game)
        {
            AfterGameInitialized(game, true);
        }

        private void AfterGameInitialized(GameController game, bool wasJustInitialized)
        {
            if (game != Game)
            {
                return;
            }

            AttachListeners(wasJustInitialized);

            IsProcessing = false;

            FlushChanges(wasJustInitialized);

            if (Game != null)
            {
                //
            }
        }

        public void BattleEnded()
        {
            IsProcessing = true;
            foreach(var entry in _listeners)
            {
                entry.Listener.OnGameEndedCleanup();
            }
            IsProcessing = false;
            FlushChanges(false);
        }
    }
}
