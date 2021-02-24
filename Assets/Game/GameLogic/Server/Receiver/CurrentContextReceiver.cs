using System.Collections;
using System.Collections.Generic;

public class CurrentContextReceiver
{
    private Server _server;

    public CurrentContextReceiver(Server server)
    {
        _server = server;
    }

    public bool Accept(CurrentContextGateway<IClientCommand, IServerCommand> clientSide)
    {
        CurrentContextGateway<IServerCommand, IClientCommand> serverSide = new CurrentContextGateway<IServerCommand, IClientCommand>();
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
