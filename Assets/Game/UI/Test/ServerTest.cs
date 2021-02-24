using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServerTest : MonoBehaviour
{
    private const int PORT = 2000;

    private Server _server;

    public CurrentContextReceiver CCReceiver => _ccReceiver;
    private CurrentContextReceiver _ccReceiver;

    private NetworkReceiver _networkReceiver;

    private PacketParser _parser;
    private ThreadManager _threadManager;

    private void Awake()
    {
        _threadManager = new ThreadManager();

        IEnumerable<Tile> field = FieldFactory.SimpleField6();
        Playground playground = new Playground(field);
        GameDirector gameDirector = new GameDirector(playground, 2);


        Unit u = UnitFactory.Warrior();
        u.Health.Current.Amount = 22;
        playground.AddUnit(u, playground.TileAt(new System.Numerics.Vector3(2, -2, 0)));


        _server = new Server(gameDirector, 50, LoggerManager.Server);

        _parser = new PacketParser(_threadManager);
        _networkReceiver = new NetworkReceiver(_server, PORT, _parser);

        _ccReceiver = new CurrentContextReceiver(_server);
    }

    void Update()
    {
        _threadManager.Update();
    }
}
