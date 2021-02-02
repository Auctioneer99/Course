using UnityEngine;

public class CCClientTest : MonoBehaviour
{
    [SerializeField]
    private GameObject _server;
    private Client _client;
    private CurrentContextConnector _connector;

    void Start()
    {
        _client = new Client(new UnityLogger("CCClient", "#ebbc62"));
        _connector = new CurrentContextConnector(_client);

        CurrentContextReceiver receiver = _server.GetComponent<ServerTest>().CCReceiver;
        _connector.Connect(receiver);
    }
}
