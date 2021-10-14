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
        private TextMeshProUGUI _text = null;

        public StateTimer StateTimer { get; set; }
        private bool _active;

        private void Awake()
        {
            ToggleTimer(false);
        }

        private void Update()
        {
            if (StateTimer == null || _active == false)
            {
                return;
            }
            _text.text = Math.Round((double)StateTimer.Timer.TimeRemaining / 1000, 0).ToString();
        }

        public void UnInitialize()
        {
            StateTimer = null;
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
