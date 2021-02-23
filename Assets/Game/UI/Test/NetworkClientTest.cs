using UnityEngine;
using System.Linq;

public class NetworkClientTest : MonoBehaviour
{
    public bool Enabled;

    [SerializeField]
    private FieldBuilder _builder;
    [SerializeField]
    private UnityUnitFactory _unitFactory;

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
            //IPlaygroundFactory factory = new UnityPlaygroundFactory(new System.Numerics.Vector3(0, 0, 500), _builder, _unitFactory);
            GameDirector director = new GameDirector();

            director.Playground.FieldChanged += (field) =>
            {
                _builder.Build(new System.Numerics.Vector3(0, 0, 500), field.Values.Select(t => t.Position));

                foreach (var u in director.Playground.Units)
                {
                    GameObject unit = _unitFactory.Spawn(u);
                    System.Numerics.Vector3 pos = director.Playground.TileAtUnit(u).Position;
                    unit.transform.position = new Vector3(pos.X, pos.Y, pos.Z);
                }
            };

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
    }
}
