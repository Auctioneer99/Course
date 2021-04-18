using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class TimerSettings : IDeserializable, IStateObjectCloneable<TimerSettings>
    {
        public bool EnableTimers;
        public List<TimerDefenition> Timers;

        public TimerSettings()
        {

        }

        public TimerSettings(bool enableTimers)
        {
            EnableTimers = enableTimers;
            if (enableTimers)
            {
                Timers = TimerFactory.CreateDefaultDefinitions();
            }
        }

        public TimerSettings(Packet packet)
        {
            FromPacket(packet);
        }

        public TimerSettings(List<TimerDefenition> definitions)
        {
            EnableTimers = true;
            Timers.AddRange(definitions);
        }

        public TimerSettings Clone(GameController controller)
        {
            TimerSettings settings = new TimerSettings();
            settings.Copy(this, controller);
            return settings;
        }

        public void Copy(TimerSettings other, GameController controller)
        {
            EnableTimers = other.EnableTimers;
            Timers = other.Timers.Clone(controller);
        }

        public void FromPacket(Packet packet)
        {
            EnableTimers = packet.ReadBool();
            int length = packet.ReadInt();
            Timers = new List<TimerDefenition>(length);
            for(int i = 0; i < length; i++)
            {
                Timers.Add(new TimerDefenition(packet));
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EnableTimers)
                .Write(Timers);
        }
    }
}
