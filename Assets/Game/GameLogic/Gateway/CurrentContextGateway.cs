using System;
using System.Collections;
using System.Collections.Generic;

public class CurrentContextGateway : IGateway
{
    public int ClientId => _clientId;
    private int _clientId;

    public event Action<int, ICommand> Received;

    public CurrentContextGateway Gateway;

    public CurrentContextGateway()
    {

    }


    public void Initialize(int id)
    {
        _clientId = id;
    }

    public void Send(ICommand command)
    {
        Gateway.Accept(command);
    }

    public void Accept(ICommand command)
    {
        Received?.Invoke(_clientId, command);
    }
}
