using System.Collections;
using System.Collections.Generic;

public class Server
{
    public class Client
    {
        public List<IClientCommand> IncludedCommands = new List<IClientCommand>();
        public List<IClientCommand> ExcludedCommands = new List<IClientCommand>();

        public int Id => _id;
        private int _id;

        public Client(int clientid) 
        {
            _id = clientid;
        }
    }

    public List<Client> Clients = new List<Client>();

    private Dictionary<int, IGateway> _gateways;

    private int _maxConnections;
    private ILogger _logger;

    public GameDirector GameDirector => _gameDirector;
    private GameDirector _gameDirector;

    public Server(GameDirector gameDirector, int maxConnections, ILogger logger)
    {
        _gameDirector = gameDirector;
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
