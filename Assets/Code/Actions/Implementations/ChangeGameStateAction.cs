using System;

namespace Gameplay
{
    public class ChangeGameStateAction : AAction
    {
        public override EAction EAction => EAction.ChangeGameState;

        public EGameState From { get; private set; }
        public EGameState To { get; private set; }


        protected override void ApplyImplementation()
        {
            throw new NotImplementedException();
        }

        protected override void AttributesFromPacket(Packet packet)
        {
            From = packet.ReadInt();
            To = packet.ReadInt();
        }

        protected override void AttributesToPacket(Packet packet)
        {
            packet.Write(From);
            packet.Write(To);
        }
    }
}
