using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class MoveAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.Move;

        public List<MoveDefinition> Moves { get; private set; }

        public Position To { get; private set; }
        public new MoveAction Initialize()
        {
            base.Initialize();

            Moves = new List<MoveDefinition>();
            return this;
        }

        protected override void ApplyImplementation()
        {
            foreach (var def in Moves)
            {
                GameController.BoardManager.Move(def);
            }
        }

        protected override void AttributesFrom(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void AttributesTo(Packet packet)
        {
            throw new NotImplementedException();
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            throw new NotImplementedException();
        }
    }
}
