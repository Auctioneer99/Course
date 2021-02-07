using UnityEngine;

public class NetworkClientTest : MonoBehaviour
{
    public bool Enabled;

    private ThreadManager _threadManager;
    private Client _client;
    private NetworkConnector _networkConnector;
    private PacketParser _packetParser;

    private string ip = "127.0.0.1";
    private int port = 2000;

    private void Start()
    {
        if (Enabled)
        {
            Playground playground = new Playground(new Tile[0]);
            _client = new Client(playground, LoggerManager.NetworkClient);

            _threadManager = new ThreadManager();
            _packetParser = new PacketParser(_threadManager);
            _networkConnector = new NetworkConnector(_client, _packetParser);

            _networkConnector.Connect(ip, port);
        }
    }

    private void Update()
    {
        if (Enabled)
        {
            _threadManager.Update();
        }
    }
}
