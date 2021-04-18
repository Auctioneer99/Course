using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;
using System.Threading;

namespace Gameplay
{
    public class TestServer : MonoBehaviour
    {
        private LocalConnector _server, _client1;

        private void Awake()
        {
            GameInstance server = new GameInstance(EGameMode.Server, new Settings(2));
            ServerDefinition serverDef = ServerDefinition.SetupOnline(server, 8000);
            serverDef.Start();
            _server = server.Controller.Network;
            Debug.Log("<color=green>Creating clients</color>");

            GameInstance client1 = new GameInstance(EGameMode.Client, new Settings(0));
            ClientDefinition clientdef1 = new ClientDefinition(client1);
            clientdef1.Start();
            clientdef1.ConnectToOnline("localhost", 8000);
            _client1 = client1.Controller.Network;


            GameInstance client2 = new GameInstance(EGameMode.Client, new Settings(0));
            ClientDefinition clientdef2 = new ClientDefinition(client2);
            clientdef2.Start();
            clientdef2.ConnectToLocal(serverDef);

            Debug.Log(_server);
            Debug.Log(_client1);
        }

        private void FixedUpdate()
        {
            if (_server.IsConnected)
            {
                _server.Manager.Update();
            }
            if (_client1.IsConnected)
            {
                _client1.Manager.Update();
            }
        }
    }
}
