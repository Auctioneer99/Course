using System.Collections.Generic;

namespace Gameplay
{
    public class SyncTimersSettingsAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SyncTimersSettings;

        public TimerSettings Settings { get; private set; }

        public SyncTimersSettingsAction()
        {
        }

        public SyncTimersSettingsAction Initialize(TimerSettings settings)
        {
            Initialize();

            Settings = settings;
            return this;
        }

        public override bool IsValid()
        {
            return true;
        }

        protected override void CopyImplementation(AAction copyFrom, GameController controller)
        {
            Settings.Copy((copyFrom as SyncTimersSettingsAction).Settings);
        }

        protected override void ApplyImplementation()
        {
            if (GameController.HasAuthority == false)
            {
                TimerSettings currentSettings = GameController.GameInstance.Settings.TimerSettings;
                currentSettings.EnableTimers = Settings.EnableTimers;
                currentSettings.StateTimers = new List<StateTimerDefinition>(Settings.StateTimers);

                GameController.StateMachine.TimeManager.SetupTimers();
            }
        }

        protected override void AttributesFrom(Packet packet)
        {
            Settings.FromPacket(packet);
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Settings);
        }
    }
}
