using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class ConnectInitializationAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.ConnectInitialization;

        public int ConnectionId { get; private set; }

        public EPlayer PlayerGroup { get; private set; }

        public Snapshot Snapshot { get; private set; }

        public ConnectInitializationAction Initialize(int connection, EPlayer player, Snapshot snapshot)
        {
            base.Initialize();
            ConnectionId = connection;
            PlayerGroup = player;
            Snapshot = snapshot;
            return this;
        }

        protected override void ApplyImplementation()
        {
            Debug.Log(GameController);
            if (GameController.HasAuthority == false)
            {
                Debug.Log("[Client] Connection established, my id is " + ConnectionId);
                Snapshot.Restore(GameController);
            } 
        }

        public override void Copy(AAction copyFrom, GameController controller)
        {
            ConnectInitializationAction action = copyFrom as ConnectInitializationAction;
            base.Copy(copyFrom, controller);

            ConnectionId = action.ConnectionId;
            PlayerGroup = action.PlayerGroup;
            Snapshot = action.Snapshot;
        }

        protected override void AttributesFrom(Packet packet)
        {
            ConnectionId = packet.ReadInt();
            PlayerGroup = packet.ReadEPlayer();
            Snapshot = new Snapshot(packet);
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(ConnectionId)
                .Write(PlayerGroup)
                .Write(Snapshot);
        }
    }
}
