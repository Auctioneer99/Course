using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay.Unity
{
    public class App : ASingleton<App>
    {
        public GameInstance CurrentGame { get; private set; }
        public GameInstance LocalServer { get; private set; }


        private GameListenerManager _listener;

        private void Start()
        {
            _listener = new GameListenerManager();
        }

        private void OnGameInitialized(GameController controller)
        {
            //Show screen
        }

        public void SetupClient()
        {
            Settings settings = new Settings(2);
            GameInstance instance = new GameInstance(EGameMode.Server, settings);
            LocalServer = instance;
            instance.Controller.SetupServer(8000);

            settings = new Settings(0);
            instance = new GameInstance(EGameMode.Client, settings);
            instance.Controller.EventManager.OnGameInitialized.CoreEvent.AddListener(OnGameInitialized, true, int.MaxValue);
            _listener.SetGame(instance.Controller);
            CurrentGame = instance;

            instance.Controller.SetupClient("localhost", 8000);
        }

        public void SetupServer()
        {
            TryEndCurrentGame();
            Settings settings = new Settings(2);
            GameInstance instance = new GameInstance(EGameMode.Server, settings);
            CurrentGame = instance;
        }

        public void TryEndCurrentGame()
        {
            if (CurrentGame == null)
            {
                return;
            }
            //
        }
    }
}
