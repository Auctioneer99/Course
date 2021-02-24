using System;
using System.Collections;
using System.Collections.Generic;

public class CurrentContextGateway<T, N> : IGateway<T> where T: IPacketable where N: IPacketable

{
    public int ClientId => _clientId;
    private int _clientId;

    public event Action<int, T> Received;

    public CurrentContextGateway<N, T> Gateway;

    public CurrentContextGateway()
    {

    }


    public void Initialize(int id)
    {
        _clientId = id;
    }

    public void Send(IPacketable command)
    {
        Gateway.Accept((N)command);
    }

    public void Accept(T command)
    {
        Received?.Invoke(_clientId, command);
    }
}
