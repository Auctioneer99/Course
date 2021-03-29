using System.Collections.Generic;
using System.Linq;

namespace Gameplay
{
    public class RequestManager : AManager
    {
        private int _requestCounter = 1;

        private List<ARequest> _requests;

        public bool HasRequests => _requests.Count > 0;

        public RequestManager(GameController controller) : base(controller)
        {
            _requests = new List<ARequest>();
        }
        /// <summary>
        /// Перенеси это
        /// </summary>
        /// <returns></returns>
        public int AllocateRequestId()
        {
            int id = _requestCounter;
            _requestCounter++;
            if (_requestCounter <= 0)
            {
                _requestCounter = 1;
            }
            return id;
        }

        public bool Update()
        {
            int length = _requests.Count;
            bool shouldExpire = false;


            for(int i = 0; i < length; i++)
            {
                ARequest req = _requests[i];
                if (req.IsFulfilled())
                {
                    req.HandleFulfilled();
                    _requests.Remove(req);
                    return true;
                }
                else
                {
                    if (shouldExpire)
                    {
                        if (req.Expired == false)
                        {
                            GameController.Logger.LogWarning($"Request expired: {req.ERequest}");
                            req.HandleExpired();
                            req.Expired = true;
                            return true;
                        }
                        return false;
                    }

                }
            }

            return false;
        }

        public void AddRequest(ARequest request)
        {
            request.Apply();
            _requests.Insert(0, request);
        }

        public void CancelRequest(EPlayer player, int requestId)
        {
            ARequest request = Get<ARequest>(player, requestId);

            if (request == null)
            {
                GameController.Logger.LogError($"cant find request with id {requestId} for player {player}");
                return;
            }

            request.HandleCancelled();
            _requests.Remove(request);
        }

        public T Get<T>(EPlayer player, bool includeFulfilled = true) where T: ARequest
        {
            return
                _requests.Where(item =>
                    item is T request &&
                    player.Contains(request.PlayerId) &&
                    (includeFulfilled || request.IsFulfilled() == false))
                .FirstOrDefault() as T;
        }

        public T Get<T>(EPlayer player, int requestId) where T: ARequest
        {
            if (Get(requestId) is T request &&
                (player == EPlayer.Undefined || player.Contains(request.PlayerId)))
            {
                return request;
            }
            return null;
        }

        public ARequest Get(int requestId)
        {
            return _requests
                .Where(r => r.Id == requestId)
                .FirstOrDefault();
        }

        public ARequest Get(EPlayer player, ERequest requestType)
        {
            return _requests
                .Where(item => item.ERequest == requestType)
                .FirstOrDefault();
        }
    }
}
