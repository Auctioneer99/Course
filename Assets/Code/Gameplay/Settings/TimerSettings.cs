using System.Collections.Generic;

namespace Gameplay
{
    public class TimerSettings : IDeserializable
    {
        public bool EnableTimers;
        public List<TimerDefenition> Timers;

        public TimerSettings(bool enableTimers)
        {
            EnableTimers = enableTimers;
            if (enableTimers)
            {
                Timers = TimerFactory.CreateDefaultDefinitions();
            }
        }

        public TimerSettings(List<TimerDefenition> definitions)
        {
            EnableTimers = true;
            Timers.AddRange(definitions);
        }

        public void FromPacket(Packet packet)
        {
            EnableTimers = packet.ReadBool();
            int length = packet.ReadInt();
            Timers = new List<TimerDefenition>(length);
            for(int i = 0; i < length; i++)
            {
                TimerDefenition timer = new TimerDefenition();
                timer.FromPacket(packet);
                Timers.Add(timer);
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EnableTimers)
                .Write(Timers);
        }
    }
}
