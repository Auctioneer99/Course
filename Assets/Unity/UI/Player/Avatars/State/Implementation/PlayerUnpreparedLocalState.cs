using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay.Unity
{
    public class PlayerUnpreparedLocalState : APlayerState
    {
        public PlayerUnpreparedLocalState(PlayerStateMachine fsm) : base(fsm, EPlayerState.UnpreparedLocal) { }

        public override void OnEnterState()
        {
            View.ReadyButton.gameObject.SetActive(true);
            View.DisconnectButton.gameObject.SetActive(true);
        }

        public override void OnLeaveState()
        {
            View.ReadyButton.gameObject.SetActive(false);
            View.DisconnectButton.gameObject.SetActive(false);
        }

        public override void OnMouseClick()
        {

        }
    }
}
