using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace Gameplay
{
    public class ServerDefinition
    {
        public GameInstance GameInstance { get; private set; }

        public WebSocketServer OnlineListener { get; private set; }

        public LocalListener LocalListener { get; private set; }

        public void Start()
        {
            if (OnlineListener.IsListening || LocalListener.IsStarted)
            {
                throw new Exception("Definition already started listening");
            }
            GameInstance.Start();
            OnlineListener?.Start();
            LocalListener.Start();
        }

        public static ServerDefinition SetupLocal(GameInstance game)
        {
            ServerDefinition definition = new ServerDefinition();
            definition.GameInstance = game;

            NetworkManager _networkManager = new NetworkManager(game.Controller.Network, 0, EPlayer.Server, new Logger("blue"));
            definition.LocalListener = new LocalListener(_networkManager);

            return definition;
        }

        public static ServerDefinition SetupOnline(GameInstance game, int port)
        {
            ServerDefinition definition = new ServerDefinition();
            definition.GameInstance = game;

            WebSocketServer webServer = new WebSocketServer(port, false);
            NetworkManager _networkManager = new NetworkManager(game.Controller.Network, 0, EPlayer.Server, new Logger("blue"));
            webServer.AddWebSocketService<GameLobby>("/game", () =>
                new GameLobby(_networkManager)
            );

            definition.OnlineListener = webServer;

            definition.LocalListener = new LocalListener(_networkManager);

            return definition;
        }
    }
}
