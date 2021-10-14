using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Gameplay.Unity
{
    public class StateUI : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private TextMeshProUGUI _text = null;

        private void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            game.EventManager.OnGameStateChanged.VisualEvent.AddListener(OnGameStateChanged);
        }

        public void Detach(GameController game)
        {
            //throw new NotImplementedException();
        }

        public void OnGameEndedCleanup()
        {
            throw new NotImplementedException();
        }

        private void OnGameStateChanged(AGameState from, AGameState to)
        {
            //Debug.Log(from?.ToString());
            //Debug.Log(to?.ToString());
            _text.text = to.EGameState.ToString();
        }
    }
}
