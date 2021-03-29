using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Gameplay
{
    public abstract class ActionManager : AManager
    {
        public List<AAction> Actions { get; private set; }
        private int _nextNetworkActionNumber;

        public ActionManager(GameController controller) : base(controller)
        {
            _nextNetworkActionNumber = 1;
        }

        public void AddAction(AAction action, bool toFront = false)
        {
            if (toFront)
            {
                Actions.Insert(0, action);
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
                Step();
                return true; 
            }
            return false;
        }

        private void Step()
        {
            AAction action = Actions.First();

            Actions.Remove(action);
            ApplyAction(action);
        }

        private void ApplyAction(AAction action)
        {
            SendAction(action);
        }

        private void SendAction(AAction action)
        {
            bool canSend = CanSend(action);
            if (canSend)
            {
                action.NetworkActionNumber = _nextNetworkActionNumber++;
            }

            /*history?*/

            if (canSend)
            {
                ICensoredAction censoredAction = action as ICensoredAction;
                if (censoredAction == null)
                {

                }

                GetDestination(action, out NetworkTarget target, out EPlayer player);
                Send(action, target, player);
            }
             
        }

        private void Send(AAction action, NetworkTarget target, EPlayer player)
        {
            IGateway network = GameController.Network;
            network.Send(action);
        }

        protected abstract bool CanSend(AAction action);
        protected abstract void GetDestination(AAction action, out NetworkTarget target, out EPlayer targetPlayer);
    }
}
