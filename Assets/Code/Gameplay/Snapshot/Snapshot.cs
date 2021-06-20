using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class Snapshot : IDeserializable, IStateObjectCloneable<Snapshot>
    {
        public Packet GameInstancePacket { get; private set; }

        public GameInstance GameInstance { get; private set; }
        //public Settings Settings { get; private set; }
        //public GameController GameController { get; private set; }

        private Snapshot() { }

        private Snapshot(GameInstance instance)
        {
            //GameInstancePacket = new Packet();
            //GameInstancePacket.Write(instance);
            //Settings = controller.GameInstance.Settings.Clone(controller);
            GameInstance = instance.Clone();
        }

        public Snapshot(Packet packet)
        {
            FromPacket(packet);
        }

        public static Snapshot Create(GameInstance instance, EPlayer player)
        {
            GameInstance newInst = instance.Clone();
            newInst.Censor(player);
            Snapshot snapshot = new Snapshot(newInst);
            //snapshot.Settings.Censor(player);
            //Debug.Log("Snapshot created ");
            //Debug.Log(snapshot.Settings);
            return snapshot;
        }

        public void Restore(GameInstance instance)
        {
            //Debug.Log(string.Join(", ", GameInstancePacket.ToArray()));
            instance.Copy(GameInstance);
            //instance.FromPacket(GameInstancePacket);
            //instance.Copy(GameInstance);
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
            //int len = packet.ReadInt();
          //  GameInstancePacket = packet;// new Packet(packet.ReadBytes(len));
            //GameInstance = new GameInstance(packet);
            //GameController = new GameController(packet);
            //Settings = new Settings(packet);
        }

        public void ToPacket(Packet packet)
        {
            //var arr = GameInstancePacket.ToArray();
          //  packet
                //.Write(arr.Length)
          //      .Write(GameInstance);
            //packet.Write(GameInstance);
        }
        /*
        public void Censor(EPlayer player)
        {
            GameInstance.Censor(player);
        }*/

        public Snapshot Clone()
        {
            Snapshot ss = new Snapshot();
            ss.Copy(this);
            return ss;
        }

        public void Copy(Snapshot other)
        {
            //GameInstancePacket = other.GameInstancePacket;
            GameInstance = other.GameInstance.Clone();
        }
    }
}
