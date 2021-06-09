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
            if (!InitialVisibility.IsVisibleTo(GetOwner(Position), player))
            {
                Definition = CardDefinition.Unknown;
            }
        }

        private EPlayer GetOwner(Position position)
        {
            if (position.Location == ELocation.Field)
            {
                return EPlayer.Players | EPlayer.Neutral;
            }
            else
            {
                return (EPlayer) position.Id;
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[SpawnDefinition]");
            sb.AppendLine($"CardId = {CardId}");
            sb.AppendLine($"Definition = {Definition.ToString()}");
            sb.AppendLine($"Position = {Position.ToString()}");
            sb.AppendLine($"InitialVisibility = {InitialVisibility}");
            return sb.ToString();
        }
    }
}
