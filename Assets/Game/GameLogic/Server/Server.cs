using System.Collections;
using System.Collections.Generic;

public class Server
{
    private Dictionary<int, IGateway> _gateways;

    private int _maxConnections;
    private ILogger _logger;

    public IPlayground Playground => _playground;
    private IPlayground _playground;

    public Server(IPlayground playground, int maxConnections, ILogger logger)
    {
        _playground = playground;
        _maxConnections = maxConnections;
        _logger = logger;
        Initialize();
    }

    private void Initialize()
    {
        _gateways = new Dictionary<int, IGateway>();
    }

    public int AcceptConnection(IGateway gateway)
    {
        for(int i = 0; i < _maxConnections; i++)
        {
            if (_gateways.ContainsKey(i) == false)
            {
                _gateways[i] = gateway;
                gateway.Received += HandleCommand;
                return i;
            }
        }

        return -1;
    }

    private void HandleCommand(int id, ICommand command )
    {
        _logger.Log("Command received");
        ((IServerCommand)command).Execute(id, this);
    }

    public void Send(int id, ICommand command)
    {
        _gateways[id].Send(command);
    }

    public void Send(ICommand command, int exept = -1)
    {
        foreach(var value in _gateways)
        {
            if (value.Key != exept)
            {
                value.Value.Send(command);
            }
        }
    }
}
