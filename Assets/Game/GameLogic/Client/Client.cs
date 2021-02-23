public class Client
{
    public GameDirector GameDirector { get; set; }

    public IGateway Gateway
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
    private IGateway _gateway;

    private ILogger _logger;

    public Client(GameDirector director, ILogger logger) 
    {
        _logger = logger;
        _logger?.Log("Initialized");
        GameDirector = director;
    }

    private void HandleCommand(int id, ICommand command)
    {
        _logger?.Log("Command received");
        ((IClientCommand)command).Execute(GameDirector);
    }

    public void Send(ICommand command)
    {
        _logger?.Log("Sending command");
        _gateway.Send(command);
    }
}
