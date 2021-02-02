using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class LoggerManager
{
    public static ILogger NetworkClient => _networkClient;
    private static ILogger _networkClient = new UnityLogger("NetworkClient", "#6aec18");

    public static ILogger NetworkConnector => _networkConnector;
    private static ILogger _networkConnector = new UnityLogger("NetworkConnector", "#d7ff64");

    public static ILogger NetworkGatewayClient => _networkGatewayClient;
    private static ILogger _networkGatewayClient = new UnityLogger("NetworkGatewayClient", "#e4f200");

    public static ILogger Command => _command;
    private static ILogger _command = new UnityLogger("PrintCommand", "white");

    public static ILogger CCClient => _cCClient;
    private static ILogger _cCClient = new UnityLogger("CCClient", "#ebbc62");

    public static ILogger Server => _server;
    private static ILogger _server = new UnityLogger("Server", "#B42006");
}
