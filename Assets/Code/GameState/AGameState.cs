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
        public bool IsElapsed => HasTimer && Timer.IsElapsed;

        protected AGameState(GameController controller, EGameState state)
        {
            GameController = controller;
            EGameState = state;
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
                TryStopTimer();
            }
        }

        public virtual void OnEnterState(EGameState prevState)
        {
            if (GameController.HasAuthority && EGameState != EGameState.End)
            {
                EPlayerStatus playerStatus = (TryResetTimer() ? EPlayerStatus.Loading : EPlayerStatus.Active);
                GameController.PlayerManager.SetAllPlayersStatus(playerStatus);
                TryStartTimer();
            }
        }

        public virtual void OnLeaveState(EGameState newStateId)
        {
            if (HasTimer)
            {
                Timer.Reset();
            }
        }

        protected abstract void OnFinished();
    }
}
