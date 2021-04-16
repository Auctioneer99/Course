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

        protected override void ApplyImplementation()
        {
            //SpawnCardsAction spawnAction = GameController.ActionFactory.Create<SpawnCardsAction>()
            //    .Initialize();

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
    }
}
