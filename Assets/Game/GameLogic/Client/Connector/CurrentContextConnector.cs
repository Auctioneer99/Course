using System.Collections;
using System.Collections.Generic;

public class CurrentContextConnector
{
    private Client _client;
    private int _id = -1;

    public CurrentContextConnector(Client client)
    {
        _client = client;
    }

    public void Connect(CurrentContextReceiver receiver)
    {
        CurrentContextGateway gateway = new CurrentContextGateway();
        if (receiver.Accept(gateway))
        {
            gateway.Initialize(_id);
            _client.Gateway = gateway;

            ICommand command = new PrintCommand(_id, "hello from client");
            _client.Send(command);
        }
    }
}
