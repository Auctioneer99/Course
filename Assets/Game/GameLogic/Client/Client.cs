public class Client
{
    public GameDirector GameDirector { get; set; }

    public IGateway<IClientCommand> Gateway
    {
        get
        {
            return _gateway;
        }
        set
        {
            if (_gateway != null)
            {
                _gateway.Received -= HandleCommand;
            }

            _gateway = value;
            _gateway.Received += HandleCommand;
        }
    }
    private IGateway<IClientCommand> _gateway;

    private ILogger _logger;

    public Client(GameDirector director, ILogger logger) 
    {
        _logger = logger;
        _logger?.Log("Initialized");
        GameDirector = director;
    }

    private void HandleCommand(int id, IClientCommand command)
    {
        _logger?.Log("Command received");
        command.Execute(GameDirector);
    }

    public void Send(IPacketable command)
    {
        _logger?.Log("Sending command");
        _gateway.Send(command);
    }
}
