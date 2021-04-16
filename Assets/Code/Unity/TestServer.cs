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
        private void Awake()
        {
            GameInstance server = new GameInstance(EGameMode.Server, new Settings(2));
            ServerDefinition serverDef = ServerDefinition.SetupOnline(server, 8000);
            serverDef.Start();

            Debug.Log("<color=green>Creating clients</color");

            GameInstance client1 = new GameInstance(EGameMode.Client, new Settings(0));
            ClientDefinition clientdef1 = new ClientDefinition(client1);
            clientdef1.ConnectToOnline("localhost", 8000);

            Thread.Sleep(100);

            GameInstance client2 = new GameInstance(EGameMode.Client, new Settings(0));
            ClientDefinition clientdef2 = new ClientDefinition(client2);
            clientdef1.ConnectToLocal(serverDef);
        }
    }
}
