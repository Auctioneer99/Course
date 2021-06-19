using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class App : ASingleton<App>
    {
        public NetworkManager AdditionalNetwork { get; private set; }
        public NetworkManager LocalNetwork { get; private set; }

        private bool _hasPause => _pauseId >= 0;

        private int _pauseId = -1;

        public GameListenerManager Listener { get; private set; }
        public GameData GameData { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Listener = new GameListenerManager();
            GameData = GameData.Create();
        }

        public void Start()
        {
            SetupClient();
        }

        public void Update()
        {
            LocalNetwork.Update();
            //AdditionalNetwork?.Update();

            if (_hasPause)
            {
                if (Input.GetKey(KeyCode.F2))
                {
                    GameController controller = LocalNetwork.Host.Instance.Controller;
                    RemovePauseAction action = controller.ActionFactory.Create<RemovePauseAction>().Initialize(_pauseId);
                    _pauseId = -1;
                    controller.ActionDistributor.HandleAction(action);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.F1))
                {
                    GameController controller = LocalNetwork.Host.Instance.Controller;
                    PauseRequest pause = new PauseRequest(controller.PauseManager.AllocateId(), EPauseType.Timers | EPauseType.Logic, long.MaxValue);
                    UnityEngine.Debug.Log($"<color=black>{pause.ToString()}</color>");
                    AddPauseAction action = controller.ActionFactory.Create<AddPauseAction>().Initialize(pause);
                    _pauseId = pause.Id;
                    controller.ActionDistributor.HandleAction(action);
                }
            }
        }

        private void OnGameInitialized(GameController controller)
        {
            //Show screen
        }

        public void SetupClient()
        {
            GameInstance server = new GameInstance(GameData, EGameMode.Server, new Settings(1));
            ServerDefinition serverDef = ServerDefinition.SetupOnline(server, 8000);
            serverDef.Start();
            LocalNetwork = server.Controller.Network.Manager;
            Debug.Log("<color=green>Creating clients</color>");

            //GameInstance client1 = new GameInstance(EGameMode.Client, new Settings(0));
            //ClientDefinition clientdef1 = new ClientDefinition(client1);
            //clientdef1.Start();
            //clientdef1.ConnectToOnline("localhost", 8000);


            GameInstance client2 = new GameInstance(GameData, EGameMode.Client, new Settings(0));
            Listener.SetGame(client2);

            ClientDefinition clientdef2 = new ClientDefinition(client2);
            clientdef2.Start();
            clientdef2.ConnectToLocal(serverDef);


            //AdditionalNetwork = client1.Controller.Network.Manager;
        }

        public void SetupServer()
        {
            //TryEndCurrentGame();
            //Settings settings = new Settings(2);
            //GameInstance instance = new GameInstance(EGameMode.Server, settings);
            //CurrentGame = instance;
        }

        public void TryEndCurrentGame()
        {
            //if (CurrentGame == null)
           // {
           //     return;
          //  }
            //
        }
    }
}
