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

            //Debug.Log("<color=green>Spawns</color>");
            //Debug.Log(Spawns.Count);
            foreach (var spawn in Spawns)
            {
                Location location = GameController.BoardManager.GetLocation(spawn.Position);

                if (location != null && location.IsFull)
                {
                    UnityEngine.Debug.Log($"<color=red>Spawn request was cancelled{spawn.ToString()}</color");
                    Spawns.Remove(spawn);
                    continue;
                }
                //Debug.Log("<color=blue>registred</color>");
                Card card = GameController.CardManager.Register(spawn.CardId, spawn.Definition, spawn.Position);
                spawnedCards.Add(card);

            }
            GameController.EventManager.CardsSpawned.Invoke(spawnedCards);

            /////
            /////
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
