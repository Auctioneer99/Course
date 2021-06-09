using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public struct CardDefinition : ISerializable
    {
        public static readonly CardDefinition Unknown = new CardDefinition(0);

        public int TemplateId;

        public CardDefinition(int templateId)
        {
            TemplateId = templateId;
        }

        public CardDefinition(Packet packet)
        {
            TemplateId = packet.ReadInt();
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(TemplateId);
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
