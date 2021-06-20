using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class TimerSettings : IDeserializable, IStateObject<TimerSettings>
    {
        public bool EnableTimers;
        public List<StateTimerDefinition> StateTimers;

        public TimerSettings()
        {

        }

        public TimerSettings(bool enableTimers)
        {
            EnableTimers = enableTimers;
            if (enableTimers)
            {
                StateTimers = TimerFactory.CreateDefaultDefinitions();
            }
        }

        public TimerSettings(Packet packet)
        {
            FromPacket(packet);
        }

        public TimerSettings(List<StateTimerDefinition> definitions)
        {
            EnableTimers = true;
            StateTimers.AddRange(definitions);
        }

        public void Copy(TimerSettings other)
        {
            EnableTimers = other.EnableTimers;
            StateTimers = other.StateTimers.Clone();
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
    }
}
