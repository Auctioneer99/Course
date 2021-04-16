﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;
using UnityEngine;

namespace Gameplay
{
    public class ClientDefinition
    {
        public GameInstance GameInstance { get; private set; }

        private NetworkManager _networkManager;
        private OnlineConnector _onlineConnector;

        public ClientDefinition(GameInstance instance)
        {
            GameInstance = instance;
        }

        public void ConnectToLocal(ServerDefinition server)
        {
            server.LocalListener.Connect(GameInstance);
        }

        public void ConnectToOnline(string ip, int port)
        {
            WebSocket webClient = new WebSocket($"ws://{ip}:{port}/game");
            _onlineConnector = new OnlineConnector(webClient, new Logger("red"));
            _onlineConnector.Initialize(0);


            EventHandler<MessageEventArgs> initializer = null;

            initializer = (sender, e) =>
            {
                webClient.OnMessage -= initializer;

                ConnectInitializationAction action = ParsePacket(new Packet(e.RawData)) as ConnectInitializationAction;

                _networkManager = new NetworkManager(GameInstance.Controller.Network, action.ConnectionId, new Logger("red"));
                _networkManager.Add(_onlineConnector);

                Debug.Log("[Client] Connection established, my id is " + action.ConnectionId);

                webClient.OnMessage += OnMessageHandler;
            };

            webClient.OnOpen += OnOpenHandler;
            webClient.OnMessage += initializer;
            webClient.OnClose += OnCloseHandler;
            webClient.OnError += OnErrorHandler;

            webClient.Compression = CompressionMethod.Deflate;
            webClient.Origin = "http://sharp.com";

            webClient.SetCookie(new Cookie("AuthToken", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOiJiMDhmODZhZi0zNWRhLTQ4ZjItOGZhYi1jZWYzOTA0NjYwYmQifQ.-xN_h82PHVTCMA9vdoHrcZxH-x5mb11y1537t3rGzcM"));
            webClient.ConnectAsync();
        }

        private void OnOpenHandler(object sender, EventArgs e)
        {

        }

        private void OnMessageHandler(object sender, MessageEventArgs e)
        {
            AAction parsed = ParsePacket(new Packet(e.RawData));
            _onlineConnector.Send(parsed);
        }

        private void OnErrorHandler(object sender, ErrorEventArgs e)
        {
            Debug.Log("[Client] Error");
            Debug.Log(e.Message);
            Debug.Log(e.Exception.ToString());
        }

        private void OnCloseHandler(object sender, CloseEventArgs e)
        {
            Debug.Log("[Client] ConnectionClosed");
            Debug.Log(e.Reason);
        }

        private AAction ParsePacket(Packet packet)
        {
            return packet.ReadAction(GameInstance.Controller);
        }
    }
}
