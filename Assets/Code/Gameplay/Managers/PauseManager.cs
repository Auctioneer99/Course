using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class PauseManager : AManager, IRuntimeDeserializable
    {
        public const long TIMEOUT = 10000;

        public int Counter { get; private set; } = 0;

        public Dictionary<EPauseType, int> TypesCounter { get; private set; }
        public List<PauseRequest> Requests { get; private set; }

        public PauseManager(GameController controller) : base(controller)
        {
            TypesCounter = new Dictionary<EPauseType, int>(EPauseTypeExtension.Array.Length);
            foreach(var type in EPauseTypeExtension.Array)
            {
                TypesCounter.Add(type, 0);
            }

            Requests = new List<PauseRequest>();
        }

        public bool HasPause(EPauseType type)
        {
            return TypesCounter[type] > 0;
        }

        public void Add(EPauseType type, long timeout = TIMEOUT)
        {
            UpdateTypesCount(type, true);

            Requests.Add(new PauseRequest(AllocateId(), type, timeout));
        }

        public void Add(PauseRequest pause)
        {
            UpdateTypesCount(pause.Type, true);

            Requests.Add(pause);
        }

        private void UpdateTypesCount(EPauseType type, bool toAdd)
        {
            int delta = toAdd ? 1 : -1;

            EPauseType[] arr = EPauseTypeExtension.Array;
            UnityEngine.Debug.Log($"<color=black>{type}</color>");
            for (int i = 0; i < arr.Length; i++)
            {
                EPauseType pauseType = arr[i];
                if (type.Contains(pauseType))
                {
                    UnityEngine.Debug.Log($"<color=black>{pauseType}</color>");
                    int previousCount = TypesCounter[pauseType];
                    int currentCount = previousCount + delta;

                    TypesCounter[pauseType] = currentCount;

                    if (previousCount == 0 && currentCount > 0)
                    {
                        GameController.EventManager.PauseToggled.Invoke(pauseType, true);
                    }
                    else
                    {
                        if (previousCount > 0 && currentCount == 0)
                        {
                            GameController.EventManager.PauseToggled.Invoke(pauseType, false);
                        }
                    }
                }
            }
        }

        public int AllocateId()
        {
            int result = Counter;
            Counter++;
            return result;
        }

        public void Remove(EPauseType type = EPauseType.Logic | EPauseType.Timers)
        {
            foreach(var req in Requests)
            {
                if (req.Type.Contains(type))
                {
                    UpdateTypesCount(req.Type, false);
                    Requests.Remove(req);
                }
            }
            /*
            for (int i = Requests.Count - 1; i >= 0; i--)
            {
                PauseRequest req = Requests[i];

                if (req.Requester == requester && req.Type.Contains(type))
                {
                    UpdateTypesCount(req.Type, false);
                    Requests.RemoveAt(i);
                }
            }*/
        }

        public void Remove(int id)
        {
            foreach(var r in Requests)
            {
                if (r.Id == id)
                {
                    UpdateTypesCount(r.Type, false);
                    Requests.Remove(r);
                    return;
                }
            }
        }

        public void Clear()
        {
            foreach (var r in Requests)
            {
                UpdateTypesCount(r.Type, false);
                Requests.Remove(r);
            }
        }

        public void Update()
        {
            long deltaTime = GameController.StateMachine.TimeManager.DeltaTime;
            if (deltaTime <= 0)
            {
                return;
            }

            for(int i = Requests.Count - 1; i >= 0; i--)
            {
                PauseRequest req = Requests[i];
                req.TimeRemaining -= deltaTime;
                Requests[i] = req;

                if (req.TimeRemaining <= 0)
                {
                    Remove(req.Id);
                }
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            throw new NotImplementedException();
        }

        public void ToPacket(Packet packet)
        {
            throw new NotImplementedException();
        }
    }
}
