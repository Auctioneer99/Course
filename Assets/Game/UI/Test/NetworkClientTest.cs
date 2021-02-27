using UnityEngine;
using System.Linq;

public class NetworkClientTest : MonoBehaviour
{
    public bool Enabled;

    [SerializeField]
    private FieldBuilder _builder;
    [SerializeField]
    private UnityUnitFactory _unitFactory;
    [SerializeField]
    private GameObject _uiPrefab;


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
            GameDirector director = new GameDirector();

            // UI
            GameObject uiObj = Instantiate(_uiPrefab);
            UI ui = uiObj.GetComponent<UI>();
            ui.Director = director;
            ui.Client = _client;
            director.Playground.FieldChanged += (field) =>
            {
                _builder.Build(new Vector3(0, 0, 500), field.Values);
            };


            // networking
            _client = new Client(director, LoggerManager.NetworkClient);

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

        if (Input.GetKeyDown(KeyCode.F2))
        {
            IServerCommand command = new JoinAsPlayer("polya", "", Team.Blue);
            _client.Send(command);
        }
    }
}
