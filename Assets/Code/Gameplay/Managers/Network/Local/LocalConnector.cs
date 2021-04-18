using UnityEngine;

namespace Gameplay
{
    public class LocalConnector : AConnector
    {
        public GameController Controller { get; private set; }

        public LocalConnector(GameController controller)
        {
            Controller = controller;
        }

        public override void GetDecks()
        {

        }

        public override void Update()
        {
            Controller.Update();
        }

        public override void Send(AAction action)
        {
            if (action is ICensored)
            {
                foreach (var p in Controller.PlayerManager.Players.Values)
                {
                    CensorSend(action, p.EPlayer);
                }
                if (Controller.PlayerManager.HasSpectators)
                {
                    Manager.SendToGroup(this, action, EPlayer.Spectators);
                }
            }
            else
            {
                if (Controller.HasAuthority)
                {
                    Manager.SendToGroup(this, action, EPlayer.NonAuthority);
                }
                else
                {
                    Manager.SendToHost(this, action);
                }
            }
        }

        private void CensorSend(AAction action, EPlayer targetPlayer)
        {
            AAction copyToCensor = Controller.ActionFactory.Create(action.EAction);
            copyToCensor.Copy(action, Controller);
            (copyToCensor as ICensored).Censor(targetPlayer);
            Manager.SendToGroup(this, action, targetPlayer);
        }

        public override void HandleMessage(AConnector sender, AAction action)
        {
            if (CanHandleMessage(sender, action) == false)
            {
                Debug.Log("Cant handle message");
                return;
            }
            action = action.Clone(Controller);

            if (action is IPriorityAction)
            {
                Controller.ActionDistributor.HandleAction(action);
            }
            else
            {
                Controller.ActionDistributor.Add(action);
            }
        }

        private bool CanHandleMessage(AConnector connector, AAction action)
        {
            bool isPlayerAction = action is APlayerAction;
            bool isAuthorityAction = action is IAuthorityAction;
            //EPlayer senderRole = sender.Role;

            if (Controller.HasAuthority)
            {
                if (isPlayerAction)
                {
                    return true;
                }
            }
            else
            {
                if (isAuthorityAction)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
