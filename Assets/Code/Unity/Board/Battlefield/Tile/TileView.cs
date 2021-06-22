using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class TileView : LocationView, ILoggable
    {
        public override ELocation ELocation => ELocation.Field;

        public override void HandleMove(CardBattleView card)
        {
            card.transform.position = transform.position + new Vector3(0, 10, 0);
        }

        public void Initialize(BoardSideView sideView, Tile tile)
        {
            BoardSideView = sideView;
            Location = tile;

            Renderer renderer = GetComponent<Renderer>();


            if (sideView == null)
            {
                renderer.material.color = Color.red;
                return;
            }

            switch (Location.BoardSide.EPlayer)
            {
                case EPlayer.Neutral:
                    renderer.material.color = Color.white;
                    break;
                case EPlayer.Player1:
                    renderer.material.color = Color.cyan;
                    break;
                case EPlayer.Player2:
                    renderer.material.color = Color.magenta;
                    break;
                case EPlayer.Player3:
                    renderer.material.color = Color.yellow;
                    break;
            }
            var c = renderer.material.color;
            c.a = 0.2f;
            renderer.material.color = c;
        }

        public void Log()
        {
            Debug.Log(Location.ToString());
        }
    }
}
