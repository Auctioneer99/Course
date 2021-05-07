using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class StateTimer : IRuntimeDeserializable, IStateObjectCloneable<StateTimer>, ICensored
    {
        public TimeManager TimeManager { get; private set; }

        public EGameState EGameState { get; private set; }
        public Timer Timer { get; private set; }

        public GameController GameController => TimeManager.GameController;

        private StateTimer() { }

        public StateTimer(TimeManager manager, Packet packet)
        {
            FromPacket(manager.GameController, packet);

            Initialize();
        }

        public StateTimer(TimeManager manager, StateTimerDefinition definition)
        {
            TimeManager = manager;
            EGameState = definition.EGameState;
            Timer = new Timer(GameController, definition.TimerDefinition);

            Initialize();
        }

        private void Initialize()
        {
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

        public StateTimer Clone(GameController controller)
        {
            StateTimer timer = new StateTimer();
            timer.Copy(this, controller);
            return timer;
        }

        public void Copy(StateTimer other, GameController controller)
        {
            TimeManager = controller.StateMachine.TimeManager;
            EGameState = other.EGameState;
            Timer = other.Timer.Clone(controller);

            Initialize();
        }

        public void Censor(EPlayer player)
        {
            Timer.Censor(player);
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            TimeManager = controller.StateMachine.TimeManager;
            EGameState = packet.ReadEGameState();
            Timer = new Timer(controller, packet);
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
