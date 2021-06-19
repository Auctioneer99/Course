using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class ActionPoints : IStateObject<ActionPoints>, IRuntimeDeserializable
    {
        public CardData CardData { get; private set; }

        public int Current { get; private set; }
        public int Base { get; private set; }

        public ActionPoints(CardData data)
        {
            CardData = data;
        }

        public void Initialize()
        {
            CardTemplate template = CardData.Card.Template;

            Current = Base = template.ActionPoints;
        }

        public void Copy(ActionPoints other, GameController controller)
        {
            Base = other.Base;
            Current = other.Current;
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            Base = packet.ReadInt();
            Current = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Base)
                .Write(Current);
        }
    }
}
