using System;

namespace Gameplay
{
    public class ReportPlayerStatusAction : APlayerAction, IUserAction
    {
        public override EAction EAction => EAction.ReportPlayerStatus;

        public bool ValidWhenBlocked => true;

        public int RequestId { get; private set; }
        public EPlayerStatus Status { get; private set; }

        public ReportPlayerStatusAction Initialize(EPlayer player, int requestId, EPlayerStatus status)
        {
            Initialize(player);
            RequestId = requestId;
            Status = status;
            return this;
        }

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

        protected override void ApplyImplementation()
        {
            Player player = GameController.PlayerManager.GetPlayer(EPlayer);
            if (GameController.HasAuthority)
            {
                RequestPlayerFinishedReport request = GameController.RequestHolder.Get<RequestPlayerFinishedReport>(EPlayer, RequestId);

            }
            player.EStatus = Status;
        }

        protected override void AttributesFrom(Packet packet)
        {
            base.AttributesFrom(packet);

            RequestId = packet.ReadInt();
            Status = packet.ReadEPlayerStatus();
        }

        protected override void AttributesTo(Packet packet)
        {
            base.AttributesTo(packet);

            packet.Write(RequestId)
                .Write(Status);
        }
    }
}
