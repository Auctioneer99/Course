using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class SetupPlayerDeckAction : AAction, IAuthoritySideAction
    {
        public override EAction EAction => EAction.SetupPlayerDeck;

        public EPlayer Player { get; private set; }
        public RuntimeBattleDeck Deck { get; private set; }

        public SetupPlayerDeckAction Initialize(EPlayer player, BattleDeck deck)
        {
            Initialize();

            Player = player;
            Deck = new RuntimeBattleDeck(GameController, deck);
            return this;
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override void ApplyImplementation()
        {
            SpawnCardsAction spawnAction = GameController.ActionFactory.Create<SpawnCardsAction>()
                .Initialize(0);

            CardDefinition cardDef = new CardDefinition(2);
            Position pos = new Position(new TileDefinition(0, -2, 2));

            SpawnDefinition def = new SpawnDefinition(GameController.CardManager, cardDef, pos, Player, null, ECardVisibility.All);

            spawnAction.Spawns.Add(def);



            List<CardDefinition> deckCards = new List<CardDefinition>(Deck.Cards);
            List<SpawnDefinition> spawns = new List<SpawnDefinition>(deckCards.Count);

            foreach(var card in deckCards)
            {
                SpawnDefinition spawn = new SpawnDefinition(GameController.CardManager,
                    card,
                    new Position(Player, ELocation.Deck), Player, null, ECardVisibility.Noone);
                spawns.Add(spawn);
            }

            Shuffle(spawns);

            spawnAction.Spawns.AddRange(spawns);

            GameController.ActionDistributor.Add(spawnAction);
        }

        private void Shuffle<T>(List<T> list)
        {
            RandomGenerator rand = GameController.RandomGenerator;
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i);
                T tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

        protected override void AttributesFrom(Packet packet)
        {
            //
        }

        protected override void AttributesTo(Packet packet)
        {
            //
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            throw new NotImplementedException();
        }
    }
}
