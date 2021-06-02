using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class BoardManager : AManager
    {
        public Battlefield Battlefield { get; private set; }
        public BoardSide[] Sides { get; private set; }

        public BoardManager(GameController controller) : base(controller)
        {
            Battlefield = new Battlefield(this);
            Sides = new BoardSide[GameController.PlayerManager.Players.Count + 1];
            int i = 0;
            foreach(var Player in GameController.PlayerManager.Players)
            {
                Sides[i++] = new BoardSide(this, Player.Key);
            }
            Sides[i++] = new BoardSide(this, EPlayer.Neutral);
        }

        public void Initialize(BattlefieldSettings settings)
        {
            foreach(var s in settings.BattlefieldPlayerSettings)
            {
                Debug.Log(s.EPlayer);
                var side = GetBoardSide(s.EPlayer);
                if (side != null)
                {
                    Debug.Log(side);
                    side.Initialize(s);
                }
            }
            Battlefield.Setup(settings);
        }

        public BoardSide GetBoardSide(EPlayer player)
        {
            foreach(var side in Sides)
            {
                if (side.EPlayer == player)
                {
                    return side;
                }
            }
            return null;
        }

        public Location GetLocation(Position position)
        {
            if (position.Location == ELocation.Field)
            {
                return Battlefield.GetTile(position.Id);
            }
            else
            {
                BoardSide side = GetBoardSide((EPlayer)position.Id);
                return side?.GetLocation(position.Location);
            }
        }

        public void Move(MoveDefinition definition)
        {
            Location location = GetLocation(definition.To);
            if (location != null && location.IsFull)
            {
                return;
            }

            bool moved = MoveImplementation(definition);
            if (moved)
            {
                GameController.EventManager.CardMoved.Invoke();
            }
        }

        private bool MoveImplementation(MoveDefinition definition)
        {

        }
    }
}
