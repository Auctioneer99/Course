﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class BoardManager : AManager, IRuntimeStateObject<BoardManager>
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
                var side = GetBoardSide(playerSettings.Key);
                if (side != null)
                {
                    side.Initialize(playerSettings.Value);
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
                BoardSide side = GetBoardSide(position.Player);
                return side?.GetLocation(position.Location);
            }
        }

        public void Move(Card card, Position toPosition)
        {
            Position previousPosition = card.Position;
            bool moved = MoveInternal(card, toPosition);
            if (moved)
            {
                GameController.EventManager.AfterCardMoved.Invoke(card, previousPosition);
            }
        }

        public void BattlefieldMove(Card card, List<TileDefinition> path)
        {
            Position initialPosition = card.Position;

            foreach (var point in path)
            {
                Position pos = new Position(point);
                bool moved = MoveInternal(card, pos);
            }

            GameController.EventManager.AfterCardBattlefieldMoved.Invoke(card, initialPosition, path);
        }

        private bool MoveInternal(Card card, Position toPosition)
        {
            Location location = GetLocation(toPosition);

            if (location != null && location.IsFull)
            {
                return false;
            }

            Position previousPosition = card.Position;
            bool moved = MoveImplementation(card, toPosition);
            if (moved)
            {
                GameController.EventManager.CardMoved.Invoke(card, previousPosition);
            }

            return moved;
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
                    BoardSide side = GetBoardSide(currentPosition.Player);
                    side.Remove(card);
                }

                if (toPosition.IsExist)
                {
                    BoardSide side = GetBoardSide(toPosition.Player);
                    side.Add(card, toPosition);
                }
                else
                {
                    card.Position = toPosition;
                }
            }

            return true;
        }

        public void Copy(BoardManager other, GameController controller)
        {
            int length = other.Sides.Length;
            Sides = new BoardSide[length];
            for (int i = 0; i < length; i++)
            {
                Sides[i] = other.Sides[i].Clone(controller);
            }
        }
    }
}
