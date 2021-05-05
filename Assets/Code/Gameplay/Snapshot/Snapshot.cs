using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class Snapshot : IDeserializable, ICensored, IStateObjectCloneable<Snapshot>
    {
        public GameInstance GameInstance { get; private set; }
        //public Settings Settings { get; private set; }
        //public GameController GameController { get; private set; }

        private Snapshot() { }

        private Snapshot(GameInstance instance)
        {
            //Settings = controller.GameInstance.Settings.Clone(controller);
            GameInstance = instance.Clone();
        }

        public Snapshot(Packet packet)
        {
            FromPacket(packet);
        }

        public static Snapshot Create(GameInstance instance, EPlayer player)
        {
            Snapshot snapshot = new Snapshot(instance);
            snapshot.GameInstance.Censor(player);
            //snapshot.Settings.Censor(player);
            //Debug.Log("Snapshot created ");
            //Debug.Log(snapshot.Settings);
            return snapshot;
        }

        public void Restore(GameInstance instance)
        {
            instance.Copy(GameInstance);
            instance.SnapshotRestored.Invoke(instance);
            Debug.Log(instance.ToString());
            //controller.GameInstance.Settings.Copy(GameInstance.Settings, controller);
            //Debug.Log("Restoring client");
            //Debug.Log(Settings);
            //Debug.Log("------");
            //Debug.Log(controller.GameInstance.Settings);
            //controller.Copy(GameController);
            //controller.Reset();
            //controller.EventManager.OnSnapshotRestored.CoreEvent.Invoke();
        }

        public void FromPacket(Packet packet)
        {
            GameInstance = new GameInstance(packet);
            //GameController = new GameController(packet);
            //Settings = new Settings(packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(GameInstance);
        }

        public void Censor(EPlayer player)
        {
            GameInstance.Censor(player);
        }

        public Snapshot Clone(GameController controller)
        {
            Snapshot ss = new Snapshot();
            ss.Copy(this, controller);
            return ss;
        }

        public void Copy(Snapshot other, GameController controller)
        {
            GameInstance = other.GameInstance.Clone();
        }
    }
}
