namespace Gameplay
{
    public abstract class ARequest : AAction, IAuthorityAction
    {
        public int Id { get; private set; }

        public abstract ERequest ERequest { get; }

        public EPlayer PlayerId { get; private set; }

        public bool Expired { get; set; }

        public virtual bool CanCancel { get; private set; }

        protected void Initialize(EPlayer player, bool canCancel)
        {
            Initialize();

            Id = GameController.RequestManager.AllocateRequestId();
            PlayerId = player;
            CanCancel = canCancel;
        }

        public abstract bool IsFulfilled();

        public abstract void HandleFulfilled();
        public abstract void HandleCancelled();
        public abstract void HandleExpired();
        public abstract void HandleAborted();

        protected override void AttributesFrom(Packet packet)
        {
            Id = packet.ReadInt();
            PlayerId = packet.ReadEPlayer();
            CanCancel = packet.ReadBool();
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Id)
                .Write(PlayerId)
                .Write(CanCancel);
        }
    }
}
