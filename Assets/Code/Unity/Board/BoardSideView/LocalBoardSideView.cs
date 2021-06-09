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

        public override void Attach(GameController game, bool wasJustInitialized)
        {
            _controller = game;

            _controller.EventManager.OnPlayerSetup.VisualEvent.AddListener(OnPlayerSetup);
        }

        public void Setup()
        {
            Player player = _controller.PlayerManager.LocalUser;
            OnPlayerSetup(player);

        }

        private void OnPlayerSetup(Player player)
        {
            if (LocalPlayer == player.EPlayer)
            {
                //Player = player;

                //attaching events;
            }
        }
    }

}
