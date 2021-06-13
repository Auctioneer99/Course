using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class SpawnCardsAction : AAction, IAuthorityAction, ICensored
    {
        public override EAction EAction => EAction.SpawnCards;

        public ushort SpawnerId { get; private set; }
        public List<SpawnDefinition> Spawns { get; private set; }

        public SpawnCardsAction()
        {
            Spawns = new List<SpawnDefinition>();
        }

        public SpawnCardsAction Initialize(ushort spawnerId)
        {
            Initialize();
            SpawnerId = spawnerId;

            return this;
        }

        protected override void ApplyImplementation()
        {
            List<Card> spawnedCards = new List<Card>(Spawns.Count);

            Card spawner = GameController.CardManager.GetCard(SpawnerId);

            MoveAction moveAction = GameController.ActionFactory.Create<MoveAction>().Initialize();
            foreach (var spawn in Spawns)
            {
                Location location = GameController.BoardManager.GetLocation(spawn.Position);

                if (location != null && location.IsFull)
                {
                    UnityEngine.Debug.Log($"<color=red>Spawn request was cancelled{spawn.ToString()}</color");
                    Spawns.Remove(spawn);
                    continue;
                }
                Card card = GameController.CardManager.Register(spawn.CardId, spawn.Definition);
                spawnedCards.Add(card);

                MoveDefinition move = new MoveDefinition(card, spawn.Position, 0);
                moveAction.Moves.Add(move);
            }

            GameController.EventManager.CardsSpawned.Invoke(spawnedCards);
            GameController.ActionDistributor.Add(moveAction);
        }

        public void Censor(EPlayer player)
        {
            foreach(var s in Spawns)
            {
                s.Censor(player);
            }
        }

        protected override void AttributesFrom(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void AttributesTo(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            SpawnCardsAction other = copyFrom as SpawnCardsAction;

            SpawnerId = other.SpawnerId;
            Spawns = new List<SpawnDefinition>(other.Spawns.Count);
            foreach(var spawn in other.Spawns)
            {
                Spawns.Add(spawn);
            }
        }
    }
}
