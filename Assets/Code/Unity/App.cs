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


        public GameListenerManager Listener { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Listener = new GameListenerManager();
        }

        public void Start()
        {
            SetupClient();
        }

        public void Update()
        {
            LocalNetwork.Update();
            //AdditionalNetwork?.Update();
        }

        private void OnGameInitialized(GameController controller)
        {
            //Show screen
        }

        public void SetupClient()
        {
            GameInstance server = new GameInstance(EGameMode.Server, new Settings(1));
            ServerDefinition serverDef = ServerDefinition.SetupOnline(server, 8000);
            serverDef.Start();
            LocalNetwork = server.Controller.Network.Manager;
            Debug.Log("<color=green>Creating clients</color>");

            //GameInstance client1 = new GameInstance(EGameMode.Client, new Settings(0));
            //ClientDefinition clientdef1 = new ClientDefinition(client1);
            //clientdef1.Start();
            //clientdef1.ConnectToOnline("localhost", 8000);


            GameInstance client2 = new GameInstance(EGameMode.Client, new Settings(0));
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
