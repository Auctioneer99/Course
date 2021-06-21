using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct CardDefinition : IDeserializable
    {
        public static readonly CardDefinition Unknown = new CardDefinition(0);

        public int TemplateId;

        public CardDefinition(int templateId)
        {
            TemplateId = templateId;
        }

        public void FromPacket(Packet packet)
        {
            TemplateId = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(TemplateId);
        }

        public static bool operator == (CardDefinition a, CardDefinition b)
        {
            return a.TemplateId == b.TemplateId;
        }

        public static bool operator !=(CardDefinition a, CardDefinition b)
        {
            return a.TemplateId != b.TemplateId;
        }

        public override bool Equals(object obj)
        {
            if (obj is CardDefinition definition)
            {
                return this == definition;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[CardDefinition]");
            sb.AppendLine($"TemplateId = {TemplateId}");
            return sb.ToString();
        }
    }
}
