using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class LocalBoardSideView : BoardSideView
    {
        public EPlayer LocalPlayer => _controller.PlayerManager.LocalUserId;

        [SerializeField]
        public HandView Hand;

        public Dictionary<ELocation, LocationView> Locations;

        private void Start()
        {
            Locations = new Dictionary<ELocation, LocationView>
            {
                { ELocation.Hand, Hand },
            };
        }

        public override void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            if(_controller.IsInitialized)
            {
                ChangePerspectiveView(_controller.PlayerManager.PerspectivePlayer);
            }

            _controller.PlayerManager.PerspectiveChanged.VisualEvent.AddListener(ChangePerspectiveView);
        }

        public LocationView GetLocationView(Position position)
        {
            Locations.TryGetValue(position.Location, out LocationView view);
            return view;
        }

        private void ChangePerspectiveView(EPlayer player)
        {
            if (EPlayer == EPlayer.Undefined)
            {
                return;
            }

            BoardSideView view = BoardView.GetBoardSideView(player);

            Hand.Initialize(view);

        }
    }

}
