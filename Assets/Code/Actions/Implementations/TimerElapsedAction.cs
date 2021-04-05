using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class TimerElapsedAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.TimerElapsed;


        public EGameState GameState { get; private set; }

        public TimerElapsedAction Initialize(EGameState state)
        {
            Initialize();

            GameState = state;
            return this;
        }

        protected override void ApplyImplementation()
        {
            StateTimer timer = GameController.StateMachine.GetState(GameState).Timer;
            timer.Finish();
        }

        protected override void AttributesFrom(Packet packet)
        {
            GameState = packet.ReadEGameState();
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(GameState);
        }
    }
}
