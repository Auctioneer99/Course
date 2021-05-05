using UnityEngine;

namespace Gameplay
{
    public class ConnectInitializationAction : AAction, ITargetedAction, IAuthorityAction
    {
        public override EAction EAction => EAction.ConnectInitialization;

        public int Connection { get; private set; }
        public EPlayer PlayerGroup { get; private set; }
        public Snapshot Snapshot { get; private set; }
        public NetworkTarget Target => NetworkTarget.TargetPlayer;

        public ConnectInitializationAction Initialize(int connection, EPlayer player, Snapshot snapshot)
        {
            Initialize();
            Connection = connection;
            PlayerGroup = player;
            Snapshot = snapshot;
            return this;
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            ConnectInitializationAction action = copyFrom as ConnectInitializationAction;

            Connection = action.Connection;
            PlayerGroup = action.PlayerGroup;
            Snapshot = action.Snapshot.Clone(controller);
        }

        protected override void ApplyImplementation()
        {
            if (GameController.HasAuthority == false)
            {
                Debug.Log("[Client] Connection established, my id is " + Connection);
                Snapshot.Restore(GameController.GameInstance);
            } 
        }

        protected override void AttributesFrom(Packet packet)
        {
            Connection = packet.ReadInt();
            PlayerGroup = packet.ReadEPlayer();
            Snapshot = new Snapshot(packet);
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Connection)
                .Write(PlayerGroup)
                .Write(Snapshot);
        }
    }
}
