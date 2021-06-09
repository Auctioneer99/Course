using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay.Unity
{
    public class HandView : LocationView
    {
        public override ELocation ELocation => ELocation.Hand;

        public override void Attach(GameController game, bool wasJustInitialized)
        {
            base.Attach(game, wasJustInitialized);

            //draw card
            //_controller.EventManager.OnGameInitialized.VisualEvent.AddListener(SetupCardsInHand);
        }

        private void DrawCards(GameController controller)
        {

        }
    }
}
