using System.Collections.Generic;
using UnityEngine;

public class CCClientTest : MonoBehaviour
{
    public bool Enabled;

    [SerializeField]
    private GameObject _builderObject;
    private FieldBuilder _builder;

    [SerializeField]
    private GameObject _server;
    private Client _client;
    private CurrentContextConnector _connector;

    private void Awake()
    {
        _builder = _builderObject.GetComponent<FieldBuilder>();
    }

    private void Start()
    {
        if (Enabled)
        {
            Playground playground = new PlaygroundDecorator(new Tile[0], _builder);
            _client = new Client(playground, LoggerManager.CCClient);
            _connector = new CurrentContextConnector(_client);

            CurrentContextReceiver receiver = _server.GetComponent<ServerTest>().CCReceiver;
            _connector.Connect(receiver);
        }
    }
}
