namespace Gameplay
{
    public abstract class ARequest : AAction, IAuthorityAction
    {
        public int Id { get; private set; }

        public abstract ERequest ERequest { get; }

        public int Connection { get; private set; }
        public bool Expired { get; set; }
        public virtual bool CanCancel { get; private set; }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            ARequest req = copyFrom as ARequest;

            Id = req.Id;
            Connection = req.Connection;
            Expired = req.Expired;
            CanCancel = req.CanCancel;
            CopyRequestImplementation(req, controller);
        }

        protected void Initialize(int connection, bool canCancel)
        {
            Initialize();

            Id = GameController.RequestHolder.AllocateRequestId();
            Connection = connection;
            CanCancel = canCancel;
        }

        protected abstract void CopyRequestImplementation(ARequest req, GameController controller);

        public abstract bool IsFulfilled();
        public abstract void HandleFulfilled();
        public abstract void HandleCancelled();
        public abstract void HandleExpired();
        public abstract void HandleAborted();

        protected sealed override void AttributesFrom(Packet packet)
        {
            Id = packet.ReadInt();
            Connection = packet.ReadInt();
            CanCancel = packet.ReadBool();
            RequestAttributesFrom(packet);
        }

        protected sealed override void AttributesTo(Packet packet)
        {
            packet.Write(Id)
                .Write(Connection)
                .Write(CanCancel);
            RequestAttributesTo(packet);
        }

        protected abstract void RequestAttributesFrom(Packet packet);
        protected abstract void RequestAttributesTo(Packet packet);
    }
}
