using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Health: IStateObject<Health>, IRuntimeDeserializable
    {
        public CardData CardData { get; private set; }

        public int Base { get; private set; }
        public int Current { get; private set; }
        public int Max { get; private set; }

        public Health(CardData data)
        {
            CardData = data;
        }

        public void Initialize()
        {
            CardTemplate template = CardData.Card.Template;

            Current = Base = Max = template.Health;
        }

        public void SetHealth(int newHealth)
        {
            if (newHealth < 0)
            {
                newHealth = 0;
            }

            if (Current != newHealth)
            {
                int oldHealth = Current;
                Current = newHealth;

                Card card = CardData.Card;
                card.HealthChanged.Invoke(card, oldHealth, newHealth);

                if(card.CanDie)
                {
                    card.Kill();
                }
            }
        }

        public void Copy(Health other, GameController controller)
        {
            Base = other.Base;
            Current = other.Current;
            Max = other.Max;
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            Base = packet.ReadInt();
            Current = packet.ReadInt();
            Max = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Base)
                .Write(Current)
                .Write(Max);
        }
    }
}
