using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Gameplay
{
    public class ActionDistributor : AManager
    {
        public List<AAction> Actions { get; private set; }
        private int _nextNetworkActionNumber;

        public ActionDistributor(GameController controller) : base(controller)
        {
            _nextNetworkActionNumber = 1;
            Actions = new List<AAction>();
        }

        public void Add(AAction action, bool toFront = false)
        {
            if (toFront)
            {
                if (GameController.HasAuthority || action is IPriorityAction)
                {
                    Actions.Insert(0, action);
                }
                else
                {
                    throw new System.Exception("Cant push to front");
                }
            }
            else
            {
                Actions.Add(action);
            }
        }

        public bool Update()
        {
            if (Actions.Count > 0)
            {
                Debug.Log($"<color=black>Actions.Count = {Actions.Count}</color>");
                Step();
                return true; 
            }
            return false;
        }

        private void Step()
        {
            AAction action = Actions.First();

            Actions.Remove(action);
            HandleAction(action);
        }

        public void HandleAction(AAction action)
        {
            bool isValid = action.IsValid();

            ARequest request = null;

            if (isValid)
            {
                request = action as ARequest;
                if (request != null)
                {
                    GameController.RequestHolder.Add(request);
                }

                action.Apply();

                SendAction(action);
            }
            
           // if (isValid == false || request == null)
          //  {
          //      DestroyAction(action);
          //  }
        }

        private void SendAction(AAction action)
        {
            bool canSend = CanSend(action);
            if (canSend)
            {
                if (GameController.HasAuthority)
                {
                    action.NetworkActionNumber = _nextNetworkActionNumber++;
                }
                else
                {
                    action.NetworkActionNumber = GameController.PlayerManager.CurrentPlayer.PingStatus.LastKnownNetworkPacketId;
                }

                GameController.Network.Send(action);
            }
        }

        private void DestroyAction(AAction action)
        {

        }

        private bool CanSend(AAction action)
        {
            bool isClientAction = action is IUserAction;
            if (GameController.HasAuthority)
            {
                bool isServerSide = action is IAuthoritySideAction;
                bool canSend = (isClientAction || isServerSide) == false;

                if (canSend)
                {
                    return (action is ITargetedAction targetedAction
                        && targetedAction.Target.Contains(NetworkTarget.TargetPlayer)
                        && targetedAction.TargetPlayer == EPlayer.Undefined) == false;
                }
            }
            else
            {
                bool isAuthoritativeAction = action is IAuthorityAction;

                if ((isClientAction || isAuthoritativeAction) == false)
                {
                    throw new System.Exception("Unauthrized action send");
                }
                return !isAuthoritativeAction && isClientAction;
            }
            return false;
        }
    }
}
