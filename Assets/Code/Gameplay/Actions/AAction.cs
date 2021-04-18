﻿using System;

namespace Gameplay
{
    public abstract class AAction : IRuntimeDeserializable, IStateObjectCloneable<AAction>
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

        public AAction Clone(GameController controller)
        {
            AAction action = controller.ActionFactory.Create(EAction);
            action.Copy(this, controller);
            return action;
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

        public void ToPacket(Packet packet)
        {
            packet.Write(EAction)
                .Write(NetworkActionNumber);
            AttributesTo(packet);
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
