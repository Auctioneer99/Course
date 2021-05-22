using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class CardManager : AManager, IStateObject<CardManager>, IRuntimeDeserializable, ICensored
    {
        public CardManager(GameController controller) : base(controller)
        {

        }

        public void Censor(EPlayer player)
        {
            throw new NotImplementedException();
        }

        public void Copy(CardManager other, GameController controller)
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
    }
}
