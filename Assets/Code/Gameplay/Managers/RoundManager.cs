using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class RoundManager : AManager, IStateObjectCloneable<RoundManager>, IRuntimeDeserializable
    {
        public const int MAX_ROUND_COUNT = 25;

        public int Round { get; private set; } = 0;


        public RoundManager(GameController controller) : base(controller) { }


        public RoundManager Clone(GameController controller)
        {
            throw new NotImplementedException();
        }

        public void Copy(RoundManager other, GameController controller)
        {
            throw new NotImplementedException();
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            throw new NotImplementedException();
        }

        public void ToPacket(Packet packet)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[RoundManager]");
            sb.AppendLine($"CurrentRound = {Round}");
            return base.ToString();
        }
    }
}
