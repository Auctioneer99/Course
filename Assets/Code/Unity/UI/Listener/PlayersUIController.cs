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

            CreatePlayersUI();
            //eventManager.OnSnapshotRestored.CoreEvent.AddListener(OnSnapshotRestored);
        }

        public void Detach(GameController game)
        {
            Views.Clear();
        }

        public void OnGameEndedCleanup()
        {
            throw new NotImplementedException();
        }

        private void CreatePlayersUI()
        {
            //reset
            Views = new Dictionary<EPlayer, PlayerView>(Controller.GameInstance.Settings.PlayersCount);
            foreach (var ps in Controller.GameInstance.Settings.PlayersSettings)
            {
                PlayerView view = Instantiate(_playerViewPrefab, this.transform);
                view.Initialize(this, ps.Key);
                Views.Add(ps.Key, view);
            }
        }

        private void OnPlayerSetup(Player player)
        {
            Action<bool> OnPrepared = (toPrepare) =>
            {
                if (player.IsLocalUser())
                {
                    Views[player.EPlayer].FSM.TransitionTo(EPlayerState.PreparedLocal);
                }
                Views[player.EPlayer].FSM.TransitionTo(EPlayerState.Prepared);
            };


            player.Prepared.VisualEvent.AddListener(OnPrepared);


            if (player.IsLocalUser())
            {
                Views[player.EPlayer].FSM.TransitionTo(EPlayerState.UnpreparedLocal);
            }
            Views[player.EPlayer].FSM.TransitionTo(EPlayerState.Unprepared);
        }
        /*
        private void OnPrepared(bool toPrepare)
        {

        }*/
    }
}
