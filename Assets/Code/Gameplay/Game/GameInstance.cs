namespace Gameplay
{
    public class GameInstance
    {
        public Logger Logger { get; private set; }

        public GameController Controller { get; private set; }

        public Settings Settings { get; private set; }

        public EGameMode Mode { get; private set; }
        public bool HasAuthority => EGameMode.Authority.Contains(Mode);
        public bool IsMultiplayer => EGameMode.Multiplayer.Contains(Mode);

        public GameInstance(EGameMode mode, Settings settings)
        {
            string color = mode == EGameMode.Server ? "red" : "blue";
            Logger = new Logger(color);

            Mode = mode;
            Settings = settings;

            Controller = new GameController(this, true);

            if (HasAuthority == false)
            {
                Settings.TimerSettings.EnableTimers = false;
            }
            else
            {
                Settings.TimerSettings.EnableTimers = true;
            }
        }

        public void Start()
        {
            Controller.Start();
        }

        public void Update()
        {
            Controller.Update();
        }
    }
}
