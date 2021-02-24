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
        CurrentContextGateway<IClientCommand, IServerCommand> gateway = new CurrentContextGateway<IClientCommand, IServerCommand>();
        if (receiver.Accept(gateway))
        {
            gateway.Initialize(_id);
            _client.Gateway = gateway;

            IServerCommand command = new GetInitialData();

            _client.Send(command);
        }
    }
}
