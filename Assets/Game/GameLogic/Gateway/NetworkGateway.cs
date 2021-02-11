using System;
using System.Collections.Generic;
using System.Net.Sockets;

public class NetworkGateway : IGateway
{
    public const int DATA_BUFFER_SIZE = 4096;

    public event Action<int, ICommand> Received;

    private Dictionary<int, Func<Packet, ICommand>> _packetHandlers;

    public int ClientId => _clientId;
    private int _clientId;

    private TcpClient _socket;
    private PacketParser _packetParser;

    private NetworkStream _stream;
    private byte[] _receiveBuffer;

    private ILogger _logger;

    public NetworkGateway(TcpClient client, PacketParser packetParser, Dictionary<int, Func<Packet, ICommand>> packetHandlers, ILogger logger)
    {
        _socket = client;
        _packetParser = packetParser;
        _packetHandlers = packetHandlers;
        _logger = logger;
    }

    public void Initialize(int id)
    {
        _clientId = id;
        _socket.ReceiveBufferSize = DATA_BUFFER_SIZE;
        _socket.SendBufferSize = DATA_BUFFER_SIZE;

        _packetParser.Parsed += CommandParsed;
        _receiveBuffer = new byte[DATA_BUFFER_SIZE];
        _stream = _socket.GetStream();
        BeginRead();
    }

    private void CommandParsed(Packet packet)
    {
        int packetId = packet.ReadInt();
        ICommand command = _packetHandlers[packetId](packet);
        Received?.Invoke(_clientId, command);
    }

    private void BeginRead()
    {
        _stream.BeginRead(_receiveBuffer, 0, DATA_BUFFER_SIZE, ReceiveCallback, null);

        void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = _stream.EndRead(result);
                if (byteLength <= 0)
                {
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(_receiveBuffer, data, byteLength);

                _packetParser.AddToParse(data);

                BeginRead();
            }
            catch (Exception ex)
            {
                _logger.Log("Error serializing packet");
                _logger.Log(ex.ToString());
            }
        }
    }

    public void Send(ICommand command)
    {
        try
        {
            using (Packet packet = command.ToPacket())
            {
                packet.WriteLength();
                _stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
            }
        }
        catch
        {
            _logger.Log("Error sending packet");
        }
    }
}
