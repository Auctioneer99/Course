using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public abstract class AGameState
    {
        public readonly EGameState EGameState;

        public GameController GameController { get; private set; }
        public StateTimer Timer { get; private set; }

        public bool HasTimer => Timer != null;
        public bool CanStartTimer => HasTimer && Timer.IsElapsed == false;
        public bool HasRunningTimer => HasTimer && Timer.IsRunning;
        public bool IsElapsed => HasTimer && Timer.IsElapsed;


        protected AGameState(GameController controller, EGameState state)
        {
            GameController = controller;
            EGameState = state;
        }

        public virtual void OnEnterState(EGameState prevState)
        {
            if (GameController.HasAuthority && EGameState != EGameState.End)
            {
                bool reseted = TryResetTimer();
                EPlayerStatus playerStatus = reseted ? EPlayerStatus.Loading : EPlayerStatus.Active;
                GameController.PlayerManager.SetAllPlayersStatus(playerStatus);
                TryStartTimer();
            }
        }

        public virtual void OnLeaveState(EGameState newStateId)
        {
            Timer?.Reset();
        }

        public virtual void Update()
        {

        }

        public void SetupTimer(StateTimer timer)
        {
            Timer?.OnElapsed.CoreEvent.RemoveListener(OnTimerElapsed);

            Timer = timer;
            timer.OnElapsed.CoreEvent.AddListener(OnTimerElapsed);
        }

        private void OnTimerElapsed(StateTimer timer)
        {
            if (GameController.HasAuthority)
            {
                GameController.PlayerManager.SetAllPlayersStatus(EPlayerStatus.Blocked);
            }
            GameController.EventManager.OnStateTimerElapsed.Invoke(timer);
        }

        private void StopTimer()
        {
            if (GameController.HasAuthority && HasRunningTimer)
            {
                TimerElapsedAction action = GameController.ActionFactory.Create<TimerElapsedAction>()
                    .Initialize(EGameState);
                GameController.ActionDistributor.Add(action);
            }
        }

        private bool TryStartTimer()
        {
            if (GameController.HasAuthority && CanStartTimer)
            {
                TimerStartedAction action = GameController.ActionFactory.Create<TimerStartedAction>().
                    Initialize(EGameState);
                GameController.ActionDistributor.HandleAction(action);
                return true;
            }
            return false;
        }

        private bool TryResetTimer()
        {
            if (CanStartTimer)
            {
                Timer.Reset();
                StartWaitingReport();
                return true;
            }
            return false;
        }

        private void StartWaitingReport()
        {

        }

        protected abstract void OnFinished();
    }
}
