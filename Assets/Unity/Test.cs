using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using System.Net;
using System.Net.WebSockets;
using System;

namespace Gameplay.Unity
{
    public class Test : MonoBehaviour
    {
        private Packet _receivedPacket = new Packet();
        private ClientWebSocket _webSocket;
        // Start is called before the first frame update
        async void Start()
        {
            //App.Instance.SetupClient();


            _webSocket = new ClientWebSocket();
            CookieContainer cookies = new CookieContainer();
            Cookie cookie = new Cookie("AuthToken", "asdaser3r432rf");
            cookies.Add(cookie);

            _webSocket.Options.Cookies = cookies;

            Uri uri = new Uri("localhost:8000");
            CancellationToken token;
            Debug.Log("connecting with websocket");
            await _webSocket.ConnectAsync(uri, token);
            Debug.Log("after connecting " + _webSocket.State);
            BeginRead();
        }

        private async void BeginRead()
        {
            ArraySegment<byte> data;
            CancellationToken token;
            while (true)
            {
                await _webSocket.ReceiveAsync(data, token);
                Debug.Log(data);
                Debug.Log(_webSocket.State);
            }

        }
    }
}
