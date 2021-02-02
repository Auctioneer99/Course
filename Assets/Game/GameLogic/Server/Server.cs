using System.Collections;
using System.Collections.Generic;

public class Server
{
    private Dictionary<int, IGateway> _gateways;

    private int _maxConnections;
    private ILogger _logger;

    public Server(int maxConnections, ILogger logger)
    {
        _maxConnections = maxConnections;
        _logger = logger;
        Initialize();
    }

    public void Initialize()
    {
        _gateways = new Dictionary<int, IGateway>();
    }

    public int AcceptConnection(IGateway gateway)
    {
        for(int i = 0; i < _maxConnections; i++)
        {
            if (_gateways.TryGetValue(i, out var junk) == false)
            {
                _gateways[i] = gateway;
                gateway.Received += HandleCommand;
                return i;
            }
        }

        return -1;
    }

    private void HandleCommand(int id, ICommand command)
    {
        _logger.Log("Command received");
        command.Execute(id);

        ICommand response = new PrintCommand(id, "Hello from server");
        Send(id, response);
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
