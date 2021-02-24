using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CCClientTest : MonoBehaviour
{
    public bool Enabled;

    [SerializeField]
    private FieldBuilder _builder;
    [SerializeField]
    private UnityUnitFactory _unitFactory;

    [SerializeField]
    private GameObject _server;
    private Client _client;
    private CurrentContextConnector _connector;

    private void Start()
    {
        if (Enabled)
        {
            //IPlaygroundFactory factory = new UnityPlaygroundFactory(new System.Numerics.Vector3(0, 0, 0), _builder, _unitFactory);
            GameDirector director = new GameDirector();

            director.Playground.FieldChanged += (field) =>
            {
                _builder.Build(new Vector3(0, 0, 0), field.Values);

                foreach(var u in director.Playground.Units)
                {
                    GameObject unit = _unitFactory.Spawn(u);
                    System.Numerics.Vector3 pos = director.Playground.TileAtUnit(u).Position;
                    unit.transform.position = new Vector3(pos.X, pos.Y, pos.Z);
                }
            };

            _client = new Client(director, LoggerManager.CCClient);
            _connector = new CurrentContextConnector(_client);

            CurrentContextReceiver receiver = _server.GetComponent<ServerTest>().CCReceiver;
            _connector.Connect(receiver);
        }
    }
}
