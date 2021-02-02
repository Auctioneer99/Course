﻿public class Client
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
        _logger?.Log("Initialized");
    }

    public void HandleCommand(int id, ICommand command)
    {
        _logger?.Log("Command received");
        command.Execute(id);
    }

    public void Send(ICommand command)
    {
        _logger?.Log("Sending command");
        _gateway.Send(command);
    }
}
