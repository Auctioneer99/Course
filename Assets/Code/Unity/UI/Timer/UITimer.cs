using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Gameplay.Unity
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        public StateTimer Timer { get; set; }
        private bool _active;

        private void Awake()
        {
            ToggleTimer(false);
        }

        private void Update()
        {
            if (Timer == null || _active == false)
            {
                return;
            }
            _text.text = Math.Round((double)Timer.TimeRemaining / 1000, 0).ToString();
        }

        public void UnInitialize()
        {
            Timer = null;
            ToggleTimer(false);
        }

        public void ToggleTimer(bool active)
        {
            if (_active != active)
            {
                _active = active;
                UpdateVisibility(active);
            }
        }

        public void UpdateVisibility(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
