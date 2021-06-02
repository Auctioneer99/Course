using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class BoardView : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private BattlefieldController BattlefieldController;

        private GameController _controller;

        [SerializeField]
        private LocalBoardSideView LocalBoardSideView;

        private void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public BoardSideView GetBoardSideView(EPlayer player)
        {
            if (player == _controller.PlayerManager.LocalUserId)
            {
                return LocalBoardSideView;
            }

            return null;
        }

        public LocationView GetLocationView(Position position)
        {
            if (position.Location == ELocation.Field)
            {
                return BattlefieldController.GetTileView(position);
            }

            BoardSideView view = GetBoardSideView(position.Id.GetEPlayer());
            return view?.GetLocationView(position);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            _controller.EventManager.OnSnapshotRestored.VisualEvent.AddListener(OnSnapshotRestored);
        }

        public void Detach(GameController game)
        {
        }

        public void OnGameEndedCleanup()
        {
        }

        private void OnSnapshotRestored()
        {
            if (_controller.IsInitialized)
            {
                BattlefieldController.SetupField(_controller.BoardManager.Battlefield);
            }

            if (_controller.PlayerManager.LocalUserId.IsPlayer())
            {
                LocalBoardSideView.Setup();
            }
        }
    }
}
