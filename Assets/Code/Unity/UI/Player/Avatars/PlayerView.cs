using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.Unity
{
    public class PlayerView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public EPlayer Place { get; private set; }
        public PlayersUIController PlayersUI { get; private set; }
        public GameController GameController => PlayersUI.Controller;
        public Player Player => GameController.PlayerManager.GetPlayer(Place);

        public PlayerStateMachine FSM { get; private set; }


        public Image ConnectButton;
        public Button DisconnectButton, ReadyButton, UnreadyButton;

        //public GameObject Attack, Health;

        //public TextMeshPro AttackValue, HealthValue;

        public void Initialize(PlayersUIController controller, EPlayer place)
        {
            PlayersUI = controller;
            Place = place;
            FSM = new PlayerStateMachine(this);

            GameController.EventManager.OnPlayerSetup.VisualEvent.AddListener(OnPlayerSetup);
            GameController.EventManager.PlayerDisconnected.VisualEvent.AddListener(OnPlayerDisconnected);
        }

        public void DisconnectPlayer()
        {
            var action = GameController.ActionFactory.Create<AskDisconnectPlayerAwaitingStateAction>()
                .Initialize();
            GameController.ActionDistributor.Add(action);
        }

        public void Prepare()
        {
            var action = GameController.ActionFactory.Create<AskPrepare>()
                .Initialize(true);
            GameController.ActionDistributor.Add(action);
        }

        public void Unprepare()
        {
            var action = GameController.ActionFactory.Create<AskPrepare>()
                .Initialize(false);
            GameController.ActionDistributor.Add(action);
        }

        private void OnPlayerSetup(Player player)
        {
            if (player.EPlayer == Place)
            {
                if (player.IsLocalUser())
                {
                    FSM.TransitionTo(EPlayerState.UnpreparedLocal);
                    Player.Prepared.VisualEvent.AddListener(OnLocalPlayerPrepared);
                }
                else
                {
                    FSM.TransitionTo(EPlayerState.Unprepared);
                    Player.Prepared.VisualEvent.AddListener(OnPlayerPrepared);
                }
            }
        }

        private void OnPlayerDisconnected(Player player)
        {
            if (player.EPlayer == Place)
            {
                if (player.IsLocalUser())
                {
                    FSM.TransitionTo(EPlayerState.UnpreparedLocal);
                    Player.Prepared.VisualEvent.RemoveListener(OnLocalPlayerPrepared);
                }
                else
                {
                    FSM.TransitionTo(EPlayerState.Unprepared);
                    Player.Prepared.VisualEvent.RemoveListener(OnPlayerPrepared);
                }
                FSM.TransitionTo(EPlayerState.NotConnected);
            }
        }

        private void OnLocalPlayerPrepared(bool toPrepare)
        {
            if (toPrepare)
            {
                FSM.TransitionTo(EPlayerState.PreparedLocal);
            }
            else
            {
                FSM.TransitionTo(EPlayerState.UnpreparedLocal);
            }
        }

        private void OnPlayerPrepared(bool toPrepare)
        {
            if (toPrepare)
            {
                FSM.TransitionTo(EPlayerState.Prepared);
            }
            else
            {
                FSM.TransitionTo(EPlayerState.Unprepared);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            FSM.CurrentState.OnMouseClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            FSM.CurrentState.OnMouseEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            FSM.CurrentState.OnMouseLeave();
        }
    }
}
