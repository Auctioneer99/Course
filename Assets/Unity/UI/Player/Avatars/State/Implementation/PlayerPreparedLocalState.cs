using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay.Unity
{
    public class PlayerPreparedLocalState : APlayerState
    {
        public PlayerPreparedLocalState(PlayerStateMachine fsm) : base(fsm, EPlayerState.PreparedLocal) { }

        public override void OnEnterState()
        {
            View.UnreadyButton.gameObject.SetActive(true);
        }

        public override void OnLeaveState()
        {
            View.UnreadyButton.gameObject.SetActive(false);
        }

        public override void OnMouseClick()
        {

        }
    }
}
