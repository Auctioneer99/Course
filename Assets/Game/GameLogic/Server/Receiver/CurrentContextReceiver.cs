using System.Collections;
using System.Collections.Generic;

public class CurrentContextReceiver
{
    private Server _server;

    public CurrentContextReceiver(Server server)
    {
        _server = server;
    }

    public bool Accept(CurrentContextGateway clientSide)
    {
        CurrentContextGateway serverSide = new CurrentContextGateway();
        int id = _server.AcceptConnection(serverSide);
        if (id >= 0)
        {
            serverSide.Gateway = clientSide;
            clientSide.Gateway = serverSide;
            serverSide.Initialize(id);
            return true;
        }
        return false;
    }
}
