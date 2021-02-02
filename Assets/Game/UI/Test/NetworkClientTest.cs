using UnityEngine;

public class NetworkClientTest : MonoBehaviour
{
    private ThreadManager _threadManager;
    private Client _client;
    private NetworkConnector _networkConnector;
    private PacketConverter _packetConverter;

    private string ip = "127.0.0.1";
    private int port = 2000;

    private void Start()
    {
        _client = new Client(new UnityLogger("NetworkClient", "#6aec18"));

        _threadManager = new ThreadManager();
        _packetConverter = new PacketConverter(_threadManager);
        _networkConnector = new NetworkConnector(_client, _packetConverter);

        _networkConnector.Connect(ip, port);
    }

    private void Update()
    {
        _threadManager.Update();
    }
}
