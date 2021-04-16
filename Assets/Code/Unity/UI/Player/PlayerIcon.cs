using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Gameplay.Unity
{
    public class PlayerIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image ConnectButton;

        public GameObject Attack, Health;

        public TextMeshPro AttackValue, HealthValue;

        private PlayerStateMachine _fsm;

        public void OnPointerClick(PointerEventData eventData)
        {
            _fsm.CurrentState.OnMouseClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _fsm.CurrentState.OnMouseEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _fsm.CurrentState.OnMouseLeave();
        }

        private void Start()
        {
            _fsm = new PlayerStateMachine(this, new Player(null, EPlayer.Player1));

            _fsm.TransitionTo(EPlayerState.NotConnected);
        }
    }
}
