using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class LocationViewManager : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private BattlefieldController BattlefieldController;

        private GameController _controller;

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            game.EventManager.OnSnapshotRestored.VisualEvent.AddListener(OnSnapshotRestored);
        }

        public void Detach(GameController game)
        {
            
        }

        public LocationView GetLocationView(Position position)
        {

            return null;
        }

        public void OnGameEndedCleanup()
        {
            
        }

        private void OnSnapshotRestored()
        {
            //creating location views
        }
    }
}
