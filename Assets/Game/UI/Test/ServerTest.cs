using System.Collections.Generic;
using System.Linq;
using Aws.GameLift.Server;
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

       // _parser = new PacketParser(_threadManager);
        //_networkReceiver = new NetworkReceiver(_server, PORT, _parser);

        _ccReceiver = new CurrentContextReceiver(_server);
        //some();
    }

    void some()
    {


        var initSDKOutcome = GameLiftServerAPI.InitSDK();
        if (initSDKOutcome.Success)
        {
            ProcessParameters processParameters = new ProcessParameters(
                (gameSession) => {
                    print("callback 1");
                    //Respond to new game session activation request. GameLift sends activation request 
                    //to the game server along with a game session object containing game properties 
                    //and other settings. Once the game server is ready to receive player connections, 
                    //invoke GameLiftServerAPI.ActivateGameSession()
                    GameLiftServerAPI.ActivateGameSession();
                },
                () => {
                    print("callback 2");
                    //OnProcessTerminate callback. GameLift invokes this callback before shutting down 
                    //an instance hosting this game server. It gives this game server a chance to save
                    //its state, communicate with services, etc., before being shut down. 
                    //In this case, we simply tell GameLift we are indeed going to shut down.
                    GameLiftServerAPI.ProcessEnding();
                },
                () => {
                    print("callback 3");
                    //This is the HealthCheck callback.
                    //GameLift invokes this callback every 60 seconds or so.
                    //Here, a game server might want to check the health of dependencies and such.
                    //Simply return true if healthy, false otherwise.
                    //The game server has 60 seconds to respond with its health status. 
                    //GameLift will default to 'false' if the game server doesn't respond in time.
                    //In this case, we're always healthy!
                    return true;
                },
                //Here, the game server tells GameLift what port it is listening on for incoming player 
                //connections. In this example, the port is hardcoded for simplicity. Active game
                //that are on the same instance must have unique ports.
                PORT,
                new LogParameters(new List<string>()
                {
                    //Here, the game server tells GameLift what set of files to upload when the game session ends.
                    //GameLift uploads everything specified here for the developers to fetch later.
                    "/local/game/logs/myserver.log"
                }));

            //Calling ProcessReady tells GameLift this game server is ready to receive incoming game sessions!
            var processReadyOutcome = GameLiftServerAPI.ProcessReady(processParameters);
            if (processReadyOutcome.Success)
            {
                print("ProcessReady success.");
            }
            else
            {
                print("ProcessReady failure : " + processReadyOutcome.Error.ToString());
            }
        }
        else
        {
            print("InitSDK failure : " + initSDKOutcome.Error.ToString());
        }
    }

    void Update()
    {
        _threadManager.Update();
    }

    void OnApplicationQuit()
    {
        print("callback 4");
        //Make sure to call GameLiftServerAPI.ProcessEnding() when the application quits. 
        //This resets the local connection with GameLift's agent.
        //GameLiftServerAPI.ProcessEnding();
    }
}
