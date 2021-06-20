using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class CardData: IRuntimeStateObject<CardData>, IRuntimeDeserializable
    {
        public Card Card { get; private set; }

        public Health Health { get; private set; }
        public Initiative Initiative { get; private set; }
        public Attack Attack { get; private set; }
        public ActionPoints ActionPoints { get; private set; }


        public CardData(Card card)
        {
            Card = card;

            Health = new Health(this);
            Initiative = new Initiative(this);
            Attack = new Attack(this);
            ActionPoints = new ActionPoints(this);
        }

        public void Initialize()
        {
            Health.Initialize();
            Initiative.Initialize();
            Attack.Initialize();
            ActionPoints.Initialize();
        }

        public void Copy(CardData other, GameController controller)
        {
            Health.Copy(other.Health, controller);
            Initiative.Copy(other.Initiative, controller);
            Attack.Copy(other.Attack, controller);
            ActionPoints.Copy(other.ActionPoints, controller);
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            Health.FromPacket(controller, packet);
            Initiative.FromPacket(controller, packet);
            Attack.FromPacket(controller, packet);
            ActionPoints.FromPacket(controller, packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Health)
                .Write(Initiative)
                .Write(Attack)
                .Write(ActionPoints);
        }
    }
}
