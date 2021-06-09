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
            foreach(var playerSettings in settings.BattlefieldPlayerSettings)
            {
                var side = GetBoardSide(playerSettings.EPlayer);
                if (side != null)
                {
                    side.Initialize(playerSettings);
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

        public void Move(Card card, Position toPosition)
        {
            Location location = GetLocation(toPosition);
            if (location != null && location.IsFull)
            {
                return;
            }

            Position previousPosition = card.Position;
            bool moved = MoveImplementation(card, toPosition);
            if (moved)
            {
                GameController.EventManager.CardMoved.Invoke(card, previousPosition);
            }
        }

        private bool MoveImplementation(Card card, Position toPosition)
        {
            Position currentPosition = card.Position;

            if((currentPosition.Location == toPosition.Location && 
                currentPosition.Id == toPosition.Id) == false)
            {
                Location location = GetLocation(toPosition);
                if (location != null && location.IsFull)
                {
                    return false;
                }
            }

            if (toPosition.Location == ELocation.Field)
            {
                
                if (currentPosition.IsExist)
                {
                    Tile tile = Battlefield.GetTile(currentPosition.Id);
                    tile.TryRemove(card);
                }

                if (toPosition.IsExist)
                {
                    Tile tile = Battlefield.GetTile(toPosition.Id);
                    tile.TryAdd(card, toPosition.Index);
                }
                else
                {
                    card.Position = toPosition;
                }

            }
            else
            {
                if (currentPosition.IsExist)
                {
                    BoardSide side = GetBoardSide((EPlayer)currentPosition.Id);
                    side.Remove(card);
                }

                if (toPosition.IsExist)
                {
                    BoardSide side = GetBoardSide((EPlayer)toPosition.Id);
                    side.Add(card, toPosition);
                }
                else
                {
                    card.Position = toPosition;
                }
            }

            return true;
        }
    }
}
