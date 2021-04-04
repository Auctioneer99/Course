using System;

namespace Gameplay
{
    public abstract class AAction : IPacketable
    {
        public abstract EAction EAction { get; }
        public int NetworkActionNumber { get; set; }
        public GameController GameController { get; set; }

        public bool Initialized { get; private set; }

        public virtual bool IsValid()
        {
            return true;
        }

        protected void Initialize()
        {
            if (Initialized)
            {
                throw new Exception("Action " + EAction + "already initialized");
            }
            Initialized = true;
        }

        public virtual void Copy(AAction copyFrom, GameController controller)
        {
            Initialized = copyFrom.Initialized;
            NetworkActionNumber = copyFrom.NetworkActionNumber;
        }

        public void Apply()
        {
            if (Initialized == false)
            {
                throw new Exception("Action " + EAction + " is not initialized");
            }
            ApplyImplementation();
        }

        protected abstract void ApplyImplementation();

        public Packet ToPacket()
        {
            Packet packet = new Packet((int)EAction);
            packet.Write(NetworkActionNumber);
            AttributesTo(packet);
            return packet;
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            Initialized = true;
            NetworkActionNumber = packet.ReadInt();
            AttributesFrom(packet);
        }

        protected abstract void AttributesTo(Packet packet);
        protected abstract void AttributesFrom(Packet packet);
    }
}
