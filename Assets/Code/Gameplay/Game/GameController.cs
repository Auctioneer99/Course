using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class GameController : ICensored, IDeserializable
    {
        public bool HasAuthority => GameInstance.HasAuthority;

        public EGameStatus Status { get; private set; } = EGameStatus.Uninit;
        public bool IsRunning => Status == EGameStatus.Running;

        public bool IsInitialized { get; private set; }

        public Logger Logger => GameInstance.Logger;

        public FiniteGameStateMachine StateMachine { get; private set; }

        public ActionFactory ActionFactory { get; private set; }

        public CardManager CardManager { get; private set; }
        public PlayerManager PlayerManager { get; private set; }
        public ActionDistributor ActionDistributor { get; private set; }
        public RequestHolder RequestHolder { get; private set; }
        public LocalConnector Network => GameInstance.Network;
        public EventManager EventManager { get; private set; }

        public BoardManager BoardManager { get; private set; }
        //public TimeManager TimeManager { get; private set; }

        public GameInstance GameInstance { get; private set; }

        public GameController(GameInstance instance, bool isMainController)
        {
            //Network = new LocalConnector(this);
            GameInstance = instance;
            ActionFactory = new ActionFactory(this);

            StateMachine = new FiniteGameStateMachine(this);
            //TimeManager = new TimeManager(this);

            PlayerManager = new PlayerManager(this);
            ActionDistributor = new ActionDistributor(this);
            RequestHolder = new RequestHolder(this);

            EventManager = new EventManager(this);

            BoardManager = new BoardManager(this);
            CardManager = new CardManager(this);

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

            StateMachine.Start();
            /*
            if (HasAuthority)
            {
                TimeManager.SetupTimers();
            }

            StateMachine.TransitionTo(EGameState.AwaitingPlayers);
            */
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

            //TimeManager.Update();
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
            //Debug.Log("[Game Controller] IsFinished = " + (!ActionDistributor.HasActions && (ignoreRequests || !RequestHolder.HasRequests)));
            return !ActionDistributor.HasActions && (ignoreRequests || !RequestHolder.HasRequests);
                //&& !AbilityManager.HasInstances && !BoardManager.HasCardsInPlayStack() && DeathManager.CardsWaitingToDie.Count == 0;
        }

        public void Reset(bool resetVisuals = false)
        {
            PlayerManager?.Reset();
            //TimeManager?.Reset();
        }

        public void FromPacket(Packet packet)
        {
            IsInitialized = packet.ReadBool();
            Status = packet.ReadEGameStatus();
            StateMachine.FromPacket(this, packet);
            PlayerManager.FromPacket(this, packet);
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(IsInitialized)
                .Write(Status)
                .Write(StateMachine)
                .Write(PlayerManager);
            
        }

        public GameController Clone(GameInstance gi)
        {
            GameController gc = new GameController(gi, false);
            gc.Copy(this);
            return gc;
        }

        public void Copy(GameController controller)
        {
            IsInitialized = controller.IsInitialized;
            Status = controller.Status;
            StateMachine = controller.StateMachine.Clone(this);
            PlayerManager = controller.PlayerManager.Clone(this);
            //TimeManager = controller.TimeManager.Clone(this);
        }

        public void Censor(EPlayer player)
        {
            PlayerManager.Censor(player);
            StateMachine.Censor(player);
            //TimeManager.Censor(player);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[GameController]");
            sb.AppendLine($"Status = {Status}");
            sb.AppendLine($"Initialized = {IsInitialized}");
            sb.AppendLine(StateMachine.ToString());
            sb.AppendLine(PlayerManager.ToString());
           // sb.AppendLine(TimeManager.ToString());
            return sb.ToString();
        }
    }
}
