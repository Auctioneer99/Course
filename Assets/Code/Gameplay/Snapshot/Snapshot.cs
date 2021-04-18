using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class Snapshot : IDeserializable
    {
        public Settings Settings { get; private set; }

        private Snapshot(GameController controller)
        {
            Settings = controller.GameInstance.Settings.Clone(controller);
        }

        public Snapshot(Packet packet)
        {
            FromPacket(packet);
        }

        public static Snapshot Create(GameController controller, EPlayer player)
        {
            Snapshot snapshot = new Snapshot(controller);
            snapshot.Settings.Censor(player);
            return snapshot;
        }

        public void Restore(GameController controller)
        {
            Debug.Log("Restoring client");
            controller.EventManager.OnSnapshotRestored.CoreEvent.Invoke();
        }

        public void FromPacket(Packet packet)
        {
            Settings = new Settings(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Settings);
        }
    }
}
