using System;

public class PacketParser
{
    public event Action<Packet> Parsed;

    private ThreadManager _threadManager;
    private Packet _receivedPacket;

    public PacketParser(ThreadManager threadManager)
    {
        _threadManager = threadManager;
        Initialize();
    }

    private void Initialize()
    {
        _receivedPacket = new Packet();
    }

    public void AddToParse(byte[] data)
    {
        bool handled = HandleData(data);
        _receivedPacket.Reset(handled);
    }

    private bool HandleData(byte[] data)
    {
        int packetLength;

        _receivedPacket.SetBytes(data);

        packetLength = 0;
        if (_receivedPacket.UnreadLength() >= 4)
        {
            packetLength = _receivedPacket.ReadInt();
            if (packetLength <= 0)
            {
                return true;
            }
        }

        while (HasEnoughDataForPacket(packetLength))
        {
            byte[] packetBytes = _receivedPacket.ReadBytes(packetLength);

            CreatePacket(packetBytes);

            packetLength = 0;
            if (_receivedPacket.UnreadLength() >= 4)
            {
                packetLength = _receivedPacket.ReadInt();
                if (packetLength <= 0)
                {
                    return true;
                }
            }
        }

        if (packetLength <= 1)
        {
            return true;
        }

        return false;
    }

    private bool HasEnoughDataForPacket(int packetLength)
    {
        return packetLength > 0 && packetLength <= _receivedPacket.UnreadLength();
    }

    private void CreatePacket(byte[] data)
    {
        //_threadManager.ExecuteOnMainThread(() =>
        // {
            using (Packet packet = new Packet(data))
            {
                Parsed?.Invoke(packet);
            }
        //});
    }
}
