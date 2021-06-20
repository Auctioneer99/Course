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

        public override bool IsValid()
        {
            return true;
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            GameState = (copyFrom as TimerElapsedAction).GameState;
        }

        protected override void ApplyImplementation()
        {
            UnityEngine.Debug.Log(GameController.HasAuthority);
            Timer timer = GameController.StateMachine.GetState(GameState).Timer;
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
