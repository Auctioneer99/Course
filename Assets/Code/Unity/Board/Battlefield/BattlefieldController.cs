using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class BattlefieldController : MonoBehaviour, IGameListener
    {
        [SerializeField]
        private GameObject _tilePrefab;
        [SerializeField]
        private float _distanceBetween = 0;
        [SerializeField]
        private BoardView _boardView;

        private GameController _controller;
        private Dictionary<int, TileView> _tiles;

        private void Start()
        {
            App.Instance.Listener.Add(this);
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            EventManager eManager = game.EventManager;

            eManager.OnBattleFieldSetuped.VisualEvent.AddListener(SetupField);
        }

        public void Detach(GameController game)
        {
            //throw new NotImplementedException();
        }

        public void OnGameEndedCleanup()
        {
            
        }

        public TileView GetTileView(Position position)
        {
            //Debug.Log(string.Join(", ", _tiles.Keys));
            return _tiles[position.Id];
        }

        public void SetupField(Battlefield field)
        {
            Quaternion rotation = Quaternion.Euler(-Mathf.Atan(Mathf.Sqrt(2) / 2) * (180 / Mathf.PI), 0, 45);
            _tiles = new Dictionary<int, TileView>(field.Tiles.Length);


            foreach(var tile in field.Tiles)
            {
                GameObject tileModel = Instantiate(_tilePrefab, transform);
                //tileModel.GetComponent<UnityTile>().Tile = tile;
                Vector3 rawPosition = new Vector3(tile.Definition.X, tile.Definition.Y, tile.Definition.Z);
                Vector3 position = rotation * rawPosition;
                //position.y -= 0.1f;
                tileModel.transform.localPosition = position * _distanceBetween;

                TileView view = tileModel.GetComponent<TileView>();
                Debug.Log(tile.BoardSide);
                view.Prepare(_boardView.GetBoardSideView(tile.BoardSide.EPlayer));

                _tiles.Add(tile.Id, tileModel.GetComponent<TileView>());
            }
        }
    }
}
