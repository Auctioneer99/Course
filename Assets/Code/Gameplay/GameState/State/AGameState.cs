﻿using UnityEngine;

namespace Gameplay
{
    public abstract class AGameState : IRuntimeStateObject<AGameState>, IRuntimeDeserializable
    {
        public EGameState EGameState { get; private set; }

        public GameController GameController { get; private set; }
        public StateTimer StateTimer { get; private set; }
        public Timer Timer => StateTimer?.Timer;

        public bool HasTimer => Timer != null;
        public bool CanStartTimer => HasTimer && Timer.IsElapsed == false;
        public bool HasRunningTimer => HasTimer && Timer.IsRunning;
        public bool IsElapsed => HasTimer && Timer.IsElapsed;

        protected AGameState(GameController controller, EGameState state)
        {
            GameController = controller;
            EGameState = state;
        }

        public virtual void Reset()
        {

        }

        public virtual void OnEnterState(EGameState prevState)
        {
            if (GameController.HasAuthority && EGameState != EGameState.EndGame)
            {
                //Timer?.Reset();

                GameController.PlayerManager.SetAllPlayersStatus(EPlayerStatus.Active);
                TryStartTimer();
                SendWaitingForFinishedReport();
            }
        }

        public virtual void OnLeaveState(EGameState newStateId)
        {
            TryStopTimer();
        }

        public virtual void Update()
        {
            if (GameController.HasAuthority)
            {
                //Debug.Log("[AGameState]");
                //Debug.Log(GameController.RequestHolder.HasRequests);
                //Debug.Log(GameController.ActionDistributor.HasActions);
                if (GameController.IsFinished())
                {
                    if (AreFinished())
                    {
                        //Debug.Log("[AGameState] AreFinished");
                        OnFinished();
                    }
                }
            }
        }

        public void SetupTimer(StateTimer timer)
        {
            Timer?.Elapsed.CoreEvent.RemoveListener(OnTimerElapsed);

            StateTimer = timer;
            Timer.Elapsed.CoreEvent.AddListener(OnTimerElapsed);
            //GameController.Logger.Log("GameState Timer Setuped");
        }

        private bool TryStartTimer()
        {
            if (GameController.HasAuthority && CanStartTimer)
            {
                TimerStartedAction action = GameController.ActionFactory.Create<TimerStartedAction>()
                    .Initialize(EGameState);
                GameController.ActionDistributor.HandleAction(action);
                return true;
            }
            return false;
        }

        private bool TryStopTimer()
        {
            if (GameController.HasAuthority && HasRunningTimer)
            {
                TimerElapsedAction action = GameController.ActionFactory.Create<TimerElapsedAction>()
                    .Initialize(EGameState);
                GameController.ActionDistributor.Add(action);
                return true;
            }
            return false;
        }

        private void OnTimerElapsed(Timer timer)
        {
            if (GameController.HasAuthority)
            {
                GameController.PlayerManager.SetAllPlayersStatus(EPlayerStatus.Blocked);
                OnFinished();
            }
            //GameController.EventManager.OnStateTimerElapsed.Invoke(timer);
            //GameController.Logger.Log("[GameState] OnStateTimerElapsed Invoked");
        }

        protected void SwitchState(EGameState state)
        {
            if (GameController.HasAuthority)
            {
                SwitchGameStateAction action = GameController.ActionFactory.Create<SwitchGameStateAction>()
                    .Initialize(EGameState, state);
                GameController.ActionDistributor.Add(action);
            }
        }


        protected abstract void SendWaitingForFinishedReport();
        protected abstract bool AreFinished();
        protected abstract void OnFinished();

        public virtual void FromPacket(GameController controller, Packet packet)
        {
            
        }

        public virtual void ToPacket(Packet packet)
        {
            
        }

        public void Copy(AGameState other, GameController controller)
        {
            EGameState = other.EGameState;
        }
    }
}
