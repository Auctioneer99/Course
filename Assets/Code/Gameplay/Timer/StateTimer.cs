using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class StateTimer : IRuntimeDeserializable, IRuntimeStateObject<StateTimer>, ICensored
    {
        public TimeManager TimeManager { get; private set; }

        public EGameState EGameState { get; private set; }
        public Timer Timer { get; private set; }

        public GameController GameController => TimeManager.GameController;

        public StateTimer(TimeManager manager, StateTimerDefinition definition)
        {
            TimeManager = manager;
            EGameState = definition.EGameState;
            Timer = new Timer(GameController, definition.TimerDefinition);

            Timer.Started.CoreEvent.AddListener(OnTimerStarted);
            Timer.Elapsed.CoreEvent.AddListener(OnTimerElapsed);
        }

        public void Reset()
        {
            Timer.Reset();
        }

        private void OnTimerStarted(Timer timer)
        {
            GameController.EventManager.OnStateTimerStarted.Invoke(this);
        }

        private void OnTimerElapsed(Timer timer)
        {
            GameController.EventManager.OnStateTimerElapsed.Invoke(this);
        }

        public void Copy(StateTimer other, GameController controller)
        {
            EGameState = other.EGameState;
            Timer.Copy(other.Timer, controller);
        }

        public void Censor(EPlayer player)
        {
            Timer.Censor(player);
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            EGameState = packet.ReadEGameState();
            Timer.FromPacket(controller, packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EGameState)
                .Write(Timer);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[GameStateTimer]");
            sb.AppendLine($"EGameState = {EGameState}");
            sb.AppendLine(Timer.ToString());
            return sb.ToString();
        }
    }
}
