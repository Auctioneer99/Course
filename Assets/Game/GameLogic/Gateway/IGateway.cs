using System;

public interface IGateway<T> where T: IPacketable
{
    int ClientId { get; }

    event Action<int, T> Received;

    void Initialize(int id);

    void Send(IPacketable packet);
}
