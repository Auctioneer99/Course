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
        private BoardSideView BoadSideViewPrefab = null;

        [SerializeField]
        private BattlefieldController BattlefieldController = null;
        [SerializeField]
        private LocalBoardSideView LocalBoardSideView = null;

        private GameController _controller;
        private Dictionary<EPlayer, BoardSideView> _boardSideViews;

        private void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public BoardSideView GetBoardSideView(EPlayer player)
        {
            foreach(var side in _boardSideViews)
            {
                if (side.Key == player)
                {
                    return side.Value;
                }
            }
            return null;
        }

        public LocationView GetLocationView(Position position)
        {
            if (position.Location == ELocation.Field)
            {
                return BattlefieldController.GetTileView(position);
            }

            if (position.Player == LocalBoardSideView.EPlayer)
            {
                return LocalBoardSideView.GetLocationView(position);
            }
            return null;
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            //_controller.EventManager.OnSnapshotRestored.VisualEvent.AddListener();
            _controller.GameInstance.SnapshotRestored.VisualEvent.AddListener(OnSnapshotRestored);
            _controller.EventManager.OnPlayerSetup.VisualEvent.AddListener(OnPlayerSetup);
        }

        public void Detach(GameController game)
        {
        }

        public void OnGameEndedCleanup()
        {
        }

        private void OnPlayerSetup(Player player)
        {
            if (_controller.Network.ConnectionId == player.ConnectionId)
            {
                LocalBoardSideView.Initialize(this, player.EPlayer);
            }
        }

        private void OnSnapshotRestored(GameInstance instance)
        {

            //Debug.Log("<color=green>Initialize boardView</color>");

            _boardSideViews = new Dictionary<EPlayer, BoardSideView>(_controller.PlayerManager.Players.Count + 1);
            foreach(var side in _controller.BoardManager.Sides)
            {
                BoardSideView view = Instantiate(BoadSideViewPrefab, this.transform);
                view.Initialize(this, side.EPlayer);
                _boardSideViews.Add(side.EPlayer, view);
            }

            if (_controller.IsInitialized)
            {
                BattlefieldController.SetupField(_controller.BoardManager.Battlefield);
            }
            //BoardSideView neutralView = Instantiate(BoadSideViewPrefab, this.transform);
            //neutralView.Initialize(this, EPlayer.Neutral);
            //_boardSideViews.Add(EPlayer.Neutral, neutralView);
            /*
            if (_controller.PlayerManager.LocalUserId.IsPlayer())
            {
                LocalBoardSideView.Setup();
            }*/
        }
    }
}
