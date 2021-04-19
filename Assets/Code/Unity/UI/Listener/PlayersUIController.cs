using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class PlayersUIController : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private PlayerView _playerViewPrefab;

        public Dictionary<EPlayer, PlayerView> Views { get; private set; }
        public GameController Controller { get; private set; }

        private void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            Debug.Log("<color=purple>Attaching</color>");
            Controller = game;
            EventManager eventManager = game.EventManager;

            eventManager.OnPlayerSetup.CoreEvent.AddListener(OnPlayerSetup);

            eventManager.OnSnapshotRestored.CoreEvent.AddListener(OnSnapshotRestored);
        }

        public void Detach(GameController game)
        {
            throw new NotImplementedException();
        }

        public void OnGameEndedCleanup()
        {
            throw new NotImplementedException();
        }

        private void OnSnapshotRestored()
        {
            Debug.Log($"<color=purple>{Controller.GameInstance.Settings.PlayersCount} players</color>");
            //reset
            Views = new Dictionary<EPlayer, PlayerView>(Controller.GameInstance.Settings.PlayersCount);
            foreach (var ps in Controller.GameInstance.Settings.PlayersSettings)
            {
                Debug.Log("Creating Player view " + ps.Key);
                PlayerView view = Instantiate(_playerViewPrefab, this.transform);
                view.Initialize(this, ps.Key);
                Views.Add(ps.Key, view);
            }
        }

        private void OnPlayerSetup(Player player)
        {
            Debug.Log(player);
            Views[player.EPlayer].FSM.TransitionTo(EPlayerState.AwaitingStart);
            Debug.Log("ui change new player");
        }
    }
}
