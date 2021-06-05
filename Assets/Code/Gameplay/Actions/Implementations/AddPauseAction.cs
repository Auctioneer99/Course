using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class AddPauseAction : AAction, IAuthorityAction, IPriorityAction
    {
        public override EAction EAction => EAction.AddPause;

        public PauseRequest Pause { get; private set; }

        public AddPauseAction Initialize(PauseRequest pause)
        {
            Initialize();

            Pause = pause;
            return this;
        }

        protected override void ApplyImplementation()
        {
            GameController.PauseManager.Add(Pause);
        }

        protected override void AttributesFrom(Packet packet)
        {
            Pause = new PauseRequest(packet);
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Pause);
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            AddPauseAction other = copyFrom as AddPauseAction;

            Pause = other.Pause;
        }
    }
}
