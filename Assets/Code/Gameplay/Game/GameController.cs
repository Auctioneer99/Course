using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class GameController
    {
        public bool HasAuthority => GameInstance.HasAuthority;

        public EGameStatus Status { get; private set; } = EGameStatus.Uninit;
        public bool IsRunning => Status == EGameStatus.Running;

        public bool IsInitialized { get; private set; }

        public Logger Logger => GameInstance.Logger;

        public FiniteGameStateMachine StateMachine { get; private set; }

        public ActionFactory ActionFactory { get; private set; }

        public PlayerManager PlayerManager { get; private set; }
        public ActionDistributor ActionDistributor { get; private set; }
        public RequestHolder RequestHolder { get; private set; }
        public LocalConnector Network { get; private set; }
        public EventManager EventManager { get; private set; }
        public TimeManager TimeManager { get; private set; }

        public GameInstance GameInstance { get; private set; }

        public GameController(GameInstance instance, bool isMainController)
        {
            Network = new LocalConnector(this);
            GameInstance = instance;
            ActionFactory = new ActionFactory(this);

            PlayerManager = new PlayerManager(this);
            ActionDistributor = new ActionDistributor(this);
            RequestHolder = new RequestHolder(this);

            EventManager = new EventManager(this);
            TimeManager = new TimeManager(this);

            StateMachine = new FiniteGameStateMachine(this);
        }

        public void SetupServer(int port)
        {
            //OnlineNetworkRegistrator.CreateServer(Network, port);
        }

        public void SetupClient(string ip, int port)
        {
            //reset
            //OnlineNetworkRegistrator.ConnectClient(Network, ip, port);
        }

        public void SetStatus(EGameStatus status)
        {
            if (Status != status)
            {
                Status = status;
                EventManager.OnGameStatusChanged.Invoke();
            }
        }
        /*
        public void Setup()
        {
            SetStatus(EGameStatus.Running);
            //PlayerManager.SetupPlayers(perspectivePlayer, players);

            if (HasAuthority)
            {
                TimeManager.SetupTimers();
            }
        }*/

        public void Start()
        {
            SetStatus(EGameStatus.Running);
            //PlayerManager.SetupPlayers(perspectivePlayer, players);

            if (HasAuthority)
            {
                TimeManager.SetupTimers();
            }

            StateMachine.TransitionTo(EGameState.AwaitingPlayers);
            //settings.Init
            Initialize();
        }

        private void Initialize()
        {
            if (IsInitialized)
            {
                throw new Exception("Already running");
            }
            IsInitialized = true;
            EventManager.OnGameInitialized.Invoke(this);
        }

        public void Update()
        {
            if (IsInitialized == false)
            {
                return;
            }

            TimeManager.Update();
            StateMachine.Update();
            //PlayerManager.Update();


            while (true)
            {
                if (Progress() == false)
                {
                    break;
                }
            }

            //EventManager.Update();
        }

        private bool Progress()
        {
            bool actionProgression = ActionDistributor.Update();
            bool requestProgression = RequestHolder.Update();

            if (IsRunning)
            {
                //
            }

            bool progressed = false;
            progressed |= actionProgression;
            progressed |= requestProgression;

            return progressed;
        }

        public bool IsFinished(bool ignoreRequests = false)
        {
            return true;
        }

        public void Reset(bool resetVisuals = false)
        {
            PlayerManager?.Reset();
        }
    }
}
