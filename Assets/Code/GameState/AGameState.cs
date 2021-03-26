using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public abstract class AGameState
    {
        public readonly EGameState GameState;

        public GameDirector GameDirector;

        private Action<int> _onTimerElapsed;

        protected AGameState(GameDirector director, EGameState state)
        {
            GameDirector = director;
            GameState = state;
        }

        public virtual void OnEnterState(EGameState prevState)
        {
            if (GameDirector.HasAuthority && GameState != EGameState.End)
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
