using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

public class NetworkReceiver
{
    private int _port;
    private TcpListener _listener;
    private Server _server;
    private PacketConverter _converter;

    public NetworkReceiver(Server server, int port, PacketConverter converter)
    {
        _port = port;
        _server = server;
        _converter = converter;
        Initialize();
    }

    private void Initialize()
    {
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();

        BeginAccept();
    }

    private void BeginAccept()
    {
        _listener.BeginAcceptTcpClient(new AsyncCallback(ConnectCallback), null);
    }

    private void ConnectCallback(IAsyncResult result)
    {
        TcpClient socket = _listener.EndAcceptTcpClient(result);

        IGateway gateway = new NetworkGateway(socket, _converter, new UnityLogger("NetworkGatewayServer", "#000000"));
        int id = _server.AcceptConnection(gateway);
        if (id >= 0)
        {
            gateway.Initialize(id);
        }
        else
        {
            socket.Close();
            socket.Dispose();
        }

        BeginAccept();
    }
}
