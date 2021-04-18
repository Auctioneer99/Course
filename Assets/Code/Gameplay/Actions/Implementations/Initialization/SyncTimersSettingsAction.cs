namespace Gameplay
{
    public class SyncTimersSettingsAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SyncTimersSettings;

        public TimerSettings Settings { get; private set; }

        public SyncTimersSettingsAction Initialize(TimerSettings settings)
        {
            Settings = settings;
            return this;
        }

        protected override void ApplyImplementation()
        {
            if (GameController.HasAuthority == false)
            {
                TimerSettings currentSettings = GameController.GameInstance.Settings.TimerSettings;
                currentSettings.EnableTimers = Settings.EnableTimers;
                currentSettings.Timers = Settings.Timers;

                GameController.TimeManager.SetupTimers();
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
