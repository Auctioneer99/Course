using Gameplay.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gameplay
{
    public class Location : IRuntimeStateObject<Location>, IRuntimeDeserializable
    {
        public BattleEvent<Location> Changed { get; private set; }

        public BoardSide BoardSide { get; private set; }

        public ELocation ELocation { get; private set; }
        public EPlayer Player => BoardSide.EPlayer;

        public List<Card> Cards { get; private set; }
        public int Capacity { get; private set; }

        public bool IsFull => Capacity == Cards.Count;

        public Location(BoardSide side, ELocation location, int capacity = -1)
        {
            BoardSide = side;
            ELocation = location;
            Capacity = capacity > 0 ? capacity : 64;
            Cards = new List<Card>(Capacity);

            Changed = new BattleEvent<Location>(null);
        }

        public bool TryAdd(Card card, int index)
        {
            bool added = Add_Internal(card, index);

            if (added)
            {
                Changed.Invoke(this);
            }
            return added;
        }

        private bool Add_Internal(Card card, int index)
        {
            index = GetValidIndex(index);

            if (index == Cards.Count)
            {
                Cards.Add(card);
            }
            else
            {
                Cards.Insert(index, card);
            }
            UpdateIndexes();

            return true;
        }

        private int GetValidIndex(int index)
        {
            int resultIndex;

            switch(index)
            {
                case Position.FIRST:
                    resultIndex = 0;
                    break;
                case Position.LAST:
                    resultIndex = Cards.Count;
                    break;
                case Position.RANDOM:
                    resultIndex = BoardSide.BoardManager.GameController.RandomGenerator.Next(Cards.Count);
                    break;
                default:
                    resultIndex = index;
                    if (resultIndex < 0)
                    {
                        resultIndex = 0;
                    }
                    else
                    {
                        if (resultIndex > Cards.Count)
                        {
                            resultIndex = Cards.Count;
                        }
                    }
                    break;
            }

            return resultIndex;
        }

        public bool TryRemove(Card card)
        {
            bool removed = Remove_Internal(card);
            if (removed)
            {
                Changed.Invoke(this);

            }
            return removed;
        }

        private bool Remove_Internal(Card card)
        {
            if (Cards.Remove(card) == false)
            {
                return false;
            }
            UpdateIndexes();
            return true;
        }

        protected virtual void UpdateIndexes()
        {
            
            Position position = new Position(Player, ELocation);
            for(int i = 0, count = Cards.Count; i < count; i++)
            {
                position.Index = i;
                Cards[i].Position = position;
            }
        }

        public virtual void Copy(Location other, GameController controller)
        {
            Cards.Clear();

            foreach(var c in other.Cards)
            {
                Card card = controller.CardManager.GetCard(c.Id);
                Cards.Add(card);
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            throw new NotImplementedException();
        }

        public void ToPacket(Packet packet)
        {
            foreach(var c in Cards)
            {
                packet.Write(c.Id);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Location]");
            sb.AppendLine($"Location = {ELocation}");
            sb.AppendLine($"Capacity = {Capacity}");
            sb.AppendLine($"CardsCount = {Cards.Count}");

            foreach(var card in Cards)
            {
                sb.Append(card.ToString());
            }

            return sb.ToString();
        }
    }
}
