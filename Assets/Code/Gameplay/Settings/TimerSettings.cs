using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Gameplay
{
    public struct TimerSettings : IDeserializable, IStateObject<TimerSettings>
    {
        public bool EnableTimers;
        public List<StateTimerDefinition> StateTimers;

        public TimerSettings(bool enableTimers)
        {
            EnableTimers = enableTimers;
            if (enableTimers)
            {
                StateTimers = TimerFactory.CreateDefaultDefinitions();
            }
            else
            {
                StateTimers = null;
            }
        }

        public void Copy(TimerSettings other)
        {
            EnableTimers = other.EnableTimers;
            StateTimers = new List<StateTimerDefinition>(other.StateTimers);
        }

        public void FromPacket(Packet packet)
        {
            EnableTimers = packet.ReadBool();
            int length = packet.ReadInt();
            StateTimers = new List<StateTimerDefinition>(length);
            for(int i = 0; i < length; i++)
            {
                StateTimers.Add(new StateTimerDefinition(packet));
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EnableTimers)
                .Write(StateTimers);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[TimerSettings]");
            sb.AppendLine($"EnableTimers = {EnableTimers}");
            sb.AppendLine("Timers");
            foreach (var t in StateTimers)
            {
                sb.Append(t.ToString());
            }

            return sb.ToString();
        }
    }
}
