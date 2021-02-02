using UnityEngine;

public class ServerTest : MonoBehaviour
{
    private const int PORT = 2000;

    private Server _server;

    public CurrentContextReceiver CCReceiver => _ccReceiver;
    private CurrentContextReceiver _ccReceiver;

    private NetworkReceiver _networkReceiver;

    private PacketConverter _converter;
    private ThreadManager _threadManager;

    private void Awake()
    {
        _threadManager = new ThreadManager();

        _server = new Server(50, new UnityLogger("Server", "#B42006"));

        _converter = new PacketConverter(_threadManager);
        _networkReceiver = new NetworkReceiver(_server, PORT, _converter);

        _ccReceiver = new CurrentContextReceiver(_server);
    }

    void Update()
    {
        _threadManager.Update();
    }
}
