using System;
using System.Net.Sockets;

public class NetworkConnector
{
    private Client _client;
    private PacketParser _packetParser;

    private int _id = -1;

    private ILogger _logger = LoggerManager.NetworkConnector;

    public NetworkConnector(Client client, PacketParser packetParser) 
    {
        _client = client;
        _packetParser = packetParser;
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
                NetworkGateway gateway = new NetworkGateway(socket, _packetParser, ServerPacketHandler.Handlers, LoggerManager.NetworkGatewayClient);
                gateway.Initialize(_id);
                _client.Gateway = gateway;

                IServerCommand command = new JoinAsSpectator();

                //IServerCommand command = new PrintCommand(_id, "hello from client");
                _client.Send(command);
            }
        }
    }
}
