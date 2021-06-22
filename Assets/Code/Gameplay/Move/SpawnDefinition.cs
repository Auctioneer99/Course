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
        public EPlayer Owner;
        public CardDefinition Definition;
        public Position Position;
        public ECardVisibility InitialVisibility;

        public SpawnDefinition(CardManager cardManager, CardDefinition definition, Position position, EPlayer owner = EPlayer.Neutral,
             Card spawner = null, ECardVisibility initialVisibility = ECardVisibility.Noone) 
        {
            CardId = cardManager.AllocateCardId();
            Owner = owner;
            Definition = definition;
            Position = position;
            InitialVisibility = initialVisibility;
        }

        public void Censor(EPlayer player)
        {
            if (InitialVisibility.IsVisibleTo(Position.Player, player) == false)
            {
                Definition = CardDefinition.Unknown;
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            CardId = packet.ReadUShort();
            Owner = packet.ReadEPlayer();
            Definition.FromPacket(packet);
            Position.FromPacket(packet);
            InitialVisibility = packet.ReadECardVisibility();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(CardId)
                .Write(Owner)
                .Write(Definition)
                .Write(Position)
                .Write(InitialVisibility);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[SpawnDefinition]");
            sb.AppendLine($"Owner = {Owner}");
            sb.AppendLine($"CardId = {CardId}");
            sb.AppendLine($"Definition = {Definition.ToString()}");
            sb.AppendLine($"Position = {Position.ToString()}");
            sb.AppendLine($"InitialVisibility = {InitialVisibility}");
            return sb.ToString();
        }
    }
}
