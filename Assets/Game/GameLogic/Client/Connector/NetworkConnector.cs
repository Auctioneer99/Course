using System;
using System.Net.Sockets;

public class NetworkConnector
{
    private Client _client;
    private PacketConverter _packetConverter;

    private int _id = -1;

    private ILogger _logger = new UnityLogger("NetworkConnector", "#d7ff64");

    public NetworkConnector(Client client, PacketConverter packetConverter) 
    {
        _client = client;
        _packetConverter = packetConverter;
    }

    public void Connect(string ip, int port)
    {
        TcpClient socket = new TcpClient();

        socket.BeginConnect(ip, port, ConnectCallback, socket);

        void ConnectCallback(IAsyncResult result)
        {
            socket.EndConnect(result);

            if (socket.Connected == true)
            {
                _logger.Log("Connected");
                NetworkGateway gateway = new NetworkGateway(socket, _packetConverter, new UnityLogger("NetworkGatewayClient", "#e4f200"));
                gateway.Initialize(_id);
                _client.Gateway = gateway;

                ICommand command = new PrintCommand(_id, "hello from client");
                _client.Send(command);
            }
        }
    }
}
