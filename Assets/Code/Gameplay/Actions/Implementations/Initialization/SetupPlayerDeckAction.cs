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

            CardDefinition cardDef = new CardDefinition(88);
            Position pos = new Position(0, ELocation.Field);

            SpawnDefinition def = new SpawnDefinition(GameController.CardManager, cardDef, pos, null, ECardVisibility.All);

            spawnAction.Spawns.Add(def);


            GameController.ActionDistributor.Add(spawnAction);




            bool spawnVictory = false;

            if (spawnVictory)
            {
                //
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
