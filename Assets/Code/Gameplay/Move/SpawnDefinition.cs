using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct SpawnDefinition : ICensored, IRuntimeDeserializable
    {
        public ushort CardId;
        public CardDefinition Definition;
        public Position Position;
        public ECardVisibility InitialVisibility;

        public SpawnDefinition(CardManager cardManager, CardDefinition definition, Position position,
             Card spawner = null, ECardVisibility initialVisibility = ECardVisibility.Noone) 
        {
            CardId = cardManager.AllocateCardId();
            Definition = definition;
            Position = position;
            InitialVisibility = initialVisibility;
        }

        public void Censor(EPlayer player)
        {
            if (!InitialVisibility.IsVisibleTo(Position.Player, player))
            {
                Definition = CardDefinition.Unknown;
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            CardId = packet.ReadUShort();
            Definition = new CardDefinition(packet);
            Position = new Position(packet);
            InitialVisibility = packet.ReadECardVisibility();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(CardId)
                .Write(Definition)
                .Write(Position)
                .Write(InitialVisibility);
        }
    }
}
