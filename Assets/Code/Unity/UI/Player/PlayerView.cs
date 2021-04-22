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
        public PlayerStateMachine FSM { get; private set; }

        public Player Player => PlayersUI.Controller.PlayerManager.GetPlayer(Place);

        public void Initialize(PlayersUIController controller, EPlayer place)
        {
            PlayersUI = controller;
            Place = place;
            FSM = new PlayerStateMachine(this);
        }

        public Image ConnectButton;

        public GameObject Attack, Health;

        public TextMeshPro AttackValue, HealthValue;

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
