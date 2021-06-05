using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class RemovePauseAction : AAction, IAuthorityAction, IPriorityAction
    {
        public override EAction EAction => EAction.RemovePause;

        public int Id { get; private set; }

        public RemovePauseAction Initialize(int id)
        {
            Initialize();

            Id = id;
            return this;
        }

        protected override void ApplyImplementation()
        {
            GameController.PauseManager.Remove(Id);
        }

        protected override void AttributesFrom(Packet packet)
        {
            Id = packet.ReadInt();
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Id);
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            RemovePauseAction other = copyFrom as RemovePauseAction;

            Id = other.Id;
        }
    }
}
