using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class UITimerController : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private UITimer _timerUI = null;

        private GameController _controller;

        private void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            StateTimer stateTimer = game.StateMachine.TimeManager.GetCurrentStateTimer();
            EventManager eManager = game.EventManager;

            if (stateTimer != null && stateTimer.Timer.IsRunning)
            {
                OnStateTimerStarted(stateTimer);
            }

            eManager.OnStateTimerStarted.VisualEvent.AddListener(OnStateTimerStarted);
            eManager.OnStateTimerElapsed.VisualEvent.AddListener(OnStateTimerElapsed);
        }

        public void Detach(GameController game)
        {
            _controller = null;
            _timerUI.UnInitialize();
        }

        public void OnGameEndedCleanup()
        {
            throw new NotImplementedException();
        }

        private void OnStateTimerStarted(StateTimer timer)
        {
            //_timerUI = timer;
            _timerUI.StateTimer = timer;
            _timerUI.ToggleTimer(true);
        }

        private void OnStateTimerElapsed(StateTimer timer)
        {
            _timerUI.ToggleTimer(false);
        }
    }
}
