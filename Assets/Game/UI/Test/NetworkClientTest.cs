using UnityEngine;

public class NetworkClientTest : MonoBehaviour
{
    public bool Enabled;

    [SerializeField]
    private GameObject _builderObject;
    private FieldBuilder _builder;

    private ThreadManager _threadManager;
    private Client _client;
    private NetworkConnector _networkConnector;
    private PacketParser _packetParser;

    private string ip = "127.0.0.1";
    private int port = 2000;

    private void Awake()
    {
        _builder = _builderObject.GetComponent<FieldBuilder>();
    }

    private void Start()
    {
        if (Enabled)
        {
            IPlaygroundFactory factory = new UnityPlaygroundFactory(new System.Numerics.Vector3(0, 0, 500), _builder);

            _client = new Client(factory, LoggerManager.NetworkClient);

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
