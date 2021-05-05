namespace Gameplay
{
    public class ReportPlayerStatusAction : APlayerAction, IUserAction
    {
        public override EAction EAction => EAction.ReportPlayerStatus;

        public bool ValidWhenBlocked => true;

        public int RequestId { get; private set; }
        public EPlayerStatus Status { get; private set; }

        public override bool IsValid()
        {
            if (base.IsValid())
            {
                if (GameController.HasAuthority)
                {
                    RequestPlayerFinishedReport request = GameController.RequestHolder.Get<RequestPlayerFinishedReport>(EPlayer, RequestId);
                    return request != null;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public ReportPlayerStatusAction Initialize(int connection, int requestId, EPlayerStatus status)
        {
            Initialize(connection);
            RequestId = requestId;
            Status = status;
            return this;
        }

        protected override void PlayerCopyImplementation(APlayerAction copyFrom, GameController controller)
        {
            ReportPlayerStatusAction other = copyFrom as ReportPlayerStatusAction;

            RequestId = other.RequestId;
            Status = other.Status;
        }

        protected override void ApplyImplementation()
        {
            Player player = GameController.PlayerManager.GetPlayer(EPlayer);
            if (GameController.HasAuthority)
            {
                RequestPlayerFinishedReport request = GameController.RequestHolder.Get<RequestPlayerFinishedReport>(EPlayer, RequestId);

            }
            player.EStatus = Status;
        }

        protected override void PlayerAttributesFrom(Packet packet)
        {
            RequestId = packet.ReadInt();
            Status = packet.ReadEPlayerStatus();
        }

        protected override void PlayerAttributesTo(Packet packet)
        {
            packet.Write(RequestId)
                .Write(Status);
        }
    }
}
