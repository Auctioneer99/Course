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
        private UITimer _timerUI;

        private GameController _controller;

        private void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            Debug.Log("Timers attached");
            _controller = game;

            StateTimer timer = game.TimeManager.GetCurrentStateTimer();
            EventManager eManager = game.EventManager;

            Debug.Log("Is there is a timer?");
            if (timer != null && timer.IsRunning)
            {
                Debug.Log("There is!");
                OnStateTimerStarted(timer);
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
            _timerUI.Timer = timer;
            _timerUI.ToggleTimer(true);
        }

        private void OnStateTimerElapsed(StateTimer timer)
        {
            _timerUI.ToggleTimer(false);
        }
    }
}
