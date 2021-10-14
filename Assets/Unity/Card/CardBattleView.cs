using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class CardBattleView : MonoBehaviour, ILoggable
    {
        public Card Card { get; private set; }

        private GameObject _parent;

        private void Awake()
        {
            _parent = gameObject;
        }

        public void Initialize(Card card)
        {
            Card = card;
            UpdateTeam();

        }

        private void UpdateTeam()
        {
            Renderer renderer = GetComponent<Renderer>();

            switch (Card.Owner)
            {
                case EPlayer.Neutral:
                    renderer.material.color = Color.white;
                    break;
                case EPlayer.Player1:
                    renderer.material.color = Color.cyan;
                    break;
                case EPlayer.Player2:
                    renderer.material.color = Color.magenta;
                    break;
                case EPlayer.Player3:
                    renderer.material.color = Color.yellow;
                    break;
                default:
                    renderer.material.color = Color.red;
                    break;
            }
        }

        public void ToggleVisibility(bool toDisplay)
        {
            if (toDisplay)
            {
                _parent.SetActive(true);
            }
            else
            {
                _parent.SetActive(false);
            }
        }

        public void Log()
        {
            Debug.Log(Card.ToString());
        }
    }
}
