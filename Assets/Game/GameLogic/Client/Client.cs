public class Client
{
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

    public Client(ILogger logger) 
    {
        _logger = logger;
        _logger?.Log("initialized");
    }

    public void HandleCommand(int id, ICommand command)
    {
        _logger?.Log("command received");
        command.Execute();
    }

    public void Send(ICommand command)
    {
        _logger?.Log("sending command");
        _gateway.Send(command);
    }
}
